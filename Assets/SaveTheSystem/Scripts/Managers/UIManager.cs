using UnityEngine;

namespace SaveTheSystem
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu UiMainMenu;
        public UIOptions UiOptions;
 


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayOptionsMenu(false);
       
        }

        public void DisplayMainMenu(bool isActive)
        {
            UiMainMenu.DisplayCanvas(isActive);
        }

        public void DisplayOptionsMenu(bool isActive)
        {
            UiOptions.DisplayCanvas(isActive);
        }
    }
}
