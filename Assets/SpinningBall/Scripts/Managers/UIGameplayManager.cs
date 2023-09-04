using UnityEngine;

namespace SpinningBall
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIMainMenu UIMainMenu;
        public UIGameplay UIGameplay;
        public UIGameover UIGameover;


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
            DisplayMenu(true);
        }

        public void CloseAll()
        {
            DisplayMenu(false);
            DisplayGameplayMenu(false);
            DisplayGameoverMenu(false);     
        }

        public void DisplayMenu(bool isActive)
        {
            UIMainMenu.DisplayCanvas(isActive);
        }


        public void DisplayGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }


        public void DisplayGameoverMenu(bool isActive)
        {
            UIGameover.DisplayCanvas(isActive);
        }
    }
}
