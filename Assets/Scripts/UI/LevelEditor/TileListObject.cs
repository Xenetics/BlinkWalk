using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Tile ListObject for the selection popup
/// </summary>
public class TileListObject : MonoBehaviour
{
    /// <summary> Title Text Object </summary>
    public Text m_Title;
    /// <summary> The tile type this will select </summary>
    public Tile.TileType m_Tiletype;

    /// <summary> Handles the button on the tile list object </summary>
    public void Select()
    {
        LevelEditor.Instance.SelectedTile.SetTile(m_Tiletype);
        LevelEditor.Instance.SelectedTile = null;
    }
}
