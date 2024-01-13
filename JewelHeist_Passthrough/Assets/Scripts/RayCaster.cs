using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControl;
using SceneUnderstanding;
using TMPro;

public class RayCaster : MonoBehaviour
{

    [SerializeField] private Transform _rayStart;
    [SerializeField] private float _rayDistance;
    [SerializeField] private GameObject _instructions;
    [SerializeField] private TMP_Text _instructions_txt;
    [SerializeField] private float _minDistance;

    private SceneUnderstandingManager _sceneUnderstanding;

    bool _lookForPodium;
    bool _setup;
    // Start is called before the first frame update
    void Start()
    {
        _sceneUnderstanding = GameObject.FindObjectOfType<SceneUnderstandingManager>();

        GameController.ResetGame += Reset;
        //this delays the ability to trigger the podium until the player has moved away from teh center
        Invoke(nameof(CheckDistance), 1);
        _instructions_txt.text = "You're too close. \n Move to a corner of your space.";
        _lookForPodium = false;

    }

    private void OnDisable()
    {
        GameController.ResetGame -= Reset;
    }

    private void CheckDistance()
    {
        _setup = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_setup)
        {

            float distance = Vector3.Magnitude(_rayStart.transform.position - _instructions.transform.position);
            Debug.Log("distance" + distance);

            if (distance < _minDistance)
            {
                Instructions(0);
            }
            if (distance >= _minDistance)
            {
                Instructions(1);
            }
        }


        Ray _ray = new Ray(_rayStart.transform.position, _rayStart.forward);
        Debug.DrawRay(_rayStart.transform.position, _rayStart.forward, Color.red, _rayDistance);
        LayerMask mask = LayerMask.GetMask("RayTarget");
        Debug.Log("drawing ray");
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, _rayDistance, mask))
        {
            if (_lookForPodium)
            {
                if (_hit.collider.CompareTag("PodiumTrigger"))
                {
                    Debug.Log("start podium");

                    _hit.collider.gameObject.SetActive(false);
                    _sceneUnderstanding.ActivatePodium();
                    _lookForPodium = false;
                }
            }
        }
    }




    private void Reset()
    {
        Debug.Log("look for podium");
        _lookForPodium = false;
        CheckDistance();
    }

    private void Instructions(int instructions)
    {
        switch (instructions)
        {
            case 0:
                if (_instructions_txt != null)
                {
                    _instructions_txt.text = "You're too close. \n Move to a corner of your space.";
                }
                break;
            case 1:
                if (_instructions_txt != null)
                {
                    //_sceneUnderstanding.ActivatePodium();
                    _sceneUnderstanding.ActivateGroundTarget();
                    _setup = false;
                    _instructions_txt.text = "Look at the circle on the ground.";
                    _lookForPodium = true;
                }
                break;
        }
    }
}
