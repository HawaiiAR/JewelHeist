using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControl;

namespace lasers
{
    public class FireMines : MonoBehaviour
    {
        public static Action LasersSet;

        [SerializeField] private GameObject _mine;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private float _fireInterval;
        [SerializeField] private float _fireSpeed;

        [SerializeField] private int[] _laserCount;
        [SerializeField] private RandomRotator _rotator;

        public List<int> _faultiMines = new List<int>();
        public List<int> _activeiMines = new List<int>();
        int _difficultyLevel;

        private float _time;
        private bool _startGame;


        // Start is called before the first frame update
        void Start()
        {
            _startGame = false;

            GameController.DifficultyLevel += SetLevel;
        }

        private void OnDisable()
        {
            GameController.DifficultyLevel -= SetLevel;
        }

        private void FireMine()
        {

            GameObject _projectileMine = Instantiate(_mine, _firePoint.transform.position, _firePoint.transform.rotation);
            Rigidbody _rb = _projectileMine.GetComponent<Rigidbody>();
            _rb.AddForce(_firePoint.transform.forward * _fireSpeed, ForceMode.Impulse);
            _rotator.GenerateRandomRotation();
        }

        //laser count is set in inspector
        private void SetLevel(string _dificulty)
        {
            Debug.Log("start shooting mines");
            switch (_dificulty)
            {
                case "easy":
                    StartCoroutine(SetUpLevel(_laserCount[0]));
                  
                    _difficultyLevel = _laserCount[0];
                    break;
                case "hard":
                    StartCoroutine(SetUpLevel(_laserCount[1]));
                   
                    _difficultyLevel = _laserCount[1];
                    break;
                case "impossible":
                    StartCoroutine(SetUpLevel(_laserCount[2]));
                 
                    _difficultyLevel = _laserCount[2];
                    break;
            }


        }

        //fires number of mines based on difficulty
        IEnumerator SetUpLevel(int _lasersToPlace)
        {
            _rotator.startSpinning = true;
            for (int i = 0; i < _lasersToPlace; i++)
            {
                yield return new WaitForSeconds(_fireInterval);
                FireMine();
            }

            CheckFaultyMines();
        }

        private void CheckFaultyMines()
        {

            Debug.Log("check mines");
            if (_activeiMines.Count < _difficultyLevel)
            {
                StartCoroutine(SetUpLevel(_faultiMines.Count));
                _faultiMines.Clear();
            }
            else
            {
                LasersSet?.Invoke();
                this.gameObject.SetActive(false);
            }
        }

        public void AddMine()
        {
            Debug.Log("mineAdded");
            _activeiMines.Add(1);
        }

        public void AddFaultyMine()
        {
            _faultiMines.Add(1);
        }
    }
}
