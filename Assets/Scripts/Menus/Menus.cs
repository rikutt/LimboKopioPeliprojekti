using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

/* 
 * Setups the start view and creates actions to open different menus and toggle them
 */

namespace Barebones2D.Menus
{
    public class Menus : MonoBehaviour
    {
        private VisualElement menuView;
        private VisualElement settingsView;

        [SerializeField] private AudioMixerSnapshot normalVolume;
        [SerializeField] private AudioMixerSnapshot mutedVolume;

        private void Start()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            menuView = root.Q("BasicMenu");
            settingsView = root.Q("ControlsMenu");

            SetupStartMenu();
            SetupControlsMenu();
        }


        private void SetupStartMenu()
        {
            MainMenuPresenter mainViewPresenter = new MainMenuPresenter(menuView, normalVolume, mutedVolume);

            mainViewPresenter.OpenSettings = () =>
            {
                menuView.Display(false);
                settingsView.Display(true);
            };
        }

        private void SetupControlsMenu()
        {
            ControlsPresenter controlsPresenter = new ControlsPresenter(settingsView);
            
            controlsPresenter.BackAction = () =>
            {
                menuView.Display(true);
                settingsView.Display(false);
            };
        }
    }
}
