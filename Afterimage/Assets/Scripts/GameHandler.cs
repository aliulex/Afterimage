using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{

        [SerializeField] GameObject pauseMenu;

        public void Pause() {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
        }

        public void Resume() {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
        }

        public void MainMenu() {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
        }

        public void Restart() {
                Time.timeScale = 1f;
                SceneManager.LoadScene("Tilemap-movement");
        }

        void Start() {
                pauseMenu.SetActive(false);
        }

        void Update(){         //delete this quit functionality when a Pause Menu is added
                if (Input.GetKey("escape")){
                        // Application.Quit();
                        // QuitGame();
                        Pause();
                }
        }
}