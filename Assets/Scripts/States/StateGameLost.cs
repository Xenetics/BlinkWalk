using UnityEngine;
using System.Collections;
public class StateGameLost : GameState 
{
	public StateGameLost(GameManager manager):base(manager){ }

    private float time;

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
        //GUILayout.Label("Lost");
        /*
        time += Time.deltaTime;
        if (time >= 8)
        {
            gameManager.NewGameState(gameManager.stateGameMenu);
            Application.LoadLevel("menu");
        }
        */
	}
}
