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
    [SerializeField] float _velocityAmount;

    private void Start()
    {
        GameController.StartGame += BoxIsArmed;

        _canSoundAlarm = false;
    }

    private void OnDisable()
    {
        GameController.StartGame -= BoxIsArmed;
    }

    private void BoxIsArmed(string gameStarted)
    {
        Debug.Log("Armed");
        _canSoundAlarm = true;
        _rb = this.GetComponent<Rigidbody>();
        _rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canSoundAlarm)
        {
            if (_rb.velocity.magnitude >= _velocityAmount)
            {
                TooLoud?.Invoke();
            }
        }
    }
}
