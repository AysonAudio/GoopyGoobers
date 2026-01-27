using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Settings;
namespace Input {

    /// <summary>
    /// Maps velocity of attached object's local rotation to a Vec2 position input.
    /// Maps pitch to y-input, and yaw to x-input.
    /// Scales input based on screen resolution.
    /// </summary>
    public class InputTiltDrift  : MonoBehaviour, MainInputs.IPlayerActions {
        /// <summary> Velocity when input is at a far edge of the screen. </summary>
        public float pitchMaxVelocity = 6.7f;
        /// <summary> Will stop rotating when it reaches this cap. </summary>
        public float pitchMinOffset = 20f;
        /// <summary> Will stop rotating when it reaches this cap. </summary>
        public float pitchMaxOffset = -20f;

        /// <summary> Velocity when input is at a far edge of the screen. </summary>
        public float yawMaxVelocity = 10f;
        /// <summary> Will stop rotating when it reaches this cap. </summary>
        public float yawMinOffset = -30f;
        /// <summary> Will stop rotating when it reaches this cap. </summary>
        public float yawMaxOffset = 30f;

        ////////////////////////////////////////
        // IPlayerActions Events
        ////////////////////////////////////////
        public void OnPoint(InputAction.CallbackContext context) {
            throw new NotImplementedException();
            // var inputPos = context.ReadValue<Vector2>();
            // var pitchScalar =
            // var yawScalar =
        }

        public void OnAction(InputAction.CallbackContext context) {}

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        public void OnEnable() => InputManager.MainInputs.Player.AddCallbacks(this);
        public void OnDisable() => InputManager.MainInputs.Player.RemoveCallbacks(this);
    }
}
