using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonDontDestroy<SoundManager>
{

    [Header("Audio Sources")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] BGMClipData[] bgmClips;
    [SerializeField] SFXClipData[] sfxClips;

    Dictionary<BGMType, AudioClip> bgmDict;
    Dictionary<SFXType, AudioClip> sfxDict;

    void Awake()
    {
        base.OnAwake();
        Init();
    }

    void Init()
    {
        bgmDict = new Dictionary<BGMType, AudioClip>();
        sfxDict = new Dictionary<SFXType, AudioClip>();

        foreach (var data in bgmClips)
            bgmDict[data.type] = data.clip;

        foreach (var data in sfxClips)
            sfxDict[data.type] = data.clip;
    }

    // ?? BGM
    public void PlayBGM(BGMType type)
    {
        if (!bgmDict.ContainsKey(type)) return;

        bgmSource.clip = bgmDict[type];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // ?? SFX
    public void PlaySFX(SFXType type)
    {
        if (!sfxDict.ContainsKey(type)) return;

        sfxSource.PlayOneShot(sfxDict[type]);
    }
}
