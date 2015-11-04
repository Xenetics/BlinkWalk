using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Handle level editing
/// </summary>
public class LevelEditor : Singleton<LevelEditor>
{
    protected LevelEditor() { }

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

	void Start ()
    {
        m_Tiles = new List<GameObject>();
        BuildTiles();   
    }
	

	void Update ()
    {
	    
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

    public void Export()
    {
        // Parse to file and upload to azure
    }

    public void Import()
    {
        // Call to azure dll and pass in input
    }
}
