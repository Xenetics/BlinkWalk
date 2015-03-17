using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

// Used for all Main menu functionality and interaction with the game manager

public class MenuManager : MonoBehaviour 
{
    private float transitionSpeed = 2.0f;
    private bool transitioning = false;
    private bool transitioningIn = true;

    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Image mainBackdrop;
    [SerializeField]
    private Image instructionImage;
    private bool instructionsShowing = false;
    [SerializeField]
    private float resizeSpeed = 10f;

    [SerializeField]
    private Canvas challengesCanvas;
    [SerializeField]
    private Image challengeBackdrop;
    private Button[] lvlButtons;

    [SerializeField]
    private Canvas creditsCanvas;
    [SerializeField]
    private Image creditBackdrop;

    [SerializeField]
    private Canvas optionsCanvas;
    [SerializeField]
    private Image optionsBackdrop;
    [SerializeField]
    private Image soundOn;
    [SerializeField]
    private Image soundOff;

    [SerializeField]
    private Canvas highScoreCanvas;
    [SerializeField]
    private Image highscoreBackdrop;

    private Canvas currentCanvas;
    private Image currentBackdrop;
    private bool canvasSwapped = false;

    private string buttonPushed;

	void Start () 
    {
        currentCanvas = menuCanvas;
        currentBackdrop = mainBackdrop;
	}

	void Update () 
    {
        Transitions();

        if (currentCanvas == challengesCanvas)
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
        InstructionResize();
	}

    private void Transition(string button)  // swap enabled canvases
    {
        if (button == "play")
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
            Application.LoadLevel("game");
        }
        else if(button == "challenges")
        {
            //currentCanvas.gameObject.SetActive(false);
            //currentBackdrop = challengeBackdrop;
            //currentCanvas = challengesCanvas;
            //challengesCanvas.gameObject.SetActive(true);
            //canvasSwapped = true;
            //transitioningIn = true;
        }
        else if (button == "credits")
        {
            currentCanvas.gameObject.SetActive(false);
            currentBackdrop = creditBackdrop;
            currentCanvas = creditsCanvas;
            creditsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
            transitioningIn = true;
        }
        else if (button == "options")
        {
            currentCanvas.gameObject.SetActive(false);
            currentBackdrop = optionsBackdrop;
            currentCanvas = optionsCanvas;
            optionsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
            transitioningIn = true;
        }
        else if (button == "highscores")
        {
            currentCanvas.gameObject.SetActive(false);
            currentBackdrop = highscoreBackdrop;
            currentCanvas = highScoreCanvas;
            highScoreCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
            transitioningIn = true;
        }
        else if (button == "back")
        {
            currentCanvas.gameObject.SetActive(false);
            currentBackdrop = mainBackdrop;
            currentCanvas = menuCanvas;
            menuCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
            transitioningIn = true;
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

    public void InstructionButton()
    {
        if (!instructionsShowing)
        {
            instructionsShowing = true;
        }
        else
        {
            instructionsShowing = false;
        }
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
        Button[] potentialBtns = challengesCanvas.GetComponentsInChildren<Button>();

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

    private void InstructionResize()
    {
        if (!instructionsShowing)
        {
            instructionImage.color = Color.Lerp(instructionImage.color, new Color(instructionImage.color.r, instructionImage.color.g, instructionImage.color.b, 0), Time.deltaTime * resizeSpeed * 1.5f);
            instructionImage.rectTransform.sizeDelta = Vector2.Lerp(instructionImage.rectTransform.sizeDelta, new Vector2(1, 1), Time.deltaTime * resizeSpeed);
            instructionImage.rectTransform.localPosition = Vector3.Lerp(instructionImage.rectTransform.localPosition, new Vector3(640, -360, 0), Time.deltaTime * resizeSpeed);
        }
        else
        {
            instructionImage.color = Color.Lerp(instructionImage.color, new Color(instructionImage.color.r, instructionImage.color.g, instructionImage.color.b, 1), Time.deltaTime * resizeSpeed * 0.5f);
            instructionImage.rectTransform.sizeDelta = Vector2.Lerp(instructionImage.rectTransform.sizeDelta, new Vector2(300, 300), Time.deltaTime * resizeSpeed);
            instructionImage.rectTransform.localPosition = Vector3.Lerp(instructionImage.rectTransform.localPosition, new Vector3(490, -210, 0), Time.deltaTime * resizeSpeed);
        }
    }

    private void Transitions()
    {
        if (transitioning)
        {
            transitioningIn = false;
            // ADD TRANSITION HERE
            currentBackdrop.color = Color.Lerp(currentBackdrop.color, Color.white, Time.deltaTime * transitionSpeed);

            if (Vector4.Distance(currentBackdrop.color, Color.white) < 0.1f)
            {
                currentBackdrop.color = Color.white;

            }

            if (currentBackdrop.color == Color.white)
            {
                Transition(buttonPushed);
                transitioning = false;
                canvasSwapped = false;
            }
        }

        if (transitioningIn)
        {
            currentBackdrop.color = Color.Lerp(currentBackdrop.color, new Color(0.215f, 0.215f, 0.215f, 0.941f), Time.deltaTime * transitionSpeed);

            if(Vector4.Distance(currentBackdrop.color, new Color(0.215f, 0.215f, 0.215f, 0.941f)) < 0.05f)
            {
                currentBackdrop.color = new Color(0.215f, 0.215f, 0.215f, 0.941f);
                transitioningIn = false;
            }
        }
    }
}
