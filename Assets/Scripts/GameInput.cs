using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput: MonoBehaviour
{


    private CellsManager.MovementDirection _direction;
    private bool SecondaryKeyWasPressed = false;
    private bool rightKeyWasPressed = false;
    private bool leftKeyWasPressed = false;
    public event EventHandler<KeysPressedEventArgs> OnKeysPressed; 
    public class KeysPressedEventArgs : EventArgs
    {
        public CellsManager.MovementDirection KeysPressed;
    }
    private InputAction upArrow;
    private InputAction downArrow;
    private InputAction rightArrow;
    private InputAction leftArrow;
    
    private void Start()
    {
        upArrow = new InputAction(
            type:InputActionType.Button,
            binding:"<Keyboard>/upArrow"
            );
        downArrow = new InputAction(
            type:InputActionType.Button,
            binding:"<Keyboard>/downArrow"
        );
        rightArrow = new InputAction(
            type:InputActionType.Button,
            binding:"<Keyboard>/rightArrow"
        );
        leftArrow = new InputAction(
            type:InputActionType.Button,
            binding:"<Keyboard>/leftArrow"
        );
        
        upArrow.Enable();
        downArrow.Enable();
        rightArrow.Enable();
        leftArrow.Enable();
        
        upArrow.canceled+= _ =>
        {
            if (rightArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.RightUp;
                Debug.Log(_direction.ToString());
                SecondaryKeyWasPressed = true;
                InvokeOnMovementPerformedEvent(_direction);
            }
            if (leftArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.LeftUp;
                Debug.Log(_direction.ToString());
                SecondaryKeyWasPressed = true;
                InvokeOnMovementPerformedEvent(_direction);
            }
        };
        downArrow.canceled+= _ =>
        {
            if (rightArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.RightDown;
                Debug.Log(_direction.ToString());
                SecondaryKeyWasPressed = true;
                InvokeOnMovementPerformedEvent(_direction);
            }
            if (leftArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.LeftDown;
                Debug.Log(_direction.ToString());
                SecondaryKeyWasPressed = true;
                InvokeOnMovementPerformedEvent(_direction);
            }
        };
        rightArrow.canceled+= _ =>
        {
            if (upArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.RightUp;
                InvokeOnMovementPerformedEvent(_direction);
                Debug.Log(_direction.ToString());
                return;
            }
            if (downArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.RightDown;
                InvokeOnMovementPerformedEvent(_direction);
                Debug.Log(_direction.ToString());
                return;
            }
            if (leftArrow.IsPressed())
            {
                rightKeyWasPressed = true;
                return;
            }

            if (leftKeyWasPressed)
            {
                leftKeyWasPressed = false;
                return;
            }

            if (SecondaryKeyWasPressed)
            {
                SecondaryKeyWasPressed = false;
                return;
            }

            _direction = CellsManager.MovementDirection.Right;
            InvokeOnMovementPerformedEvent(_direction);
            Debug.Log(_direction.ToString());
        };
        leftArrow.canceled+= _ =>
        {
            if (upArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.LeftUp;
                InvokeOnMovementPerformedEvent(_direction);
                Debug.Log(_direction.ToString());
                return;
            }
            if (downArrow.IsPressed())
            {
                _direction = CellsManager.MovementDirection.LeftDown;
                InvokeOnMovementPerformedEvent(_direction);
                Debug.Log(_direction.ToString());
                return;
            }
            if (rightArrow.IsPressed())
            {
                leftKeyWasPressed = true;
                return;
            }

            if (rightKeyWasPressed)
            {
                rightKeyWasPressed = false;
                return;
            }
            if (SecondaryKeyWasPressed)
            {
                SecondaryKeyWasPressed = false;
                return;
            }

            _direction = CellsManager.MovementDirection.Left;
            InvokeOnMovementPerformedEvent(_direction);
            Debug.Log(_direction.ToString());
        };
    }

    private void InvokeOnMovementPerformedEvent(CellsManager.MovementDirection direction)
    {
        OnKeysPressed?.Invoke(this , new KeysPressedEventArgs
        {
            KeysPressed = direction
        });
    }
}
