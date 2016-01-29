using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        musicOn = true;
        soundOn = true;
    }
    [SerializeField]
    private List<GameObject> m_MusicPlayer = new List<GameObject>();
    [SerializeField]
    private List<AudioClip> m_Sounds = new List<AudioClip>();
    public bool musicOn { get; set; }
    public bool soundOn { get; set; }
	
	void Update () 
	{
        ApplyMute();
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
                gameObject.transform.SetParent(m_MusicPlayer[i].transform.parent);
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
