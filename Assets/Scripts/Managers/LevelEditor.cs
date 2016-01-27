using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Handle level editing
/// </summary>
public class LevelEditor : MonoBehaviour
{
    private static LevelEditor instance = null;
    public static LevelEditor Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }
    /// <summary> UI object for the import file name input </summary>
    [SerializeField]
    private InputField m_ImportInput;
    /// <summary> UI object for the export file name input </summary>
    [SerializeField]
    private InputField m_ExportInput;
    /// <summary> Width of editing area </summary>
    [SerializeField]
    private int m_Width;
    /// <summary> Height of editing area </summary>
    [SerializeField]
    private int m_Height;
    /// <summary> Tile object for selection </summary>
    [SerializeField]
    private GameObject m_Prefab;
    /// <summary> List of tile objects </summary>
    private List<GameObject> m_Tiles;
    /// <summary> Selected Tile object </summary>
    [HideInInspector]
    public Tile SelectedTile;
    /// <summary> The tile at the Center of the sheet </summary>
    public GameObject CenterTile;

	void Start ()
    {
        m_Tiles = new List<GameObject>();
        BuildTiles();
        //CenterTile = FindCenterTile();
    }
	
    /// <summary> Builds the tile grid a column at a time </summary>
    private void BuildTiles()
    {
        for(int i = 0; i < m_Width; i++)
        {
            for(int j = 0; j < m_Height; j++)
            {
                GameObject newTile = Instantiate(m_Prefab);
                newTile.name = m_Prefab.name;
                newTile.transform.position = new Vector3(i, j, 0);
                newTile.transform.SetParent(gameObject.transform);
                m_Tiles.Add(newTile);
            }
        }
    }

    /// <summary> Exports the Level to azure storage </summary>
    public void Export()
    {
        // Build string / File
        // Call to azure helper
    }

    /// <summary> Imports the level from azure storage </summary>
    public void Import()
    {
        // use input to call into azure
        // decode string / File
        // build level
    }

    /// <summary> Finds the tile at the center of the sheet </summary>
    public GameObject FindCenterTile()
    {
        return m_Tiles[(m_Tiles.Count / 2) + (m_Height / 2)];
    }

    /// <summary> Returns the height of tile set </summary>
    /// <returns> Height of tile set </returns>
    public int GetHeight()
    {
        return m_Height;
    }

    /// <summary> Returns the width of tile set </summary>
    /// <returns> width of tile set </returns>
    public int GetWidth()
    {
        return m_Width;
    }
}
