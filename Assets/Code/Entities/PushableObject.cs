using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody _body;
    private Vector3 _pendingMovement;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_pendingMovement != Vector3.zero)
        {
            _body.MovePosition(_body.position + _pendingMovement * Time.fixedDeltaTime);
            _pendingMovement = Vector3.zero;
        }
    }

    public void Grab()
    {

    }

    public void Release()
    {

    }

    public void Move(Vector2 delta)
    {
       _pendingMovement = new Vector3(delta.x, 0, delta.y);
    }
}