using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class InputComponent : Component
{
    public override void Update()
    {
        Vector3 dir = Vector3.zero;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            dir += forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir -= right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir -= forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += right;
        }

        dir = dir.normalized;

        float moveAmount = dir.magnitude;

        if (moveAmount > 0)
        {
            new MoveCommand(dir).Execute(Entity);
        }

        //Animation speed update 
        Entity.GetComponent<AnimationComponent>()?.SetMoveSpeed(moveAmount);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            new CrouchCommand().Execute(Entity);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            new StandUpCommand().Execute(Entity);
        }
    }
}
