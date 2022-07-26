using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public AudioSource[] _audioSources;
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
}
