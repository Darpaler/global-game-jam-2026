using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private const string GRIP_ACTION = "Grip", PINCH_ACTION = "Pinch";

    private Animator _animator;

    [SerializeField] private InputActionReference _gripActionReference, _pinchActionReference;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_animator != null)
        {
            float gripValue = _gripActionReference.action.ReadValue<float>();
            float pinchValue = _pinchActionReference.action.ReadValue<float>();

            _animator.SetFloat(GRIP_ACTION, gripValue);
            _animator.SetFloat(PINCH_ACTION, pinchValue);
        }
    }
    #endregion
}
