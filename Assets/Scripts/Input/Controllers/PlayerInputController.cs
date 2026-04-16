using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Input
{
    public class PlayerInputController : IPlayerInput, IDisposable
    {
        public event Action<Vector2> OnFirstTouch;
        public event Action<Vector2> OnMouseDown;
        public event Action OnSpacePress;
        public event Action OnSpaceRelease;

        public Vector2 TouchFirstPosition => _playerInput.TouchInput.FirstTouchPosition.ReadValue<Vector2>();
        public Vector2 MousePositionValue => _playerInput.MouseInput.MousePosition.ReadValue<Vector2>();

        private readonly PlayerInput _playerInput;

        public PlayerInputController(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.CommonInput.SpaceAction.started += OnSpacePressStarted;
            _playerInput.CommonInput.SpaceAction.canceled += OnSpacePressCanceled;
            _playerInput.TouchInput.FirstTouchContact.started += OnFirstTouchContactStarted;
            _playerInput.MouseInput.LeftMousePress.started += OnLeftMousePressStarted;
        }

        public void Dispose()
        {
            OnFirstTouch = null;
            OnMouseDown = null;
            OnSpacePress = null;
            OnSpaceRelease = null;
            _playerInput.CommonInput.SpaceAction.started -= OnSpacePressStarted;
            _playerInput.CommonInput.SpaceAction.canceled -= OnSpacePressCanceled;
            _playerInput.TouchInput.FirstTouchContact.started -= OnFirstTouchContactStarted;
            _playerInput.MouseInput.LeftMousePress.started -= OnLeftMousePressStarted;
        }

        private void OnSpacePressStarted(InputAction.CallbackContext context)
        {
            OnSpacePress?.Invoke();
        }

        private void OnSpacePressCanceled(InputAction.CallbackContext context)
        {
            OnSpaceRelease?.Invoke();
        }

        private void OnFirstTouchContactStarted(InputAction.CallbackContext context)
        {
            Debug.Log(TouchFirstPosition);
            OnFirstTouch?.Invoke(TouchFirstPosition);
        }

        private void OnLeftMousePressStarted(InputAction.CallbackContext context)
        {
            Debug.Log(MousePositionValue);
            OnMouseDown?.Invoke(MousePositionValue);
        }
    }
}