using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAndFollow : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _subMenu;
    [SerializeField] private GameObject _mainCamera;
  
    
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
     
       this.transform.position =  _mainCamera.transform.position + _mainCamera.transform.forward * .5f;

        float _distance = Vector3.Distance(_menu.transform.position, this.transform.position);
       
        if (_distance > .1)
        {
            _menu.transform.position = Vector3.Lerp(_menu.transform.position, this.transform.position, _speed * Time.deltaTime);
        }

         Vector3 _direction =  _subMenu.transform.position - _mainCamera.transform.position;
        _direction.y = 0;
        _subMenu.transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);

    }
}
