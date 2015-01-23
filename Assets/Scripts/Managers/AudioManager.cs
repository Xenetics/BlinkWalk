using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	private static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

        musicOn = true;
        soundOn = true;
        leaveState = false;
	}

    public GameObject[] MusicPlayer; // maked this an array to use multiple musics so its a more general sound manager.
    private GameObject[] Musics;
    public AudioClip[] Sounds;
    public bool musicOn { get; set; }
    public bool soundOn { get; set; }
    public bool leaveState { get; set; }
     
	void Start () 
	{
        Musics = new GameObject[MusicPlayer.Length];
        FillMusics();
	}
	
	void Update () 
	{
        ApplyMute();
	}

    void LateUpdate()
    {

    }

	public void PlaySound(string toPlay)
	{
        for(int i = 0; i < Sounds.Length; ++i)
        {
            if(soundOn && toPlay == Sounds[i].name)
            {
                AudioSource.PlayClipAtPoint(Sounds[i], Camera.main.transform.position);
            }
        }
	}

    private void ApplyMute()
    {
        for(int i = 0; i < Musics.Length; ++i)
        {
            string tempName = Application.loadedLevelName;
            if (musicOn && tempName == Musics[i].gameObject.name)
            {
                Musics[i].audio.mute = false;
                if (!Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Play();
                }
            }
            else
            {
                Musics[i].transform.parent = gameObject.transform;
                Musics[i].audio.mute = true;
                if (Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Stop();
                }
            }
        }
    }
    
    private void FillMusics()
    {
        for(int i = 0; i < MusicPlayer .Length; ++i)
        {
            GameObject add = (GameObject)Instantiate(MusicPlayer[i], Camera.main.transform.position, Quaternion.identity);
            add.name = MusicPlayer[i].name;
            Musics[i] = add;
            Musics[i].transform.parent = gameObject.transform;
            Musics[i].audio.mute = true;
        }
    }
    
    public void ToggleMusic(bool toggle)
    {
        musicOn = toggle;
    }

    public void ToggleSound(bool toggle)
    {
        soundOn = toggle;
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }

    public bool IsSoundOn()
    {
        return soundOn;
    }
}
