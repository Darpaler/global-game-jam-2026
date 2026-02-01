using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ColorLayerManager : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private XROrigin _xROrigin;

    private LayerMask _currentColorLayer;

    [SerializeField]
    private XRSocketInteractor _faceSocket;

    [SerializeField]
    [Tooltip("The Snap Teleportation Provider locomotion component.")]
    private SnapTeleportationProvider _snapTeleportationProvider;

    [SerializeField]
    [Tooltip("The volume to change the color of.")]
    private Volume _colorVolume;

    [SerializeField]
    [Tooltip("The default color layer the player is on.")]
    private LayerMask _defaultColorLayer;

    [SerializeField]
    [Tooltip("The default volume profile for that color layer.")]
    private VolumeProfile _defaultVolumeProfile;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        _xROrigin = GetComponent<XROrigin>();
        SetColorLayer(_defaultColorLayer, _defaultVolumeProfile);
    }

    private void OnEnable()
    {
        _faceSocket.selectEntered.AddListener(SetColorLayer);
        _faceSocket.selectExited.AddListener(SetColorLayer);
    }

    private void OnDisable()
    {
        _faceSocket.selectEntered.RemoveListener(SetColorLayer);
        _faceSocket.selectExited.RemoveListener(SetColorLayer);
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetColorLayer(SelectEnterEventArgs args)
    {
        Mask mask = args.interactableObject.transform.GetComponent<Mask>();
        if (mask)
            SetColorLayer(mask.ColorLayer, mask.VolumeProfile);
    }

    private void SetColorLayer(SelectExitEventArgs args)
    {
        SetColorLayer(_defaultColorLayer, _defaultVolumeProfile);
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetColorLayer(LayerMask colorLayer, VolumeProfile volumeProfile)
    {
        _xROrigin.Camera.cullingMask -= _currentColorLayer;
        _xROrigin.Camera.cullingMask += colorLayer;

        _snapTeleportationProvider.LayerMask -= _currentColorLayer;
        _snapTeleportationProvider.LayerMask += colorLayer;

        _currentColorLayer = colorLayer;

        _colorVolume.profile = volumeProfile;
    }
    #endregion
}
