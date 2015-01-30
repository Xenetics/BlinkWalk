using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour 
{
    private static InGameUIManager instance = null;
    public static InGameUIManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        maxBarLength = visionBar.rectTransform.sizeDelta.x;
        originalBarPos = visionBar.rectTransform.localPosition.x;
    }

    public bool paused { get; set; }
    [SerializeField]
    private Canvas UICanvas;
    [SerializeField]
    private Image visionBar;
    private float maxBarLength;
    private float originalBarPos;
    [SerializeField]
    private Text timerText;
    private float time = 0f;

    [SerializeField]
    private Canvas PausedCanvas;

    [SerializeField]
    private Canvas GameOverCanvas;

    [SerializeField]
    private Canvas InstructionCanvas;
    [SerializeField]
    private Image InstructionImage;
    private bool tutDone = false;

	void Start () 
    {

	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Paused(false);
            }
            else
            {
                Paused(true);
            }
        }

        if (!paused)
        {
            /*
            if (tutDone == false)
            {
                if (time > 5)
                {
                    InstructionImage.color = new Color(InstructionImage.color.r, InstructionImage.color.g, InstructionImage.color.b, InstructionImage.color.a - Time.deltaTime);
                }
                InstructionCanvas.gameObject.SetActive(true);
                if (InstructionImage.color.a < 0)
                {
                    tutDone = true;
                }
            }
            else
            {
                InstructionImage.color = new Color(InstructionImage.color.r, InstructionImage.color.g, InstructionImage.color.b, 255);
                InstructionCanvas.gameObject.SetActive(false);

            }
            */

            if (GameManager.WhatState() == "playing")
            {
                
            }
            Timer(); // move into the above IF
            VisionBar();
        }
	}

    private void VisionBar()
    {
        float scale = RunnerPlayerController.Instance.VisionScale();

        Vector3 newLength = new Vector3(maxBarLength * scale, visionBar.rectTransform.sizeDelta.y, 1);
        visionBar.rectTransform.sizeDelta = newLength;
        Vector3 newPosition = new Vector3(originalBarPos - (maxBarLength - visionBar.rectTransform.sizeDelta.x) * 0.5f, visionBar.rectTransform.localPosition.y, 0);
        visionBar.rectTransform.localPosition = newPosition;
    }

    private void Timer()
    {
        time += Time.deltaTime;

        int temp = Mathf.FloorToInt(time);

        int tempMinute = Mathf.FloorToInt(time / 60);
        int tempSecond;
        if (time > 60)
        {
            tempSecond = Mathf.FloorToInt(time % (tempMinute * 60));
        }
        else if (time < 60)
        {
            tempSecond = Mathf.FloorToInt(time);
        }
        else
        {
            tempSecond = 00;
        }

        string minute;
        string second;

        if (tempMinute < 10)
        {
            minute = "0" + tempMinute;
        }
        else
        {
            minute = tempMinute.ToString();
        }

        if (tempSecond < 10)
        {
            second = "0" + tempSecond;
        }
        else
        {
            second = tempSecond.ToString();
        }

        timerText.text = minute + ":" + second;
    }

    public void Paused(bool isPaused)
    {
        if (isPaused)
        {
            paused = true;

            UICanvas.gameObject.SetActive(false);
            PausedCanvas.gameObject.SetActive(true);
        }
        else
        {
            paused = false;

            PausedCanvas.gameObject.SetActive(false);
            UICanvas.gameObject.SetActive(true);
        }
    }

    public void Reset()
    {
        AudioManager.Instance.PlaySound("button");
        Paused(false);
        time = 0;
        GameOverCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySound("button");
        Paused(false);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGameMenu);
        Application.LoadLevel("menu");
    }

    public void EndGame()
    {
        GameManager.Instance.NewGameState(GameManager.Instance.stateGameLost);
        Application.LoadLevel("menu");
    }

    private void TallyScore()
    {
        
    }

    private void SaveScore()
    {

    }
}
