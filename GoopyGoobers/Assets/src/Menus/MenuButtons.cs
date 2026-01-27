using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Menus {

    public class MenuButtons : MonoBehaviour {
        public void StartGame() {
            SceneManager.LoadSceneAsync(1);
        }

        public void BackToMainMenu() {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
