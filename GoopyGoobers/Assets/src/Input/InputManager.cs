using Settings;
namespace Input {

    /// <summary>
    /// A singleton that manages all programmatic access to
    /// InputActionAsset, InputActionMap, InputAction and InputControlScheme instances
    /// defined in asset "Assets/Settings/MainInputs.inputactions".
    /// </summary>
    public static class InputManager {
        public static MainInputs MainInputs { get; }
        static InputManager() {
            MainInputs = new MainInputs();
            MainInputs.Player.Enable();
        }
    }
}
