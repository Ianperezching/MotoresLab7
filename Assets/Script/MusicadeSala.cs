using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicadeSala : MonoBehaviour
{
    [SerializeField] private Sonidos _audioSettings;

    private AudioSource _audioSource;
    public AudioSource Musicadefondo;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_audioSource != null && _audioSettings != null && _audioSettings.SoundClip != null)
        {
            _audioSource.PlayOneShot(_audioSettings.SoundClip, _audioSettings.Volume);
        }
        Musicadefondo.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();

        }
        Musicadefondo.Play();
    }
}
