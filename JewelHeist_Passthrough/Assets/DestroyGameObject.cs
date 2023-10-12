using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControl;

public class DestroyGameObject : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnEnable()
    {

        GameController.ResetGame -= Destroy;
    }

    private void OnDisable()
    {
        GameController.ResetGame += Destroy;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
