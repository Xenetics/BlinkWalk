using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

// Used for all Main menu functionality and interaction with the game manager

public class MenuManager : MonoBehaviour 
{
    [SerializeField]
    private GameObject backdrop;
    [SerializeField]
    private float transitionSpeed = 10.0f;
    [SerializeField]
    private float transitionTimer = 2.0f;
    private float transitionTimerActual;
    private bool transitioning = false;

    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private Canvas levelsCanvas;
    private Button[] lvlButtons;

    [SerializeField]
    private Canvas creditsCanvas;

    [SerializeField]
    private Canvas optionsCanvas;
    [SerializeField]
    private Image soundOn;
    [SerializeField]
    private Image soundOff;

    [SerializeField]
    private Canvas highScoreCanvas;

    private Canvas currentCanvas;
    private bool canvasSwapped = false;

    private string buttonPushed;

	void Start () 
    {
        currentCanvas = menuCanvas;
        transitionTimerActual = transitionTimer;
	}

	void Update () 
    {
	    if(transitioning)
        {
            // ADD TRANSITION HERE
            transitionTimerActual -= Time.deltaTime;

            if (transitionTimerActual <= 0.7f && canvasSwapped == false)
            {
                Transition(buttonPushed);
            }

            if (transitionTimerActual <= 0)
            {
                // RESET TRANSITION HERE
                transitioning = false;
                transitionTimerActual = transitionTimer;
                canvasSwapped = false;
            }
        }

        if(currentCanvas == levelsCanvas)
        {
            for(int i = 0; i < lvlButtons.Length; ++i)
            {
                if(i < PlayerPrefs.GetInt("level") || i == 0)
                {
                    lvlButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    lvlButtons[i].gameObject.SetActive(false);
                }
            }
        }
        SoundImage();
	}

    private void Transition(string button)  // swap enabled canvases
    {
        if (button == "play")
        {
            //currentCanvas.gameObject.SetActive(false);
            //currentCanvas = levelsCanvas;
            //levelsCanvas.gameObject.SetActive(true);
            //canvasSwapped = true;
            Application.LoadLevel("game");
        }
        else if (button == "credits")
        {
            currentCanvas.gameObject.SetActive(false);
            currentCanvas = creditsCanvas;
            creditsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if (button == "options")
        {
            currentCanvas.gameObject.SetActive(false);
            currentCanvas = optionsCanvas;
            optionsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if (button == "highscores")
        {
            currentCanvas.gameObject.SetActive(false);
            currentCanvas = highScoreCanvas;
            highScoreCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if (button == "back")
        {
            currentCanvas.gameObject.SetActive(false);
            currentCanvas = menuCanvas;
            menuCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if(button == "lvl")
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
            Application.LoadLevel("chellenge");
        }
    }

    public void PlayButton()
    {
        AudioManager.Instance.PlaySound("button");
        buttonPushed = "play";
        transitioning = true;
    }

    public void CreditButton()
    {
        AudioManager.Instance.PlaySound("button");
        buttonPushed = "credits";
        transitioning = true;
    }

    public void OptionsButton()
    {
        AudioManager.Instance.PlaySound("button");
        buttonPushed = "options";
        transitioning = true;
    }

    public void HighScoreButton()
    {
        AudioManager.Instance.PlaySound("button");
        buttonPushed = "highscores";
        transitioning = true;
    }

    public void BackButton()
    {
        AudioManager.Instance.PlaySound("button");
        buttonPushed = "back";
        transitioning = true;
    }

    public void SoundToggle()
    {
        AudioManager.Instance.PlaySound("button");
        AudioManager.Instance.ToggleMusic(!AudioManager.Instance.IsMusicOn());
        AudioManager.Instance.ToggleSound(!AudioManager.Instance.IsSoundOn());
    }

    public void LevelButton(int btn)
    {
        for (int i = 1; i <= lvlButtons.Length; ++i)
        {
            if(i == btn)
            {
                AudioManager.Instance.PlaySound("button");
                buttonPushed = "lvl";
                //LevelManager.Instance.levelChosen = i;
                transitioning = true;
            }
        }
    }

    private void GetLvlButtons()
    {
        Button[] potentialBtns = levelsCanvas.GetComponentsInChildren<Button>();

        lvlButtons = new Button[(potentialBtns.Length) - 1];

        for (int i = 0; i < potentialBtns.Length; ++i)
        {
            if (potentialBtns[i].name[0] == 'l')
            {
                lvlButtons[i] = potentialBtns[i];
            }
        }
    }

    private void SoundImage()
    {
        if(AudioManager.Instance.IsMusicOn())
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
        }
        else
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
        }
    }
}
