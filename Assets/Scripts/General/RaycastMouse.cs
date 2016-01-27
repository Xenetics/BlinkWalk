using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Created -> 10/25/15
/// Raycast mouse pointer for selection
/// </summary>
public class RaycastMouse : MonoBehaviour
{
    private static RaycastMouse instance = null;
    public static RaycastMouse Instance { get{ return instance; } }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    /// <summary> Mouse Coordinates on click </summary>
	private Vector2 m_MouseCoord;
    /// <summary> Ui Popup for the tile Selection </summary>
    [SerializeField]
    private GameObject m_Popup;
    /// <summary> Speed at which the Camera will move </summary>
    [SerializeField]
    private float m_CamSpeed = 0.5f;
    [HideInInspector]
    public bool Busy = false;

    void Start()
    {
        Camera.main.transform.position = new Vector3(LevelEditor.Instance.CenterTile.transform.position.x, LevelEditor.Instance.CenterTile.transform.position.y, Camera.main.transform.position.z);
    }

    void Update () 
	{
        m_MouseCoord = Input.mousePosition;
        if (Input.GetMouseButtonDown(0) && Busy == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.tag)
                {
                    case "Tile":
                        LevelEditor.Instance.SelectedTile = hit.collider.gameObject.GetComponent<Tile>();
                        m_Popup.transform.position = new Vector3(m_MouseCoord.x, m_MouseCoord.y, m_Popup.transform.position.z);
                        m_Popup.SetActive(true);
                        Busy = true;
                        break;
                }
            }
        }

        if (!m_Popup.activeSelf && (Vector2.Distance(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(m_MouseCoord)) > Camera.main.orthographicSize))
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(m_MouseCoord);
            Camera.main.transform.position =  Vector3.Lerp(Camera.main.transform.position, new Vector3(newPos.x, newPos.y, Camera.main.transform.position.z), 0.5f * Time.deltaTime);
        }
    }
} 
