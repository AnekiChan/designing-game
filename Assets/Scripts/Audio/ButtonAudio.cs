using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
		_audioSource = GetComponent<AudioSource>();
	}

    public void PlayButtonSound()
    {
        _audioSource.Play();
    }
}
