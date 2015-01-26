using UnityEngine;
using System.Collections;

public class StateGameIntro : GameState
{
    public StateGameIntro(GameManager manager) : base(manager) { }

    float time;
    private bool riffPlaying = false;

    public override void OnStateEntered()
    {

    }
    public override void OnStateExit()
    {
        
    }
    public override void StateUpdate()
    {
        time += Time.deltaTime;
        if (!riffPlaying && time > 2)
        {
            AudioManager.Instance.PlaySound("guitarIntro");
            riffPlaying = true;
        }
        if (time >= 6)
        {
            gameManager.NewGameState(gameManager.stateGameMenu);
            Application.LoadLevel("menu");
        }
    }
    public override void StateGUI()
    {
        GUILayout.Label("Intro");
    }
}