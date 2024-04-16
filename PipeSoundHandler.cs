using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Modding;
using UnityEngine;
using SFCore;

namespace MetalPipeHM;
public class PipeSoundHandler : MonoBehaviour
{

    private AudioClip _metalpipeClip;
    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = HeroController.instance.gameObject.GetComponent<AudioSource>();
        _metalpipeClip = LoadAudioClip();
    }

    public void Run()
    {
        _audioSource.PlayOneShot(_metalpipeClip);
    }

    private AudioClip LoadAudioClip()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream s = assembly.GetManifestResourceStream("MetalPipeHM.Resources.metalpipe.wav");
        AudioClip clip = SFCore.Utils.WavUtils.ToAudioClip(s, "metalpipe");
        return clip;
    }


}

