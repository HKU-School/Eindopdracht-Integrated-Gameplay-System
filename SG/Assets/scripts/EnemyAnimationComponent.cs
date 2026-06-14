using UnityEngine;

public class EnemyAnimationComponent : Component
{
    private Animator _animator;

    public override void Init()
    {
        _animator = Entity.Transform.GetComponentInChildren<Animator>();
    }

    public void SetSpeed (float speed)
    {
        Debug.Log("Speed = " + speed);
        _animator.SetFloat("Speed", speed);
    }
}
