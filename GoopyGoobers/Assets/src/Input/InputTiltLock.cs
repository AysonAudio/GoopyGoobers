using UnityEngine;
using UnityEngine.InputSystem;
using Settings;
namespace Input {

    /// <summary>
    /// Maps attached object's local rotation to a Vec2 position input.
    /// Maps pitch to y-input, and yaw to x-input.
    /// Scales input based on screen resolution.
    /// </summary>
    public class InputTilt : MonoBehaviour, MainInputs.IPlayerActions {
        /// <summary> Initial Euler angle when input is at center of screen. </summary>
        public float pitchBase = 0f;
        /// <summary> Euler angle offset when input is at bottom of screen. </summary>
        public float pitchMinOffset = 20f;
        /// <summary> Euler angle offset when input is at top of screen. </summary>
        public float pitchMaxOffset = -20f;

        /// <summary> Initial Euler angle when input is at center of screen. </summary>
        public float yawBase = 0f;
        /// <summary> Euler angle offset when input is at left of screen. </summary>
        public float yawMinOffset = -30f;
        /// <summary> Euler angle offset when input is at right of screen. </summary>
        public float yawMaxOffset = 30f;

        ////////////////////////////////////////
        // IPlayerActions Events
        ////////////////////////////////////////
        public void OnPoint(InputAction.CallbackContext context) {
            var inputPos = context.ReadValue<Vector2>();
            var pitch = pitchBase + Mathf.Lerp(pitchMinOffset, pitchMaxOffset, inputPos.y / Screen.height);
            var yaw = yawBase + Mathf.Lerp(yawMinOffset, yawMaxOffset, inputPos.x / Screen.width);
            var rotation = Quaternion.Euler(pitch, yaw, transform.eulerAngles.z);
            transform.SetLocalPositionAndRotation(transform.localPosition, rotation);
        }

        public void OnAction(InputAction.CallbackContext context) {}

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        public void OnEnable() => InputManager.MainInputs.Player.AddCallbacks(this);
        public void OnDisable() => InputManager.MainInputs.Player.RemoveCallbacks(this);
    }
}
