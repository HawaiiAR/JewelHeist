using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using GameControl;

public class ActivateGravity : MonoBehaviour
{
   

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        GameController.StartGame += ActivateRigidbodyGravity;

       _rb = this.GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        GameController.StartGame -= ActivateRigidbodyGravity;
    }



    public void DeactivateGravity()
    {
        RigidBodytState(true, false);
    }

    public void ActivateRigidbodyGravity(string gameStarted)
    {
        Debug.Log("KinematicOFf");
        _rb.isKinematic = false;
      //  RigidBodytState(false, true);
    }

    private void RigidBodytState(bool _kinimatic, bool _gravity)
    {
        _rb.isKinematic = _kinimatic;
        _rb.useGravity = _gravity;
    }

    /* private void OnCollisionEnter(Collision collision)
 {
     if (collision.gameObject.CompareTag("Hand"))
     {
         Debug.Log("Hit hand");
         RigidBodytState(true, false);

     }
 }*/

}
