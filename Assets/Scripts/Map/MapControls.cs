using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapControls : MonoBehaviour
{
    [SerializeField] private Toggle m_showBlueToggle;
    [SerializeField] private Toggle m_showRedToggle;
    [SerializeField] InputActionReference m_menuButtonInputActionRef;

    private void Start()
    {
        m_showBlueToggle.onValueChanged.AddListener(ToggleBlue);
        m_showRedToggle.onValueChanged.AddListener(ToggleRed);

        m_menuButtonInputActionRef.action.performed += ToggleMiniMapActive;

        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_showBlueToggle.onValueChanged.RemoveListener(ToggleBlue);
        m_showRedToggle.onValueChanged.RemoveListener(ToggleRed);

        m_menuButtonInputActionRef.action.performed -= ToggleMiniMapActive;

    }

    private void ToggleBlue(bool value)
    {
        MapTile.DisplayBlueTiles = value;
    }

    private void ToggleRed(bool value)
    {
        MapTile.DisplayRedTiles = value;
    }

    public void ToggleMiniMapActive(InputAction.CallbackContext context)
    {
        if (this.isActiveAndEnabled)
            this.gameObject.SetActive(false);
        else
            this.gameObject.SetActive(true);
    }
}
