using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControl;

public class FireMines : MonoBehaviour
{
    public static Action LasersSet;

    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _fireInterval;
    [SerializeField] private float _fireSpeed;

    [SerializeField] private int[] _laserCount;
    [SerializeField] private RandomRotator _rotator;

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

    // Update is called once per frame
    void Update()
    {
      //  _time += Time.deltaTime;

        /*  for (int i = 0; i < _laserCount[2]; i++)
          {
              if (_time >= _fireInterval)
              {
                  _time = 0;
                  FireMine();
              }
          }*/

    }

    private void FireMine()
    {
       
        GameObject _projectileMine = Instantiate(_mine, _firePoint.transform.position, _firePoint.transform.rotation);
        Rigidbody _rb = _projectileMine.GetComponent<Rigidbody>();
        _rb.AddForce(_firePoint.transform.forward * _fireSpeed, ForceMode.Impulse);
        _rotator.GenerateRandomRotation();
    }

    private void SetLevel(string _dificulty)
    {
        Debug.Log("start shooting mines");
        switch (_dificulty)
        {
            case "easy":
                StartCoroutine(SetUpLevel(_laserCount[0]));
                break;
            case "hard":

                StartCoroutine(SetUpLevel(_laserCount[1]));
                break;
            case "impossible":
                StartCoroutine(SetUpLevel(_laserCount[2]));
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

        LasersSet?.Invoke();
        this.gameObject.SetActive(false);
    }
}
