using UnityEngine;
namespace Anims {

    /// <summary>
    /// Listens for MonoBehaviour messages and controls animations.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharBoxAnims : MonoBehaviour {
        Animator _animator;
        public static int SpawnStateId { get; private set; }
        public static int IdleStateId { get; private set; }
        public static int ActionStateId { get; private set; }
        public static int ActionTriggerId { get; private set; }

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        void Awake() {
            _animator = GetComponent<Animator>();
            SpawnStateId = Animator.StringToHash("CharBoxSpawn");
            IdleStateId = Animator.StringToHash("CharBoxIdle");
            ActionStateId = Animator.StringToHash("CharBoxAction");
            ActionTriggerId = Animator.StringToHash("ActionTrigger");
        }

        /// <summary>
        /// If Action animation state is already playing, instantly restart it.
        /// If not, tell the animator to follow the transition graph to the Action state.
        /// </summary>
        public void OnGridAction((int, int) colRow) {
            if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == ActionStateId)
                _animator.Play(ActionStateId, -1, 0f);
            else _animator.SetTrigger(ActionTriggerId);
        }
    }
}
