using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Managers
{
    public class SoundManager : MonoBehaviour {
    //    public List<AudioClip> backgroundMusic;
    //    public AudioSource bkgrdMusicPlayer;
    //    public AudioSource buttonClickSound;

    //    static AudioSource realBkgrdMusicPlayer;

    //    [Range(0, 100)]
    //    public float musicVolume = 100;
    //    [Range(0, 100)]
    //    public float sfxVolume = 100;

    //    [HideInInspector]
    //    public bool musicOn = true;
    //    [HideInInspector]
    //    public bool sfxOn = true;

    //    public bool randomizeBackGroundMusic = true;

    //    List<AudioSource> musicSourceList;
    //    List<AudioSource> sfxSourceList;

    //    float prevMusicVolume;
    //    float prevSfxVolume;
    //    int curSongNum = 100;

    //    public GameObject settingUIRef;
    //    // Use this for initialization
    //    void Start() {
    //        if (ImmortalGameManager.GM.firstStartedUp)
    //        {
    //            realBkgrdMusicPlayer = bkgrdMusicPlayer;
    //            TransitionToNextBackgroundMusic();
    //        }

    //        SetMusicActive(ImmortalGameManager.GM.MusicOn);
    //        SetSfxActive(ImmortalGameManager.GM.SfxOn);

    //        GameObject[] musicArr = GameObject.FindGameObjectsWithTag("MusicSound");
    //        GameObject[] sfxArr = GameObject.FindGameObjectsWithTag("SfxSound");

    //        musicSourceList = new List<AudioSource>();

    //        if (sfxSourceList == null)
    //        {
    //            sfxSourceList = new List<AudioSource>();
    //        }
            

    //        foreach (GameObject musicObj in musicArr)
    //        {
    //            AudioSource musicSource = musicObj.GetComponent<AudioSource>();
    //            musicSource.volume = musicVolume / 100;
    //            musicSource.mute = !musicOn;
    //            musicSourceList.Add(musicSource);
    //        }

    //        foreach (GameObject sfxObj in sfxArr)
    //        {
    //            AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
    //            sfxSource.volume = sfxVolume / 100;
    //            sfxSource.mute = !sfxOn;
    //            sfxSourceList.Add(sfxSource);
    //        }

    //        prevMusicVolume = musicVolume;
    //        prevSfxVolume = sfxVolume;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (!realBkgrdMusicPlayer.isPlaying)
    //        {
    //            TransitionToNextBackgroundMusic();
    //        }
    //        if (prevMusicVolume != musicVolume)
    //        {
    //            SetMusicVolume();
    //            prevMusicVolume = musicVolume;
    //        }

    //        if (prevSfxVolume != sfxVolume)
    //        {
    //            SetSfxVolume();
    //            prevSfxVolume = sfxVolume;
    //        }
    //    }
    //    public void AddToSFX(AudioSource sfxSource)
    //    {
    //        sfxSource.volume = sfxVolume / 100;
    //        sfxSource.mute = !sfxOn;
    //        if (sfxSourceList == null)
    //        {
    //            sfxSourceList = new List<AudioSource>();
    //        }
    //        sfxSourceList.Add(sfxSource);
    //    }

    //    public void TransitionToNextBackgroundMusic()
    //    {
    //        if (randomizeBackGroundMusic)
    //        {
    //            int newNum = -1;
    //            do
    //            {
    //                newNum = Random.Range(0, 400) % (backgroundMusic.Count - 1);
    //            } while (newNum == curSongNum);
    //            curSongNum = newNum;
    //            realBkgrdMusicPlayer.clip = backgroundMusic[curSongNum];

    //        }
    //        else
    //        {
    //            curSongNum++;
    //            if (curSongNum >= backgroundMusic.Count)
    //            {
    //                curSongNum = 0;
    //            }
    //            realBkgrdMusicPlayer.clip = backgroundMusic[curSongNum];
    //        }
    //        realBkgrdMusicPlayer.Play();
    //    }

    //    void SetMusicVolume()
    //    {
    //        foreach (AudioSource musicSource in musicSourceList)
    //        {
    //            musicSource.volume = musicVolume / 100;
    //        }
    //    }
    //    public void ToggleMusicVolume()
    //    {
    //        foreach (AudioSource musicSource in musicSourceList)
    //        {
    //            musicSource.Play(0);
    //            musicSource.mute = musicOn;
    //        }
    //        musicOn = !musicOn;
    //        ImmortalGameManager.GM.MusicOn = musicOn;
    //        settingUIRef.transform.GetChild(1).GetChild(0).gameObject.SetActive(!musicOn);
    //    }
    //    public void SetMusicActive(bool isActive)
    //    {
    //        //Debug.Log(isActive);
    //        musicOn = isActive;
    //        if (musicSourceList != null)
    //        {
    //            foreach (AudioSource musicSource in musicSourceList)
    //            {
    //                musicSource.Play(0);
    //                musicSource.mute = !isActive;
    //            }
    //        }
    //        ImmortalGameManager.GM.MusicOn = musicOn;
    //        settingUIRef.transform.GetChild(1).GetChild(0).gameObject.SetActive(!musicOn);
    //    }
    //    void SetSfxVolume()
    //    {
    //        foreach (AudioSource sfxSource in sfxSourceList)
    //        {
    //            sfxSource.volume = sfxVolume / 100;
    //        }
    //    }
    //    public void ToggleSfxVolume()
    //    {
    //        foreach (AudioSource sfxSource in sfxSourceList)
    //        {
    //            sfxSource.mute = sfxOn;
    //        }
    //        sfxOn = !sfxOn;
    //        ImmortalGameManager.GM.SfxOn = sfxOn;
    //        settingUIRef.transform.GetChild(2).GetChild(0).gameObject.SetActive(!sfxOn);
    //    }
    //    public void SetSfxActive(bool isActive)
    //    {
    //        sfxOn = isActive;
    //        if (sfxSourceList != null)
    //        {
    //            foreach (AudioSource sfxSource in sfxSourceList)
    //            {
    //                sfxSource.mute = !isActive;
    //            }
    //        }
    //        ImmortalGameManager.GM.SfxOn = sfxOn;
    //        settingUIRef.transform.GetChild(2).GetChild(0).gameObject.SetActive(!sfxOn);
    //    }

    //    public static void PlaySound(AudioClip clip)
    //    {
    //        GameObject soundPool = GameObject.FindGameObjectWithTag("SoundPool");

    //        AudioSource source = null;

    //        for (int i =0; i < soundPool.transform.childCount; i++)
    //        {
    //            GameObject child = soundPool.transform.GetChild(i).gameObject;
    //            if (!child.activeInHierarchy)
    //            {
    //                source = child.GetComponent<AudioSource>();
    //                break;
    //            }
    //        }

    //        if (source != null)
    //        {
    //            source.clip = clip;
    //            source.gameObject.SetActive(true);
    //            //source.Play(0);
    //        }

    //    }
    //    public static void PlaySound(AudioClip clip, Vector3 posistion)
    //    {
    //        GameObject soundPool = GameObject.FindGameObjectWithTag("SoundPool");

    //        AudioSource source = null;

    //        for (int i = 0; i < soundPool.transform.childCount; i++)
    //        {
    //            GameObject child = soundPool.transform.GetChild(i).gameObject;
    //            if (!child.activeInHierarchy)
    //            {
    //                source = child.GetComponent<AudioSource>();
    //                break;
    //            }
    //        }

    //        if (source != null)
    //        {
    //            source.clip = clip;
    //            source.transform.position = posistion;
    //            source.gameObject.SetActive(true);
    //            //source.Play(0);
    //        }

    //    }
    //    public void PlayButtonSound()
    //    {
    //        buttonClickSound.Play(0);
    //    }
    }
}