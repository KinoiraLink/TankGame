using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EngineAudio : MonoBehaviour
{
    public float minVolume = 0.05f, maxVolume = 0.1f;
    public float volumeIncrease = 0.01f;

    [SerializeField] private float currentVolume;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        currentVolume = minVolume;
    }

    private void Start()
    {
        _audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float speed)
    {
        if (speed > 0)
        {
            if (currentVolume < maxVolume)
                currentVolume += volumeIncrease * Time.deltaTime;
        }
        else
        {
            if (currentVolume > minVolume)
                currentVolume -= volumeIncrease * Time.deltaTime;
        }

        currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
        _audioSource.volume = currentVolume;
    }
}
