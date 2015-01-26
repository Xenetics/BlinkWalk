using UnityEngine;
using System.Collections;
public class StateGameLost : GameState 
{
	public StateGameLost(GameManager manager):base(manager){ }

    private float time;
    private bool laughPlaying = false;

	public override void OnStateEntered()
    {

    }
	public override void OnStateExit()
    {

    }
	public override void StateUpdate() {}
	public override void StateGUI() 
	{
        GUILayout.Label("Lost");
        /*
        time += Time.deltaTime;
        if (time >= 8)
        {
            AudioManager.Instance.leaveState = true;
            gameManager.NewGameState(gameManager.stateGameMenu);
            Application.LoadLevel("menu");
        }
        */
	}
}
