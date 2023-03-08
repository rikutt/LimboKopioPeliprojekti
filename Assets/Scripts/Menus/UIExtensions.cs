using UnityEngine.UIElements;

namespace Barebones2D.Menus
{
    public static class UIExtensions
    {
        public static void Display(this VisualElement element, bool setEnabled)
        {
            if (element == null) return;
            element.style.display = setEnabled ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
