using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorLayerManager : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private XROrigin _xROrigin;

    private LayerMask _currentColorLayer;

    [SerializeField]
    [Tooltip("The Snap Teleportation Provider locomotion component.")]
    private SnapTeleportationProvider _snapTeleportationProvider;

    [SerializeField]
    [Tooltip("The default color layer the player is on.")]
    private LayerMask _defaultColorLayer, testColor;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        _xROrigin = GetComponent<XROrigin>();
        SetColorLayer(_defaultColorLayer);
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetColorLayer(LayerMask colorLayer)
    {
        _xROrigin.Camera.cullingMask -= _currentColorLayer;
        _xROrigin.Camera.cullingMask += colorLayer;

        _snapTeleportationProvider.LayerMask -= _currentColorLayer;
        _snapTeleportationProvider.LayerMask += colorLayer;

        _currentColorLayer = colorLayer;
    }

    public void SetColorLayer(string color)
    {
        
        LayerMask colorLayer = 1 << LayerMask.NameToLayer(color);
        SetColorLayer(colorLayer);
    }
    #endregion
}
