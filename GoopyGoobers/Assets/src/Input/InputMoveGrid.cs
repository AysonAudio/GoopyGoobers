using UnityEngine;
using UnityEngine.InputSystem;
using Settings;
namespace Input {

    /// <summary>
    /// Maps attached object's local translation to a Vec2 position input.
    /// Translation snaps to grid thresholds.
    /// First grid section is at bottom left of screen. <br/>
    /// On button input, sends a MonoBehaviour message with current grid cell as a tuple parameter.
    /// </summary>
    public class InputMoveGrid : MonoBehaviour, MainInputs.IPlayerActions  {
        public int CurrCol { get; private set; }
        public int CurrRow { get; private set; }

        /// <summary> Divides the screen width into this many grid sections. </summary>
        [Min(1)] public int gridCols = 3;
        /// <summary> Divides the screen height into this many grid sections. </summary>
        [Min(1)] public int gridRows = 3;

        /// <summary> Object's local position when input is at bottom left grid section (col 0 and row 0). </summary>
        public Vector3 positionOrigin = new Vector3(-3f, 1f, -3f);
        /// <summary> Change in object position when input moves one grid section in positive X direction. </summary>
        public Vector3 positionIncrementX = new Vector3(3f, 0f, 0f);
        /// <summary> Change in object position when input moves one grid section in positive Y direction. </summary>
        public Vector3 positionIncrementY = new Vector3(0f, 0f, 3f);

        ////////////////////////////////////////
        // IPlayerActions Events
        ////////////////////////////////////////
        public void OnPoint(InputAction.CallbackContext context) {
            var inputPos = context.ReadValue<Vector2>();
            var col = Mathf.Clamp((int)(inputPos.x * gridCols / Screen.width), 0, gridCols - 1);
            var row = Mathf.Clamp((int)(inputPos.y * gridRows / Screen.height), 0, gridRows - 1);
            if (col == CurrCol && row == CurrRow) return;
            transform.localPosition = positionOrigin + col * positionIncrementX + row * positionIncrementY;
            CurrCol = col;
            CurrRow = row;
        }

        public void OnAction(InputAction.CallbackContext context) {
            if (!context.performed) return;
            BroadcastMessage("OnGridAction", (CurrCol, CurrRow));
        }

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        public void OnEnable() => InputManager.MainInputs.Player.AddCallbacks(this);
        public void OnDisable() => InputManager.MainInputs.Player.RemoveCallbacks(this);
    }
}
