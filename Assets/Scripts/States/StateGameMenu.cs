using UnityEngine;
using System.Collections;

public class StateGameMenu : GameState
{
    private bool credits = false;
    private bool options = false;
    bool toggleSound;
    string message;
    public StateGameMenu(GameManager manager) : base(manager) { }

    public override void OnStateEntered() 
    {
        toggleSound = AudioManager.Instance.IsMusicOn();
    }
    public override void OnStateExit() 
    {

    }
    public override void StateUpdate() 
    {

    }
    public override void StateGUI()
    {
        //GUILayout.Label("Menu");
        /*
        GUI.skin = GuiManager.GetSkin();
        GUI.depth = 1;
        if (!credits && !options)
        {
            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.4f));
                GUILayout.Box("Shipwrecker");
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - Screen.width * 0.1f, Screen.height * 0.60f, Screen.width * 0.20f, Screen.height * 0.5f));
                if (GUILayout.Button("New Game"))
                {
                    AudioManager.Instance.PlaySound("button");
                    gameManager.NewGameState(gameManager.stateGamePlaying);
                    Application.LoadLevel("game");
                }

                if (GUILayout.Button("Options"))
                {
                    AudioManager.Instance.PlaySound("button");
                    options = true;
                }

                if (GUILayout.Button("Credits"))
                {
                    AudioManager.Instance.PlaySound("button");
                    credits = true;
                }
            GUILayout.EndArea();
        }
        else if(options)
        {
            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - Screen.width * 0.07f, Screen.height * 0.50f, Screen.width * 0.15f, Screen.height * 0.1f));
                if (toggleSound)
                {
                    message = "Music ON";
                }
                else
                {
                    message = "Music Off";
                }

                toggleSound = GUILayout.Toggle(toggleSound, message);

                AudioManager.Instance.ToggleMusic(toggleSound);
                
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width * 0.1f - Screen.width * 0.05f, Screen.height - Screen.height * 0.10f, Screen.width * 0.1f, Screen.height * 0.5f));
                if (GUILayout.Button("Back"))
                {
                    AudioManager.Instance.PlaySound("button");
                    options = false;
                }
            GUILayout.EndArea();
        }
        else if(credits)
        {
            GUILayout.BeginArea(new Rect(Screen.width * 0.5f - Screen.width * 0.4f, Screen.height * 0.5f - Screen.height * 0.2f, Screen.width * 0.8f, Screen.height * 0.8f));
                GUILayout.Label("Everything you see before you \n ~Rory O'Connor~ \n ------------------------------------------------ \n Insert more here");
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width * 0.1f - Screen.width * 0.05f, Screen.height - Screen.height * 0.10f, Screen.width * 0.1f, Screen.height * 0.5f));
                if (GUILayout.Button("Back"))
                {
                    AudioManager.Instance.PlaySound("button");
                    credits = false;
                }
            GUILayout.EndArea();
        }
         **/
    }
}
