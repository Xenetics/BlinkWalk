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
    private float m_CamSpeed = 1f;
    [HideInInspector]
    public bool Busy = false;
    /// <summary> Mouse Deadzone </summary>
    public Vector2 Deadsone = new Vector2();
    /// <summary> Center Screen </summary>
    public Vector2 CenterScreen = new Vector2();

    void Start()
    {
        LevelEditor.Instance.CenterTile = LevelEditor.Instance.FindCenterTile();
        Deadsone.x = Screen.width * 0.25f;
        Deadsone.y = Screen.height * 0.25f;
        CenterScreen.x = Screen.width * 0.5f;
        CenterScreen.y = Screen.height * 0.5f;
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
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (Input.GetMouseButton(1) && !MouseOffScreen() && !MouseInDeadzone())
        {
            if (!m_Popup.activeSelf)
            {
                Vector2 newPos = Camera.main.ScreenToWorldPoint(m_MouseCoord);
                if (    (Camera.main.transform.position.x < LevelEditor.Instance.MaxBounds.x || newPos.x < LevelEditor.Instance.MaxBounds.x)
                    &&  (Camera.main.transform.position.x > LevelEditor.Instance.MinBounds.x || newPos.x > LevelEditor.Instance.MinBounds.x)
                    &&  (Camera.main.transform.position.y < LevelEditor.Instance.MaxBounds.y || newPos.y < LevelEditor.Instance.MaxBounds.y)
                    &&  (Camera.main.transform.position.y > LevelEditor.Instance.MinBounds.y || newPos.y > LevelEditor.Instance.MinBounds.y))
                {
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(newPos.x, newPos.y, Camera.main.transform.position.z), 0.5f * Time.deltaTime * m_CamSpeed);
                }
            }
        }
    }

    /// <summary> returns true if mouse if off screen </summary>
    private bool MouseOffScreen()
    {
        bool offScreen = false;
        if(m_MouseCoord.x < CenterScreen.x - Screen.width * 0.5f)
        {
            offScreen = true;
        }
        if(m_MouseCoord.x > CenterScreen.x + Screen.width * 0.5f)
        {
            offScreen = true;
        }
        if(m_MouseCoord.y < CenterScreen.y - Screen.height * 0.5f)
        {
            offScreen = true;
        }
        if(m_MouseCoord.y > CenterScreen.y + Screen.height * 0.5f)
        {
            offScreen = true;
        }
        return offScreen;
    }

    /// <summary> Returns true if mouse is in deadzone </summary>
    private bool MouseInDeadzone()
    {
        bool inDeadzone = true;
        if(m_MouseCoord.x > CenterScreen.x + Deadsone.x)
        {
            inDeadzone = false;
        }
        if(m_MouseCoord.x < CenterScreen.x - Deadsone.x)
        {
            inDeadzone = false;
        }
        if(m_MouseCoord.y > CenterScreen.y + Deadsone.y)
        {
            inDeadzone = false;
        }
        if(m_MouseCoord.y < CenterScreen.y - Deadsone.y)
        {
            inDeadzone = false;
        }
        return inDeadzone;
    }

    private void OnGUI()
    {
        // Shows deadzone
        //GUI.Box(new Rect(CenterScreen.x - Deadsone.x, CenterScreen.y - Deadsone.y, Deadsone.x * 2, Deadsone.y * 2), "Deadzone");
    }
} 
