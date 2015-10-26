using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Raycast mouse pointer for selection
/// </summary>
public class RaycastMouse : Singleton<RaycastMouse> 
{
    protected RaycastMouse() { }

    /// <summary> Mouse Coordinates on click </summary>
	private Vector2 mouseCoord;
    /// <summary> Ui Popup for the tile Selection </summary>
    [SerializeField]
    private GameObject m_Popup;

    void Update () 
	{
        if (Input.GetMouseButtonUp(0) && LevelEditor.Instance.SelectedTile == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.tag)
                {
                    case "Tile":
                        LevelEditor.Instance.SelectedTile = hit.collider.gameObject.GetComponent<Tile>();
                        mouseCoord = Input.mousePosition;
                        m_Popup.transform.position = new Vector3(mouseCoord.x, mouseCoord.y, m_Popup.transform.position.z);
                        m_Popup.SetActive(true);
                        break;
                }
            }
        }
    }
} 
