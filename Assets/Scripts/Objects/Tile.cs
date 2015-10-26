using UnityEngine;
using System.Collections;

/// <summary>
/// Created -> 10/25/15
/// Tile Object
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary> Enum for types of tiles to use in the switch </summary>
    public enum TileType { Empty, Block, Spike, Start, End, COUNT }
    /// <summary> Current Tile type </summary>
    [SerializeField]
    private TileType m_CurrentType = TileType.Empty;
    /// <summary> Block Tile object </summary>
    [SerializeField]
    private GameObject m_Block;
    /// <summary> Spike Tile object </summary>
    [SerializeField]
    private GameObject m_Spike;
    /// <summary> Start Tile object </summary>
    [SerializeField]
    private GameObject m_Start;
    /// <summary> End Tile object </summary>
    [SerializeField]
    private GameObject m_End;

    /// <summary> Builds the Tile Grid </summary>
    /// <param name="newType"> The tile type you would like to make </param>
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