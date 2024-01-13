using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using GameControl;

public class PodiumControl : MonoBehaviour
{
    public static Action TimelineDone;

    private PlayableDirector _director;
    private Transform _player;

    bool _lookForPlayer;


    // Start is called before the first frame update
    void Start()
    {
        GameController.ResetGame += Destroy;
        _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        _director = this.GetComponent<PlayableDirector>();
        _lookForPlayer = true;
      //  Invoke(nameof(StopLooking), 2);
    }

    private void OnDisable()
    {
        GameController.ResetGame -= Destroy;
    }


    private void Update()
    {
        if (_lookForPlayer)
        {
            Vector3 _direction = this.transform.position - _player.transform.position;
            _direction.y = 0;
            this.transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
        }
    }

    public void TimelineFinished()
    {
        TimelineDone?.Invoke();
    }

    public void PlayTimeline()
    {
        _director.Play();
    }

   


    public void StopLooking()
    {
        _lookForPlayer = false;
    }


    private void Destroy()
    {
        Destroy(this.gameObject);
    }


}
