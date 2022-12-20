using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour{

        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject failMenu;
        [SerializeField] GameObject clearScreen;
        public string currentLevel = "";
        public static bool GameisPaused = false;
        public AudioMixer mixer;
        public static float volumeLevel = 1.0f;
        private Slider sliderVolumeCtrl;
        public GameObject timeText;
        public int gameTime = 60;
        public float gameTimer = 0f;

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
                failMenu.SetActive(false);
                clearScreen.SetActive(false);
                GameisPaused = false;
                UpdateTime();
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

        public void UpdateTime(){
                Text timeTextB = timeText.GetComponent<Text>();
                timeTextB.text = "" + gameTime;
        }

        void FixedUpdate() {
                gameTimer += 0.02f;
                if (!clearScreen.activeSelf) {
                        if (gameTimer >= 1f){
                                if (gameTime > 0) {
                                        gameTime -= 1;
                                }
                                gameTimer = 0;
                                UpdateTime();
                        }
                }
                if (gameTime <= 0){
                        gameTime = 0;
                        Time.timeScale = 0f;
                        GameisPaused = true;
                        failMenu.SetActive(true);
                }
        }
}