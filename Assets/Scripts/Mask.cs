using UnityEngine;
using UnityEngine.Rendering;

public class Mask : Item
{
    [Tooltip("The color layer that will be activated when wearing this mask.")]
    public LayerMask ColorLayer;

    [Tooltip("The volume profile for that color layer.")]
    public VolumeProfile VolumeProfile;
}
