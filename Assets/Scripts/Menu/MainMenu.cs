// Brackeys - https://www.youtube.com/watch?v=zc8ac_qUXQY&t=447s&ab_channel=Brackeys - 2017

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menu {
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame() {
            SceneManager.LoadScene("mainscene");
        }
    }
}
