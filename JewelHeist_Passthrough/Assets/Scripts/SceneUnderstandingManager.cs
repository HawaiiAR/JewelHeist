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
        [SerializeField] private Transform _player;

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
        }

        private void OnDisable()
        {
            _sceneManeger.SceneModelLoadedSuccessfully -= OnSceneModelLoadedSuccessfully;
            GameController.ResetGame -= OnSceneModelLoadedSuccessfully;
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
                    //make sure that the mesh prefab is using PlaneOVER mesh with furniture spawner and floor and plane prefabs added
                    BoxCollider box = obj.gameObject.AddComponent<BoxCollider>();
                    obj.gameObject.AddComponent<AttatchableSurface>();
                }
            }

            OVRSemanticClassification[] _floors = FindObjectsOfType<OVRSemanticClassification>()
            .Where(c => c.Contains(OVRSceneManager.Classification.Floor))
            .ToArray();

            foreach (var _floor in _floors)
            {
                /* Material _mat = _floor.gameObject.GetComponent<Renderer>().material;
                  _mat.SetColor("_Color", Color.red);
                  Debug.Log("Change floor Color");*/

                //instantiate podium
                Instantiate(_podium, _floor.transform.position, Quaternion.identity);

                //instantiate safezone
                Vector3 _safeZonePos = new Vector3(_player.transform.position.x, _floor.transform.position.y, _player.transform.position.z);
                Instantiate(_safeZone, _safeZonePos, Quaternion.identity);

            }
     
        }


    }
}
