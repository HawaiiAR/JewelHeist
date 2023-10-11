using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using OVR;

using System;

namespace SceneUnderstanding 

{ 
public class SceneUnderstandingManager : MonoBehaviour
{
    [SerializeField] private OVRSceneManager _sceneManeger;
    [SerializeField] private OVRSceneModelLoader _modelLoder;

    [SerializeField] private GameObject _podium;

    private void Awake()
    {
        _sceneManeger = GetComponent<OVRSceneManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _sceneManeger.SceneModelLoadedSuccessfully += OnSceneModelLoadedSuccessfully;
    }

    private void OnDisable()
    {
        _sceneManeger.SceneModelLoadedSuccessfully -= OnSceneModelLoadedSuccessfully;
    }

    private void OnSceneModelLoadedSuccessfully()
    {
        StartCoroutine(AddCollidersToModel());
    }

    IEnumerator AddCollidersToModel()
    {

        yield return new WaitForEndOfFrame();

        MeshRenderer[] _sceneObjects = FindObjectsOfType<MeshRenderer>();


        foreach(var obj in _sceneObjects)
        {
            if(obj.GetComponent<Collider>()== null)
            {
                  
                            
                    BoxCollider box = obj.gameObject.AddComponent<BoxCollider>();
                 //   Mesh mesh = obj.GetComponent<Mesh>();
                   // box.size = new Vector3(mesh.bounds.size.x, mesh.bounds.size.y, mesh.bounds.size.z + .5f);
                //    box.size = new Vector3(obj.gameObject.transform.localScale.x * 5, obj.gameObject.transform.localScale.y * 5, obj.gameObject.transform.localScale.z - .75f);
                    obj.gameObject.AddComponent<AttatchableSurface>();
                }
        }

        OVRSemanticClassification[] _floors = FindObjectsOfType<OVRSemanticClassification>()
        .Where(c => c.Contains(OVRSceneManager.Classification.Floor))
        .ToArray();
 
        foreach(var _floor in _floors)
        {
            Material _mat = _floor.gameObject.GetComponent<Renderer>().material;
            _mat.SetColor("_Color", Color.red);
            Debug.Log("Change floor Color");
            Instantiate(_podium, _floor.transform.position, Quaternion.identity);
        }

        /*    OVRSemanticClassification[] _walls = FindObjectsOfType<OVRSemanticClassification>()
         .Where(c => c.Contains(OVRSceneManager.Classification.WallFace))
         .ToArray();

            foreach (var _wall in _walls)
            {
                _wall.gameObject.AddComponent<AttatchableSurface>();
                Debug.Log("attatchable" + _wall.gameObject.name);

              BoxCollider collider = _wall.GetComponent<BoxCollider>();
               collider.size = new Vector3(_wall.gameObject.transform.localScale.x * 5, _wall.gameObject.transform.localScale.y * 5, _wall.gameObject.transform.localScale.z - .75f);
;            }*/

        }

        private void ResizeColliders(string _classificationName)
        {
         
        }

}
}
