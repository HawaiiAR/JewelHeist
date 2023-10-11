using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ActivateGravity : MonoBehaviour
{
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hit hand");
            RigidBodytState(true, false);

        }
    }

   /* private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hit hand");
            RigidBodytState(false, true);
           
        }
    }
   */

    public void DeactivateGravity()
    {
        RigidBodytState(true, false);
    }

    public void AtivateGravity()
    {
        RigidBodytState(false, true);
    }

    private void RigidBodytState(bool _kinimatic, bool _gravity)
    {
        _rb.isKinematic = _kinimatic;
        _rb.useGravity = _gravity;
    }
}
