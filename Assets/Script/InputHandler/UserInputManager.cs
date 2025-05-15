using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputManager
{
    // Start is called before the first frame update
    private InputEvents _inputEvents;
    private UserKeyboardControls _userKeyboardControlls;

    public UserInputManager(InputEvents inputEvents)
    {
        _inputEvents = inputEvents;
        _userKeyboardControlls = new UserKeyboardControls();
        _userKeyboardControlls.Enable();
        _userKeyboardControlls.UserActionMap.Movement.performed += ctx => MoveAction(ctx);
        _userKeyboardControlls.UserActionMap.Movement.canceled += ctx => MoveAction(ctx);
        _userKeyboardControlls.UserActionMap.Rotation.performed += ctx => RotateAction(ctx);
        _userKeyboardControlls.UserActionMap.Rotation.canceled += ctx => RotateAction(ctx);
    }

    // Update is called once per frame
    private void MoveAction(InputAction.CallbackContext ctx)
    {
        //Debug.Log("[UserInputManager][MoveAction] Direction : " + ctx.ReadValue<Vector3>());
        _inputEvents.MoveCamera?.Invoke(ctx.ReadValue<Vector3>());
    }

    private void RotateAction(InputAction.CallbackContext ctx)
    {
        //Debug.Log("[UserInputManager][RotateAction] Direction : " + ctx.ReadValue<Vector2>());
        _inputEvents.RotateCamera?.Invoke(ctx.ReadValue<Vector2>());
    }

}
