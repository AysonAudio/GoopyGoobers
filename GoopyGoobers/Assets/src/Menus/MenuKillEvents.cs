using Gameplay;
using TMPro;
using UnityEngine;
namespace Menus {

    public class MenuKillEvents : MonoBehaviour {
        public TextMeshProUGUI killCountTextMesh;
        public GameObject winGamePanel;

        public void OnKillGridEnemy() {
            killCountTextMesh.SetText(GameManager.Kills.ToString());
            //TODO: use GameManager.WinGame() instead
            if (GameManager.Kills >= GameManager.ActiveLevel.KillsToWin)
                winGamePanel.SetActive(true);
        }
    }
}
