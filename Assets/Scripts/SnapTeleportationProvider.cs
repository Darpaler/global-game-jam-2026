using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

/// <summary>
/// Replaces smooth movement with a teleport anchor based grid movement system.
/// Requires a Teleportation Provider.
/// </summary>
public class SnapTeleportationProvider : LocomotionProvider
{
    #region PRIVATE_PROPERTIES
    float _timeStarted;

    [SerializeField]
    [Tooltip("The layer to use for interaction.")]
    private LayerMask _layerMask;

    [SerializeField]
    [Tooltip("The maximum teleportation distance.")]
    private float _maxTeleportationDistance = 15f;

    [SerializeField]
    [Tooltip("The amount of time that the system will wait before starting another snap move.")]
    float _debounceTime = 0.5f;

    [SerializeField]
    [Tooltip("Controls whether to enable left & right strafing.")]
    bool _enableStrafing = true;

    [SerializeField]
    [Tooltip("Reads input data from the left hand controller. Input Action must be a Value action type (Vector 2).")]
    XRInputValueReader<Vector2> _leftHandMoveInput = new XRInputValueReader<Vector2>("Left Hand Move");

    [SerializeField]
    [Tooltip("Reads input data from the right hand controller. Input Action must be a Value action type (Vector 2).")]
    XRInputValueReader<Vector2> _rightHandMoveInput = new XRInputValueReader<Vector2>("Right Hand Move");
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnEnable()
    {
        // Enable and disable directly serialized actions with this behavior's enabled lifecycle.
        _leftHandMoveInput.EnableDirectActionIfModeUsed();
        _rightHandMoveInput.EnableDirectActionIfModeUsed();
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnDisable()
    {
        // Enable and disable directly serialized actions with this behavior's enabled lifecycle.
        _leftHandMoveInput.DisableDirectActionIfModeUsed();
        _rightHandMoveInput.DisableDirectActionIfModeUsed();
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Update()
    {
        // Wait for a certain amount of time before allowing another move.
        if (_timeStarted > 0f && (_timeStarted + _debounceTime < Time.time))
        {
            _timeStarted = 0f;
            return;
        }

        var input = ReadInput();
        TeleportationAnchor target = GetTeleportationTarget(input);

        if (target != null)
        {
            _timeStarted = Time.time;
            target.RequestTeleport();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    Vector2 ReadInput()
    {
        var leftHandValue = _leftHandMoveInput.ReadValue();
        var rightHandValue = _rightHandMoveInput.ReadValue();

        return leftHandValue + rightHandValue;
    }

    /// <summary>
    /// Determines the move amount for the given <paramref name="input"/> vector.
    /// </summary>
    /// <param name="input">Input vector, such as from a thumbstick.</param>
    /// <returns>Returns the move amount for the given <paramref name="input"/> vector.</returns>
    protected virtual TeleportationAnchor GetTeleportationTarget(Vector2 input)
    {
        if (input == Vector2.zero || _timeStarted > 0f)
            return null;

        RaycastHit hit;
        Vector3 rayOrigin = mediator.xrOrigin.transform.position;
        
        Vector3 rayForward = mediator.xrOrigin.Camera.transform.forward;
        rayForward.y = 0;

        Vector3 rayRight = mediator.xrOrigin.Camera.transform.right;
        rayRight.y = 0;

        TeleportationAnchor target = null;
        
        var cardinal = CardinalUtility.GetNearestCardinal(input);
        switch (cardinal)
        {
            case Cardinal.North:
                Physics.Raycast(rayOrigin, rayForward, out hit, _maxTeleportationDistance, _layerMask);
                if(hit.collider)
                    target = hit.collider.gameObject.GetComponent<TeleportationAnchor>();
                break;
            case Cardinal.South:
                Physics.Raycast(rayOrigin, -rayForward, out hit, _maxTeleportationDistance, _layerMask);
                if(hit.collider)
                    target = hit.collider.gameObject.GetComponent<TeleportationAnchor>();
                break;
            case Cardinal.East:
                if (_enableStrafing)
                {
                    Physics.Raycast(rayOrigin, rayRight, out hit, _maxTeleportationDistance, _layerMask);
                    if(hit.collider)
                        target = hit.collider.gameObject.GetComponent<TeleportationAnchor>();
                }
                break;
            case Cardinal.West:
                if (_enableStrafing)
                {
                    Physics.Raycast(rayOrigin, -rayRight, out hit, _maxTeleportationDistance, _layerMask);
                    if(hit.collider)
                        target = hit.collider.gameObject.GetComponent<TeleportationAnchor>();
                }
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(Cardinal)}={cardinal}");
                break;
        }
        return target;
    }
    #endregion
}
