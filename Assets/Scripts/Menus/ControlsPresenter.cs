using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

namespace Barebones2D.Menus
{
    public class ControlsPresenter
    {
        public Action BackAction { set => buttonBack.clicked += value; }
        private Button buttonBack;

        public ControlsPresenter(VisualElement root)
        {
            buttonBack = root.Q<Button>("Exit");
        }
    }    
}
