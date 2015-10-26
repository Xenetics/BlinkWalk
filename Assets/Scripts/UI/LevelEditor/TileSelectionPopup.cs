using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Popup Manager script for Tile Selection
/// </summary>
public class TileSelectionPopup : Singleton<TileSelectionPopup>
{
    protected TileSelectionPopup() { }

    /// <summary> Scroll Area that will contain list objects </summary>
    [SerializeField]
    private GameObject m_ScrollArea;
    /// <summary> Will hold all list objects </summary>
    [SerializeField]
    private List<GameObject> ListObjects;
    /// <summary> Spacing between list objects </summary>
    [SerializeField]
    private float m_Spacing;

    void Start()
    {
        ListObjects = new List<GameObject>();
    }

    private void BuildList()
    {

    }

    public void Close()
    {
        gameObject.SetActive(false);
        LevelEditor.Instance.SelectedTile = null;
    }
}
