using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _fireSpeed;

    [SerializeField] private float _time;
    [SerializeField] private float _rotInterval;

    private int _dirX;
    private int _dirY;
    private int _dirZ;

    public bool startSpinning;
    bool _switchDirections;

    // Start is called before the first frame update
    void Start()
    {
        _switchDirections = true;
        startSpinning = false;
     //  NewRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpinning)
        {
            _time += Time.deltaTime;

            if (_time >= _rotInterval)
            {
                _time = 0;
                NewRotation();
            }

            this.transform.Rotate(new Vector3(_dirX * _speed * Time.deltaTime, _dirY * _speed * Time.deltaTime, _dirZ * _speed * Time.deltaTime), Space.Self);
        }
    }

    private void NewRotation()
   {
        _switchDirections = !_switchDirections;

        if (_switchDirections)
        {      
            _dirY = -180;    
        }
        else
        {
            _dirY = 180;
        }

        _dirX = Random.Range(0, 360);
        _dirZ = Random.Range(0, 360);
    }
}
