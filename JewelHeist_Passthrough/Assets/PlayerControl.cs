using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    public static Action<bool> PlayerTagable;
    public static Action<bool> PlayerSafe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
         
            Debug.Log("player in safe");
        }
    }

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
