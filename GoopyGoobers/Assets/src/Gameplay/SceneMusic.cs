using UnityEngine;
using UnityEngine.SceneManagement;
namespace Gameplay {

    public class SceneMusic : MonoBehaviour {
        AudioManager _audioManager;

        void Awake() {
            _audioManager = FindFirstObjectByType<AudioManager>();
            switch (SceneManager.GetActiveScene().name) {
                default:
                case "MainMenuScene":
                    _audioManager.PlayMainMenuMusic();
                    break;
                case "ChrisDevScene":
                    _audioManager.PlayLevelMusic();
                    break;
            }
        }
    }
}
