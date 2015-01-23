using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateGamePlaying : GameState 
{
	
	public StateGamePlaying(GameManager manager):base(manager){	}
	
	public override void OnStateEntered()
    {
        
    }

	public override void OnStateExit()
    {
        
    }
	
	public override void StateUpdate() 
	{
        Screen.showCursor = true;
	}
	
	public override void StateGUI() 
	{
        //GUILayout.Label("Playing");

        /*
        GUI.skin = GuiManager.GetSkin();

		if(isPaused)
		{
            GameObject pointer = GameObject.Find("Pointer");
            pointer.GetComponentsInChildren(typeof(Transform), true);
            
			string[] names = QualitySettings.names;
			string message = "Game Paused. \n Press ESC to resume or select a new quality setting below.";
            GUILayout.BeginArea(new Rect(Screen.width * 0.01f, Screen.height * 0.05f, Screen.width * 0.98f, Screen.height * 0.3f));
                GUILayout.Box(message);
            GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
            
			
			for (int i = 0; i < names.Length; i++) 
			{
				if (GUILayout.Button(names[i],GUILayout.Width(200)))
				{
					QualitySettings.SetQualityLevel(i,true);
				}
			}
			
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndArea();
		}
        else
        {

        }
        */
	}
}