using UnityEngine;
using System.Collections;

public class RaycastMouse : MonoBehaviour 
{
	public Texture2D iconArrow;
	public Vector2 arrowRegPoint;
	public Texture2D iconClick;
	public Vector2 clickRegPoint;
	private Vector2 mouseReg;
	private Vector2 mouseCoord;
	private Texture mouseTex;

    public Camera cam;
	
	void OnDisable()
	{
		
	}
	
    void Update () 
	{
		
	}

    void OnGUI()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //determine what we hit.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.tag)
                {
                    case "Tile":
                        //mouseTex = iconClick;
                        //mouseReg = clickRegPoint;
                        Debug.Log("Yarrr we gots a Tile Matey!");
                        break;
                }
            }
            else
            {
                //mouseTex = iconArrow;
                //mouseReg = arrowRegPoint;
            }

            //GUI.depth = 0;
            //update texture object.
            //mouseCoord = Input.mousePosition;
            //GUI.DrawTexture( new Rect(mouseCoord.x-mouseReg.x, Screen.height-mouseCoord.y - mouseReg.y, mouseTex.width, mouseTex.height), mouseTex, ScaleMode.StretchToFill, true, 10.0f);
        }
    }
} 
