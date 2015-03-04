using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateGamePlaying : GameState 
{
	
	public StateGamePlaying(GameManager manager):base(manager){	}
	
	public override void OnStateEntered()
    {
        Cursor.visible = false;
    }

	public override void OnStateExit()
    {
        
    }
	
	public override void StateUpdate() 
	{

	}
	
	public override void StateGUI() 
	{
        //GUILayout.Label("Playing");
	}
}