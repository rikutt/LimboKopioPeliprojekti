using UnityEngine.UIElements;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Barebones2D.Menus
{
    public class MainMenuPresenter
    {
         
        public Action OpenSettings { set => buttonControls.clicked += value; }

        private Button buttonStart;
        private Button buttonControls;
        private Button buttonQuit;
        private Button buttonMute;

        private bool muteIsOn;

        public MainMenuPresenter(VisualElement root, AudioMixerSnapshot normalVolume, AudioMixerSnapshot mutedVolume) 
        {
            buttonStart = root.Q<Button>("StartGameButton");
            buttonControls = root.Q<Button>("ControlsButton");
            buttonQuit = root.Q<Button>("QuitButton");
            buttonMute = root.Q<Button>("MuteButton");

            buttonStart.clicked += () =>
            {
                SceneManager.LoadScene("TutorialishLevel");
            };

            buttonQuit.clicked += () =>
            {
                #if UNITY_STANDALONE
                    Application.Quit();
                #endif
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
            };

            buttonMute.clicked += () =>
            {
                if (!muteIsOn)
                {
                    mutedVolume.TransitionTo(0.3f);
                    muteIsOn = true;
                }

                else if (muteIsOn)
                {
                    normalVolume.TransitionTo(0.3f);
                    muteIsOn = false;
                }

            };

        }
        /*


        buttonControls.clicked += () => menuView.style.display = DisplayStyle.None;
            
            //buttonControls.clicked += () => Debug.Log("Options pressed");
            buttonQuit.clicked += () => Debug.Log("Quit pressed");
        */
        
    }
}
