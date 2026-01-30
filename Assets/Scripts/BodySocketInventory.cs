using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[System.Serializable]
public class BodySocket
{
    [Tooltip("The XR Socket Interactor component.")]
    public XRSocketInteractor Socket;
    
    [Range(0.01f, 1f)]
    [Tooltip("The height ratio for where the socket should be from the floor to the head.")]
    public float HeightRatio;
}

public class BodySocketInventory : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private Vector3 _currentCameraPosition;
    private Quaternion _currentCameraRotation;

    [SerializeField]
    [Tooltip("The XR Origin that the XR Socket Interactors are attached to.")]
    private XROrigin _xROrigin;
    [SerializeField]
    [Tooltip("The XR Socket Interactors attached to the XR Origin.")]
    private BodySocket[] _bodySockets;
    #endregion

    #region UNITY_METHODS
    private void Update()
    {
        _currentCameraPosition = _xROrigin.Camera.transform.position;
        _currentCameraRotation = _xROrigin.Camera.transform.rotation;

        foreach(BodySocket bodySocket in _bodySockets)
            bodySocket.Socket.transform.position = new Vector3(bodySocket.Socket.transform.position.x, _currentCameraPosition.y * bodySocket.HeightRatio, bodySocket.Socket.transform.position.z);
        transform.position = new Vector3(_currentCameraPosition.x, 0, _currentCameraPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentCameraRotation.y, transform.rotation.z, _currentCameraRotation.w);
    }
    #endregion
}
