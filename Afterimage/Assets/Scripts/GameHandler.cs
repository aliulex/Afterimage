using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour{

        [SerializeField] GameObject pauseMenu;
        public string currentLevel = "";

        public static bool GameisPaused = false;
        public AudioMixer mixer;
        public static float volumeLevel = 1.0f;
        private Slider sliderVolumeCtrl;

        void Awake (){
                SetLevel (volumeLevel);
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                }
        }

        public void SetLevel (float sliderValue){
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
        }

        public void Pause() {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
        }

        public void Resume() {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void MainMenu() {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
        }

        public void Restart() {
                Time.timeScale = 1f;
                SceneManager.LoadScene(currentLevel);
        }

        void Start() {
                pauseMenu.SetActive(false);
                GameisPaused = false;
        }

        void Update(){         //delete this quit functionality when a Pause Menu is added
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused) {
                                Resume();
                        } else {
                                Pause();
                        }
                }
        }
}