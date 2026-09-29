using UnityEngine;

public class MouseTargetPosition
{
    private Transform _robot;
    
    private Transform _head;
    private Transform[] _body;

    private float _headRotationSpeed = 5.0f;
    private float _bodyRotationSpeed = 15.0f;
    private Camera _mainCamera;

    public MouseTargetPosition(ref PlayerParents parents)
    {
        Debug.Log("me estoy instanciando");

        // _head = parents.headParent;
        // _body = parents.bodyParent;

        _robot = parents.auxRobot;
        
        Debug.Log("parents assigned");

        if (!_head)
            Debug.Log("head null");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _mainCamera = Camera.main;
    }

    public void Update()
    {
        Vector3 cameraForward = _mainCamera.transform.forward;

        if (cameraForward.sqrMagnitude > 0.05f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward.normalized);

            // _head.rotation = Quaternion.Slerp(_head.rotation, targetRotation, _headRotationSpeed * Time.deltaTime);
            //
            // foreach (Transform part in _body)
            // {
            //     part.rotation = Quaternion.Slerp(part.rotation, targetRotation, _headRotationSpeed * Time.deltaTime);
            // }
            
            _robot.rotation = Quaternion.Slerp(_robot.rotation, targetRotation, _headRotationSpeed * Time.deltaTime);
        }
    }
}