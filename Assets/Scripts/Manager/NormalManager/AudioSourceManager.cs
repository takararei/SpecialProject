using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager
{
    private AudioSource[] audioSource;//0.背景，1.特效
    private AudioSource asBG;
    private AudioSource asEffect;
    private bool playEffectMusic = true;
    private bool playBGmusic = true;

    public AudioSourceManager()
    {
        audioSource = GameManager.Instance.GetComponents<AudioSource>();
        asBG = audioSource[0];
        asEffect = audioSource[1];
    }
    public void PlayBGMusic(AudioClip audioClip)
    {
        //不在播放，或者音频不同
        if (!asBG.isPlaying || asBG.clip != audioClip)
        {
            asBG.clip = audioClip;
            asBG.Play();
        }
    }

    public void PlayEffectMusic(AudioClip audioClip)
    {
        if (playEffectMusic)
        {
            asEffect.PlayOneShot(audioClip);
        }
    }

    public void CloseEffectMusic()
    {

    }

    public void CloseBGMusic()
    {
        asBG.Stop();
    }

    public void OpenBGMusic()
    {
        asBG.Play();
    }
}
