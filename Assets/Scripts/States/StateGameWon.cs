using UnityEngine;
using System.Collections;
public class StateGameWon : GameState 
{
	public StateGameWon(GameManager manager):base(manager) { }

    float time;

	public override void OnStateEntered()
    {
        Cursor.visible = true;
    }
	public override void OnStateExit()
    {
        
    }
	public override void StateUpdate() {}
	public override void StateGUI() 
	{
        //GUILayout.Label("Won");
        /*
        time += Time.deltaTime;
        if (time >= 48)
        {
            gameManager.NewGameState(gameManager.stateGameMenu);
            Application.LoadLevel("menu");
        }
        */
	}
}