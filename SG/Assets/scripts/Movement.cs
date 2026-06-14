using UnityEngine;

public class Movement : Component
{
    private float _walkSpeed = 5f;
    private float _crouchSpeed = 2.5f;

    private float _currentSpeed;

    public override void Init()
    {
        _currentSpeed = _walkSpeed;
    }

    public void Move(Vector3 direction)
    {
        Entity.Transform.position += direction * _currentSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            Entity.Transform.rotation = Quaternion.Slerp(Entity.Transform.rotation, targetRot, 10f * Time.deltaTime);
        }
       
    }

    public void SetCrouch(bool isCrouching)
    {
        _currentSpeed = isCrouching ? _crouchSpeed : _walkSpeed;
    }
}
