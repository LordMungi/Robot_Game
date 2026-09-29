using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : PlayerState
{
    private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

    private MoveHandler _movementHandler;
    private GrabHandler _grabHandler;

    private PlayerInputActions _playerInput;
    private readonly Transform _cameraTransform;

    public MoveState(ref PlayerData data, ref PlayerParents parents)
    {
        _handlers.Add(_movementHandler = new MoveHandler(data.controller, data.config.movingMoveData));
        _handlers.Add(_grabHandler = new GrabHandler(ref parents));

        _cameraTransform = data.cameraTransform;
        _playerInput = new PlayerInputActions();
    }

    public override void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.canceled += OnMoveCanceled;
        _playerInput.Player.Grab.performed += OnGrabItem;
        _playerInput.Player.Release.performed += OnReleaseItem;
        _playerInput.Player.Jump.performed += OnJump;
        base.Enable();
    }

    public override void Disable()
    {
        _playerInput.Player.Move.canceled -= OnMoveCanceled; 
        _playerInput.Player.Grab.performed -= OnGrabItem;
        _playerInput.Player.Release.performed -= OnReleaseItem;
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Disable();
        base.Disable();
    }

    public override void Update()
    {
        Vector2 input = _playerInput.Player.Move.ReadValue<Vector2>();
        
        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * input.y) + (camRight * input.x);

        _movementHandler.Move(moveDirection);
        
        _movementHandler.Fall();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _nextState = BehaviourFSM.State.Idle;
        EventBus.Raise<OnPlayerStateChangeRequest>(_nextState);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _nextState = BehaviourFSM.State.Jump;
        EventBus.Raise<OnPlayerStateChangeRequest>(_nextState);
    }

    private void OnGrabItem(InputAction.CallbackContext context)
    {
        _grabHandler.Grab();
    }

    private void OnReleaseItem(InputAction.CallbackContext context)
    {
        _grabHandler.Release();
    }
}
