using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace lasers
{
    public class DrawLaser : MonoBehaviour
    {
        public static Action HitPlayer;

        public bool laserActivated = false;
        public bool swingLaser = false;

        [SerializeField] private float _rayDistance;
        [SerializeField] private LineRenderer _laser;
        [SerializeField] private float _laserWidth;
        [SerializeField] private Transform _laserFirePoint;

        [SerializeField] private float _rotAngle;
        [SerializeField] private float _rotOffset;
        [SerializeField] private float _rotSpeed;
        [SerializeField] private float _distance;
        [SerializeField] private float _moveSpeed;

       // [SerializeField] private GameObject _target;

        private Vector3 _laserStart;
        private Vector3 _laserEnd;
        Vector3 _targetMin;
        Vector3 _targetMax;

        bool _canHitPlayer;
       

        // Start is called before the first frame update
        void Start()
        {
            PlayerControl.PlayerSafe += LaserArmedState;
            PlayerControl.PlayerTagable += LaserArmedState;

            _laser = this.GetComponent<LineRenderer>();
            _canHitPlayer = false;
        }

        private void OnEnable()
        {

            PlayerControl.PlayerSafe -= LaserArmedState;
            PlayerControl.PlayerTagable -= LaserArmedState;

            _rotSpeed = UnityEngine.Random.Range(3, 5);
        }

        // Update is called once per frame
        void Update()
        {
            if (laserActivated)
            {
                ActivateLaser();               
            }
            if (swingLaser)
            {
                SwingLasers();
            }
        }

        private void SwingLasers()
        {
            transform.localEulerAngles = new Vector3(Mathf.PingPong(_rotSpeed * Time.timeSinceLevelLoad, _rotAngle) - _rotOffset, 0, 0);
        }

        private void ActivateLaser()
        {
            Vector3 rayDirection = _laserFirePoint.transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            Ray ray = new Ray(_laserFirePoint.transform.position, rayDirection);
    

            if (Physics.Raycast(ray, out hit, _rayDistance))
            {
                _laserEnd = hit.point;
           
                if (_canHitPlayer)
                {
                    if (hit.collider.CompareTag("MainCamera"))
                    {
                        HitPlayer?.Invoke();
                    }
                }

            }
            else
            {
                _laserEnd = _laserFirePoint.transform.forward * _rayDistance;
            }

            DrawLaserLine(_laserEnd);
        }

        private void DrawLaserLine(Vector3 _end)
        {
            _laser.startWidth = _laserWidth;
            _laserStart = _laserFirePoint.transform.position;
            _laser.positionCount = 2;
            _laser.SetPosition(0, _laserStart);
            _laser.SetPosition(1, _end);
        
        }

        private void LaserArmedState(bool armedState)
        {
            Debug.Log("armed state");
            _canHitPlayer = armedState;
        }
    }
}
