using UnityEngine;

public class MouseTargetPosition 
{
    private Transform _head;
    private Transform[] _body; 
    
    private Transform _robot; 

    private float _headRotationSpeed = 25.0f; 
    private float _bodyRotationSpeed = 5.0f;  
    
    private Camera _mainCamera;

    public MouseTargetPosition(ref PlayerParents parents)
    {
        // _head = parents.headParent;
        // _body = parents.bodyParent; 
        
       _robot = parents.auxRobot; 

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

            // _headPivot.rotation = Quaternion.Slerp(_headPivot.rotation, targetRotation, _headRotationSpeed * Time.deltaTime);
            // _bodyPivot.rotation = Quaternion.Slerp(_bodyPivot.rotation, targetRotation, _bodyRotationSpeed * Time.deltaTime);
            
            _robot.rotation = Quaternion.Slerp(_robot.rotation, targetRotation, _bodyRotationSpeed * Time.deltaTime);
        }
    }
}
