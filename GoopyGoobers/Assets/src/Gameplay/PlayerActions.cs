using UnityEngine;
namespace Gameplay {

    public class PlayerActions : MonoBehaviour {
        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        public void OnGridAction((int, int) colRow) {
            if (GameManager.KillGridEnemyAt(colRow.Item1, colRow.Item2))
                BroadcastMessage("OnKillGridEnemy");
        }
    }
}
