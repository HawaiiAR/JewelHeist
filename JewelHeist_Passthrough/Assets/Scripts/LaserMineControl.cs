using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControl;
using SceneUnderstanding;

namespace lasers
{
    public class LaserMineControl : MonoBehaviour
    {
        [SerializeField] private GameObject _laserBody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _distance;

        private Vector3 _min;
        private Vector3 _max;

        private DrawLaser _laser;
        private Rigidbody _rb;

        bool isPlaced = false;
        bool isMovingForward = true;

        // Start is called before the first frame update
        void Start()
        {
            GameController.StartGame += SetDifficulty;
           
        }

        private void OnEnable()
        {
          //  _gameController = FindObjectOfType<GameController>();
            _laser = GetComponentInChildren<DrawLaser>();
            _rb = this.GetComponent<Rigidbody>();
            _rb.isKinematic = false;
            isPlaced = false;
        }

        private void OnDisable()
        {
            GameController.StartGame -= SetDifficulty;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlaced)
            {
                MoveTarget();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // _rb.velocity = Vector3.zero;
            if (collision.gameObject.GetComponent<AttatchableSurface>())
            {
                _rb.isKinematic = true;

                this.transform.forward = collision.contacts[0].normal;
                this.transform.position = collision.contacts[0].point;
                _laser.laserActivated = true;
                

                _min = new Vector3(_laserBody.transform.localPosition.x - _distance, _laserBody.transform.localPosition.y, _laserBody.transform.localPosition.z);
                _max = new Vector3(_laserBody.transform.localPosition.x + _distance, _laserBody.transform.localPosition.y, _laserBody.transform.localPosition.z);

            }
        }

        private void SetDifficulty(string _difficulty)
        {
            switch (_difficulty)
            {
                case "easy":
                    break;
                case "hard":
                    isPlaced = true;
                    break;
                case "impossible":
                    isPlaced = true;
                    _laser.swingLaser = true;
                    break;
            }
     
        }

        private void MoveTarget()
        {

            if (isMovingForward)
            {
                float _distanceTraveled = Vector3.Distance(_laserBody.transform.localPosition, _max);
                if (_distanceTraveled > .01f)
                {
                    _laserBody.transform.localPosition = Vector3.Lerp(_laserBody.transform.localPosition, _max, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    isMovingForward = false;
                }
                //   _target.transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, transform.localPosition.x + _distance, _moveSpeed), transform.localPosition.y, transform.localPosition.z);
            }
            if (!isMovingForward)
            {
                float _distanceTraveled = Vector3.Distance(_laserBody.transform.localPosition, _min);

                if (_distanceTraveled > .01f)
                {
                    _laserBody.transform.localPosition = Vector3.Lerp(_laserBody.transform.localPosition, _min, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    isMovingForward = true;
                }
                // _target.transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, transform.localPosition.x - _distance, _moveSpeed), transform.localPosition.y, transform.localPosition.z);
            }
        }
    }
}
