using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Setting/InputControl")]
public class PlayerInputReader : ScriptableObject, PlayerInputControl.IPlayerInputMapActions
{
    private PlayerInputControl _inputControl;

    private void OnEnable()
    {
        if(_inputControl is null)
        {
            _inputControl = new PlayerInputControl();
            _inputControl.PlayerInputMap.SetCallbacks(this);
        }

        _inputControl.PlayerInputMap.Enable();
    }

    public void OnPlayerMoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            CallbackManager.Instance.Callback("PlayerMove", (MoveDir)Enum.Parse(typeof(MoveDir), context.control.displayName));
        }
    }
}
