using UnityEngine;

public class AnimationComponent : Component
{
    private Animator _animator;

    public override void Init()
    {
        _animator = Entity.Transform.GetComponentInChildren<Animator>();
    }

    //Set moevement speed in animator
    public void SetMoveSpeed(float amount)
    {
        _animator?.SetFloat("Speed", amount);
    }

    //Set courch state in animator
    public void SetCrouch(bool value)
    {
        _animator?.SetBool("IsCrouching", value);
    }
}
