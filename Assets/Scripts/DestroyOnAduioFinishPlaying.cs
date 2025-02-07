using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAduioFinishPlaying : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitCoroutine());
    }
    
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(gameObject);
    }
}
