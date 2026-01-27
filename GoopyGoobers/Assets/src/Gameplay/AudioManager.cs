using UnityEngine;
namespace Gameplay {

    public class AudioManager : MonoBehaviour {
        public AudioSource audioSource;
        public AudioClip mainMenuMusic;
        public AudioClip levelMusic;

        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMainMenuMusic() {
            audioSource.Stop();
            audioSource.clip = mainMenuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayLevelMusic() {
            audioSource.Stop();
            audioSource.clip = levelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
