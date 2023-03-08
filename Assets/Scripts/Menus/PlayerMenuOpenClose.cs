using UnityEngine.InputSystem;
using UnityEngine;

namespace Barebones2D
{
    public class PlayerMenuOpenClose : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        void OnMenuOpenClose(InputValue value)
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else 
                menu.SetActive(true);
        }
    }
}
