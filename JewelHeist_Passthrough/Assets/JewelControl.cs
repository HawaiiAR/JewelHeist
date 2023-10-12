using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JewelControl : MonoBehaviour
{
    public static Action<string> PlayerWon;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            Debug.Log("Player Wins");
            PlayerWon?.Invoke("win");
        }
    }
}
