using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets GetInstance() => instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform pipeHead;
    public Transform pipeBody;

    public SoundAudioClip[] soundAudioClips;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
