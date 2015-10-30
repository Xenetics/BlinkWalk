using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> 
{
	protected AudioManager() { }

    [SerializeField]
    private List<GameObject> m_MusicPlayer; // maked this an array to use multiple musics so its a more general sound manager.
    [SerializeField]
    private List<AudioClip> m_Sounds;
    public bool musicOn { get; set; }
    public bool soundOn { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musicOn = true;
        soundOn = true;
    }

    void Start () 
	{
        m_MusicPlayer = new List<GameObject>();
        m_Sounds = new List<AudioClip>();
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
        for(int i = 0; i < m_Sounds.Count; ++i)
        {
            if(soundOn && toPlay == m_Sounds[i].name)
            {
                AudioSource.PlayClipAtPoint(m_Sounds[i], Camera.main.transform.position);
            }
        }
	}

    private void ApplyMute()
    {
        for(int i = 0; i < m_MusicPlayer.Count; ++i)
        {
            string tempName = Application.loadedLevelName;
            if (musicOn && tempName == m_MusicPlayer[i].gameObject.name)
            {
                m_MusicPlayer[i].GetComponent<AudioSource>().mute = false;
                if (!m_MusicPlayer[i].GetComponent<AudioSource>().isPlaying)
                {
                    m_MusicPlayer[i].GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                m_MusicPlayer[i].transform.parent = gameObject.transform;
                m_MusicPlayer[i].GetComponent<AudioSource>().mute = true;
                if (m_MusicPlayer[i].GetComponent<AudioSource>().isPlaying)
                {
                    m_MusicPlayer[i].GetComponent<AudioSource>().Stop();
                }
            }
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
