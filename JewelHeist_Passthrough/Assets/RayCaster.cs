using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    [SerializeField] private Transform _rayStart;
    [SerializeField] private float _rayDistance;

   

    bool _lookForPodium;

    // Start is called before the first frame update
    void Start()
    {
       
        _lookForPodium = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lookForPodium)
        {
            Ray _ray = new Ray(_rayStart.transform.position, _rayStart.forward);
            Debug.DrawRay(_rayStart.transform.position, _rayStart.forward,  Color.red, _rayDistance);
            Debug.Log("drawing ray");
            RaycastHit _hit;
            if(Physics.Raycast(_ray, out _hit, _rayDistance))
            {
                if (_hit.collider.gameObject.TryGetComponent(out PodiumControl _podiumControl))
                {
                    _podiumControl.PlayTimeline();
                    _lookForPodium = false;
                }
            }

          
        }
    }
}
