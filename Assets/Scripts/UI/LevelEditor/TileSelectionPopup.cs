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
    private List<GameObject> m_ListObjects;
    /// <summary> UI gameobject for the list of tile types object </summary>
    [SerializeField]
    private GameObject m_ListPrefab;
    /// <summary> Percent of total height of list object to space</summary>
    [SerializeField]
    private float m_SpacingPercent = 0.05f;
    /// <summary> Spacing between list objects </summary>
    private float m_Spacing;

    void Start()
    {
        m_ListObjects = new List<GameObject>();
        BuildList();
    }

    private void BuildList()
    {
        m_Spacing = (int)Tile.TileType.COUNT * (m_ListPrefab.GetComponent<RectTransform>().sizeDelta.y) * m_SpacingPercent;
        m_ScrollArea.GetComponent<RectTransform>().sizeDelta =  new Vector2(m_ScrollArea.GetComponent<RectTransform>().sizeDelta.x
                                                                            , m_Spacing
                                                                            + ((int)Tile.TileType.COUNT * (m_ListPrefab.GetComponent<RectTransform>().sizeDelta.y + m_Spacing)));
        m_ScrollArea.GetComponent<RectTransform>().position =   new Vector2(m_ScrollArea.transform.parent.GetComponent<RectTransform>().position.x
                                                                            , m_ScrollArea.transform.parent.GetComponent<RectTransform>().anchoredPosition.y
                                                                            + ((m_ScrollArea.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.5f))
                                                                            - (m_ScrollArea.GetComponent<RectTransform>().sizeDelta.y * 0.5f));
        for (int i = 0; i < (int)Tile.TileType.COUNT; i++)
        {
            GameObject temp = Instantiate(m_ListPrefab);
              
            if (i != 0)
            {
                temp.GetComponent<RectTransform>().position =   new Vector3(m_ScrollArea.GetComponent<RectTransform>().position.x
                                                                            , m_ListObjects[i - 1].GetComponent<RectTransform>().position.y
                                                                            - m_ListPrefab.GetComponent<RectTransform>().sizeDelta.y 
                                                                            - m_Spacing
                                                                            , m_ListPrefab.transform.position.z);
            }
            else
            {
                temp.GetComponent<RectTransform>().position =   new Vector3(m_ScrollArea.GetComponent<RectTransform>().position.x
                                                                            , m_ScrollArea.GetComponent<RectTransform>().position.y
                                                                            + (m_ScrollArea.GetComponent<RectTransform>().sizeDelta.y * 0.5f)
                                                                            - (m_ListPrefab.GetComponent<RectTransform>().sizeDelta.y * 0.5f)
                                                                            - m_Spacing
                                                                            , m_ListPrefab.transform.position.z);
            }
            temp.transform.SetParent(m_ScrollArea.transform);
            temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            temp.GetComponent<TileListObject>().Tiletype = (Tile.TileType)i;
            temp.GetComponent<TileListObject>().Title.text = temp.GetComponent<TileListObject>().Tiletype.ToString();
            m_ListObjects.Add(temp);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        LevelEditor.Instance.SelectedTile = null;
    }
}
