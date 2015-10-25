using UnityEngine;
using System.Collections;

/// <summary>
/// Created -> 10/25/15
/// Tile Object
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>  </summary>
    public enum TileType { Empty, Block, Spike, Start, End }
    /// <summary>  </summary>
    [SerializeField]
    private TileType m_CurrentType = TileType.Empty;
    /// <summary>  </summary>
    [SerializeField]
    private GameObject m_Block;
    /// <summary>  </summary>
    [SerializeField]
    private GameObject m_Spike;
    /// <summary>  </summary>
    [SerializeField]
    private GameObject m_Start;
    /// <summary>  </summary>
    [SerializeField]
    private GameObject m_End;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newType"></param>
    public void SetTile(TileType newType)
    {
        SanatizeTile();
        switch (newType)
        {
            case TileType.Empty:
                
                break;
            case TileType.Block:
                m_Block.SetActive(true);
                break;
            case TileType.Spike:
                m_Spike.SetActive(true);
                break;
            case TileType.Start:
                m_Start.SetActive(true);
                break;
            case TileType.End:
                m_End.SetActive(true);
                break;
        }
    }

    /// <summary> Sanatized Tiles to empty </summary>
    private void SanatizeTile()
    {
        m_Spike.SetActive(false);
        m_Start.SetActive(false);
        m_End.SetActive(false);
    }
}