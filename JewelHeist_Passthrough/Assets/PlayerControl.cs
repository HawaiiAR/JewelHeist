using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    public static Action<bool> PlayerTagable;
    public static Action<bool> PlayerSafe;


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("SafeZone"))
        {
            PlayerTagable?.Invoke(true);
            Debug.Log("player in danger");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("SafeZone"))
        {
            PlayerSafe?.Invoke(false);
            Debug.Log("player in safe");
        }
    }
}
