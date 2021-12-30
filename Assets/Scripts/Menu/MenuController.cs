// Brackeys - https://www.youtube.com/watch?v=zc8ac_qUXQY&t=447s&ab_channel=Brackeys - 2017


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
    

namespace Menu {
    public class MenuController : MonoBehaviour {
        
        public GameObject controlsMenu;
        public GameObject mainMenu;
        public GameObject backButton;
        public GameObject playButton;
        
        private void Awake() {
           
        }

        public void PlayGame() {
            SceneManager.LoadScene("mainscene");
        }

        public void DisplayControls() {
            controlsMenu.SetActive(true);
            mainMenu.SetActive(false);
            mainMenu.SetActive(!mainMenu.activeSelf);
            
            // Set back button as first selected
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(backButton, new BaseEventData(eventSystem));
        }
        
        public void DisplayMain() {
            controlsMenu.SetActive(false);
            mainMenu.SetActive(true);
            // Set back button as first selected
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(playButton, new BaseEventData(eventSystem));
        }
    }
}
