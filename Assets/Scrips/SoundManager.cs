using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [Header("BGM")]
    public AudioClip[] bgmcilp;
    public float bgmVolume;
    AudioSource bgmplayer;

    [Header("SFX")]
    public AudioClip[] sfxcilp;
    public float sfxVolume;
    public int channerls;
    AudioSource[] sfxplayer;
    int channelIndex;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmplayer = bgmObject.AddComponent<AudioSource>();
        bgmplayer.playOnAwake = false;
        bgmplayer.volume = bgmVolume;
        bgmplayer.clip = bgmcilp[0];

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxplayer = new AudioSource[channerls];

        for(int index =0; index< sfxplayer.Length; index++)
        {
            sfxplayer[index]= sfxObject.AddComponent<AudioSource>();
            sfxplayer[index].playOnAwake = false;
            sfxplayer[index].volume = bgmVolume;
        }
    }
}