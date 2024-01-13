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
        public static Action ResetGame;

        [SerializeField] private GameObject _menu;
     
        [SerializeField] private GameObject _easy_btn;
        [SerializeField] private GameObject _hard_btn;
        [SerializeField] private GameObject _impossible_btn;
        [SerializeField] private GameObject _start_btn;
        [SerializeField] private GameObject _resetGame_btn;

      

        [SerializeField] private Camera _mainCamera;
        [SerializeField] Color _startColor;
        [SerializeField] Color _gameOverColor;
        private string _difficultyLevel;

        [SerializeField] GameObject _winLosePannel;
        [SerializeField] private TMP_Text _winLose_txt;
        

        // Start is called before the first frame update
        void Start()
        {

            FireMines.LasersSet += ActivateStartButton;
            DrawLaser.HitPlayer = GameOver;
            SoundEmitter.TooLoud += GameOver;
            PodiumControl.TimelineDone += DisplayMenu;
            JewelControl.PlayerWon += WinState;
            InitialSetup();
        }


        private void OnDisable()
        {
            FireMines.LasersSet -= ActivateStartButton;
            DrawLaser.HitPlayer -= GameOver;
            SoundEmitter.TooLoud -= GameOver;
            PodiumControl.TimelineDone -= DisplayMenu;
            JewelControl.PlayerWon -= WinState;
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

            _menu.SetActive(false);
        }

        private void ActivateStartButton()
        {
            _menu.SetActive(true);
            _hard_btn.gameObject.SetActive(false);
            _easy_btn.gameObject.SetActive(false);
            _impossible_btn.gameObject.SetActive(false);
            _start_btn.gameObject.SetActive(true);
        }

        public void StartGamePlay()
        {

            StartGame?.Invoke(_difficultyLevel);
            _start_btn.gameObject.SetActive(false);
            _menu.gameObject.SetActive(false);
        }

        private void GameOver()
        {
            WinState("lose");
            _mainCamera.backgroundColor = _gameOverColor;
           
        }

        private void WinState(string winState)
        {
            _menu.SetActive(true);
            _winLosePannel.SetActive(true);
            _resetGame_btn.gameObject.SetActive(true);

            switch (winState)
            {
                case "win":
                    _winLose_txt.text = "YOU WIN!";
                    break;
                case "lose":
                    _winLose_txt.text = "YOU LOSE!";
                    break;

            }
        }

        private void InitialSetup()
        {
            _hard_btn.gameObject.SetActive(true);
            _easy_btn.gameObject.SetActive(true);
            _impossible_btn.gameObject.SetActive(true);
            _resetGame_btn.SetActive(false);
            _menu.SetActive(false);
            _start_btn.SetActive(false);
            _winLosePannel.SetActive(false);
            _mainCamera.backgroundColor = _startColor;
        }

        public void ResetGamePlay()
        {
            ResetGame?.Invoke();
            InitialSetup();
        }

        //this is called from timline
        public void DisplayMenu()
        {
            _menu.SetActive(true);
        }


    }
}

