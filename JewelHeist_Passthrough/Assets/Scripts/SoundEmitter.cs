using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameControl;

public class SoundEmitter : MonoBehaviour
{
    public static Action TooLoud;

    bool _canSoundAlarm;
    Rigidbody _rb;
    private float _velocityAmount;

    private void Start()
    {
       
        GameController.StartGame += BoxIsArmed;

        _canSoundAlarm = false;
    }

    private void OnDisable()
    {
        GameController.StartGame -= BoxIsArmed;
    }

    private void BoxIsArmed(string _difficulty)
    {
        Debug.Log("Armed");
        _canSoundAlarm = true;
        _rb = this.GetComponent<Rigidbody>();
        _rb.isKinematic = false;


        switch (_difficulty)
        {
            case "easy":
                _velocityAmount = 3;
                break;
            case "hard":
                _velocityAmount = 2;
                break;
            case "impossible":
                _velocityAmount = 1f;
                break;


        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerControl>(out PlayerControl _player)) return;

        if (_canSoundAlarm)
        {
            if (_rb.velocity.magnitude >= _velocityAmount)
            {
                TooLoud?.Invoke();
            }
        }
    }
}
