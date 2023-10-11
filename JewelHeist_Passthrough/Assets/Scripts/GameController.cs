using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        public static Action<string> DifficultyLevel;
        public static Action<string> StartGame;

        public bool isEasy;
        public bool isHard;
        public bool isImpossible;

        [SerializeField] private CanvasGroup _menu;
        [SerializeField] private GameObject _easy_btn;
        [SerializeField] private GameObject _hard_btn;
        [SerializeField] private GameObject _impossible_btn;
        [SerializeField] private GameObject _start_btn;

        private string _difficultyLevel;

        // Start is called before the first frame update
        void Start()
        {
          /*  _easy_btn.onClick.AddListener(delegate { Difficulty("easy"); });
            _hard_btn.onClick.AddListener(delegate { Difficulty("hard"); });
            _impossible_btn.onClick.AddListener(delegate { Difficulty("impossible"); });
            _start_btn.onClick.AddListener(delegate { StartGamePlay(); });*/
            FireMines.LasersSet += ActivateStartButton;
            _start_btn.SetActive(false);
        }
        private void OnDisable()
        {
            FireMines.LasersSet -= ActivateStartButton;
        }

        public void Difficulty(string _dificulty)
        {
            _difficultyLevel = _dificulty;

            switch (_dificulty)
            {
                case "easy":
                    DifficultyLevel?.Invoke(_dificulty);              
                    isEasy = true;
                    break;
                case "hard":
                    DifficultyLevel?.Invoke(_dificulty);
                    isHard = true;
                    break;
                case "impossible":
                    DifficultyLevel?.Invoke(_dificulty);
                    isImpossible = true;
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
    }
}

