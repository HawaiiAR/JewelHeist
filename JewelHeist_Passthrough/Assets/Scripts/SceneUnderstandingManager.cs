using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameControl;

namespace SceneUnderstanding 

{
    public class SceneUnderstandingManager : MonoBehaviour
    {
        [SerializeField] private OVRSceneManager _sceneManeger;
        [SerializeField] private OVRSceneModelLoader _modelLoder;

        [SerializeField] private GameObject _podium;
        [SerializeField] private GameObject _safeZone;
        [SerializeField] private GameObject _instructions;
        [SerializeField] private GameObject _groundTarget;
        [SerializeField] private Transform _player;

        private Vector3 _floorPos;

        private void Awake()
        {
            _sceneManeger = GetComponent<OVRSceneManager>();
            _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _sceneManeger.SceneModelLoadedSuccessfully += OnSceneModelLoadedSuccessfully;
            GameController.ResetGame += OnSceneModelLoadedSuccessfully;
            _groundTarget.SetActive(false);
           // GameController.StartGame += ActivateSafeZone;
        }

        private void OnDisable()
        {
            _sceneManeger.SceneModelLoadedSuccessfully -= OnSceneModelLoadedSuccessfully;
            GameController.ResetGame -= OnSceneModelLoadedSuccessfully;
          //  GameController.StartGame -= ActivateSafeZone;
        }

        private void OnSceneModelLoadedSuccessfully()
        {
            StartCoroutine(AddCollidersToModel());
        }

        IEnumerator AddCollidersToModel()
        {

            yield return new WaitForEndOfFrame();

            MeshRenderer[] _sceneObjects = FindObjectsOfType<MeshRenderer>();


            foreach (var obj in _sceneObjects)
            {
                if (obj.GetComponent<Collider>() == null)
                {
             
                   MeshCollider collider = obj.gameObject.AddComponent<MeshCollider>();
                    collider.convex = true;
               
                    obj.gameObject.AddComponent<AttatchableSurface>();
                }
            }

            OVRSemanticClassification[] _floors = FindObjectsOfType<OVRSemanticClassification>()
            .Where(c => c.Contains(OVRSceneManager.Classification.Floor))
            .ToArray();

            foreach (var _floor in _floors)
            {
             
                _floorPos = _floor.transform.position;

                _instructions.transform.position = new Vector3(_floor.transform.position.x, _player.transform.position.y, _floor.transform.position.z);
                _instructions.SetActive(true);

            }
     
        }

        public void ActivateGroundTarget()
        {
            _groundTarget.transform.position = new Vector3(_floorPos.x, .1f, _floorPos.z);
            _groundTarget.SetActive(true);
        }

        public void ActivatePodium()
        {
            _groundTarget.SetActive(false);
            _instructions.SetActive(false);
    
            Instantiate(_podium, _floorPos, Quaternion.identity);
            Invoke(nameof(ActivateSafeZone), 2);

        }

        public void ActivateSafeZone()
        { 
            //instantiate safezone
            Vector3 _safeZonePos = new Vector3(_player.transform.position.x, _floorPos.y, _player.transform.position.z);
            Instantiate(_safeZone, _safeZonePos, Quaternion.identity);
           
        }


    }
}
