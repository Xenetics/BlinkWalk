using UnityEngine;
using System.Collections;

public class StateGameMenu : GameState
{
    public StateGameMenu(GameManager manager) : base(manager) { }

    public override void OnStateEntered() 
    {
        Screen.showCursor = true;
    }
    public override void OnStateExit() 
    {

    }
    public override void StateUpdate() 
    {

    }
    public override void StateGUI()
    {
        GUILayout.Label("Menu");
    }
}
