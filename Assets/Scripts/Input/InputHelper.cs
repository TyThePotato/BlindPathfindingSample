using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PathfindingGame.Input {

    public class InputHelper : MonoBehaviour {

        private static InputActionsGeneral _iag;

        private static InputHelper _inputHelper;
        
        private void Awake() {
            if (_inputHelper != null)
                Destroy(this);

            _inputHelper = this;

            _iag = new InputActionsGeneral();
            _iag.Enable();
        }
        
        // Input Shortcuts

        public static Vector2 Movement => _iag.General.Movement.ReadValue<Vector2>();
        public static bool IsSneaking => _iag.General.Sneak.IsPressed();

        public static InputAction SneakAction => _iag.General.Sneak;
        public static InputAction ThrowAction => _iag.General.Throw;

    }

}
