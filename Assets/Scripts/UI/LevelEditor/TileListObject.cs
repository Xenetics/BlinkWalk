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
    public Text Title;
    /// <summary> The tile type this will select </summary>
    [HideInInspector]
    public Tile.TileType Tiletype;
    [HideInInspector]
    public GameObject ListPopup;

    /// <summary> Handles the button on the tile list object </summary>
    public void Select()
    {
        ListPopup.GetComponent<TileSelectionPopup>().ScrollBar.value = 1;
        ListPopup.SetActive(false);
        LevelEditor.Instance.SelectedTile.SetTile(Tiletype);
        LevelEditor.Instance.SelectedTile = null;
        RaycastMouse.Instance.Busy = false;
    }
}
