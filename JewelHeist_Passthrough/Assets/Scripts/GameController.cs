using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using lasers;

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        public static Action<string> DifficultyLevel;
        public static Action<string> StartGame;
 
        [SerializeField] private CanvasGroup _menu;
        [SerializeField] private GameObject _easy_btn;
        [SerializeField] private GameObject _hard_btn;
        [SerializeField] private GameObject _impossible_btn;
        [SerializeField] private GameObject _start_btn;
        [SerializeField] private GameObject _resetGame_btn;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] Color _startColor;
        [SerializeField] Color _gameOverColor;
        private string _difficultyLevel;

        // Start is called before the first frame update
        void Start()
        {
       
            FireMines.LasersSet += ActivateStartButton;
            DrawLaser.HitPlayer = GameOver;
            SoundEmitter.TooLoud += GameOver;

            _start_btn.SetActive(false);
            _mainCamera.backgroundColor = _startColor;
        }


        private void OnDisable()
        {
            FireMines.LasersSet -= ActivateStartButton;
            DrawLaser.HitPlayer -= GameOver;
            SoundEmitter.TooLoud -= GameOver;
        }

        public void Difficulty(string _dificulty)
        {
            _difficultyLevel = _dificulty;

            switch (_dificulty)
            {
                case "easy":
                    DifficultyLevel?.Invoke(_dificulty);              
               
                    break;
                case "hard":
                    DifficultyLevel?.Invoke(_dificulty);
                 
                    break;
                case "impossible":
                    DifficultyLevel?.Invoke(_dificulty);
               
                    break;
            }

            
        }

        private void ActivateStartButton()
        {
            _hard_btn.gameObject.SetActive(false);
            _easy_btn.gameObject.SetActive(false);
            _impossible_btn.gameObject.SetActive(false);
            _start_btn.gameObject.SetActive(true);
        }

        public void StartGamePlay()
        {

            StartGame?.Invoke(_difficultyLevel);
            _menu.gameObject.SetActive(false);
        }

        private void GameOver()
        {
            _mainCamera.backgroundColor = _gameOverColor;
        }
    }
}

