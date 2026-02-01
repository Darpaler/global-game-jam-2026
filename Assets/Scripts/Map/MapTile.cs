using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[ExecuteAlways]
public class MapTile : MonoBehaviour
{
    [SerializeField] private Color m_red;
    [SerializeField] private Color m_blue;
    [SerializeField] private Color m_gray;
    private Color m_myColor = Color.clear;
    private SpriteRenderer m_mySpriteRend;
    private enum TileType { Red, Blue, Gray};
    private TileType m_TileType = TileType.Gray;

    public static bool DisplayBlueTiles = false;
    public static bool DisplayRedTiles = false;
    public static bool DisplayGrayTiles = true;

    void Awake()
    {
        m_mySpriteRend = GetComponent<SpriteRenderer>();

        if (m_mySpriteRend)
            AssignTileColor();
    }

    private void Update()
    {
        switch (m_TileType)
        {
            case TileType.Red:
                if (DisplayRedTiles && !m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = true;
                else if (!DisplayRedTiles && m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = false;
                    break;
            case TileType.Blue:
                if (DisplayBlueTiles && !m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = true;
                else if (!DisplayBlueTiles && m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = false;
                break;
            case TileType.Gray:
                if (DisplayGrayTiles && !m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = true;
                else if (!DisplayGrayTiles && m_mySpriteRend.enabled)
                    m_mySpriteRend.enabled = false;
                break;
            default:
                Debug.Log("Tile type not implemented!");
                break;
        }

    }

    private void AssignTileColor()
    {
        string _parentLayer = LayerMask.LayerToName(transform.parent.gameObject.layer);

        switch (_parentLayer)
        {
            case "Gray":
                m_myColor = m_gray;
                m_TileType = TileType.Gray;
                break;

            case "Blue":
                m_myColor = m_blue;
                m_TileType = TileType.Blue;
                break;

            case "Red":
                m_myColor = m_red;
                m_TileType = TileType.Red;
                break;

            default:
                Debug.Log("Parent Layer Name not recognized in MapTile: " + _parentLayer);
                m_myColor = Color.clear;
                break;
        }

        m_mySpriteRend.color = m_myColor;
    }

}
