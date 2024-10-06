using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    [SerializeField] private Rigidbody _myRBD;
    [SerializeField] private float velocity;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Sonidos Sonido;


    private Vector2 _movement;
    private void Start()
    {
      if (_audioSource != null && Sonido != null)
        {
          _audioSource.clip = Sonido.SoundClip;
            _audioSource.volume = Sonido.Volume;
            _audioSource.loop = false;
        }
    }

    private void FixedUpdate()
    {
        _myRBD.velocity = new Vector3(_movement.x, _myRBD.velocity.y, _movement.y);

        if (_movement.magnitude > 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>() * velocity;
    }

}
