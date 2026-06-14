using UnityEngine;

public interface ICommand
{
    void Execute(EntityObject entity);
}


public class MoveCommand : ICommand
{
    private readonly Vector3 _dir;

    public MoveCommand(Vector3 dir)
    {
        _dir = dir;
    }

    public void Execute(EntityObject entity)
    {
        entity.GetComponent<Movement>()?.Move(_dir);
    }
}

public class CrouchCommand : ICommand
{
    public void Execute(EntityObject entity)
    {
        entity.GetComponent<PlayerCrouch>()?.SetCrouch(true);
        entity.GetComponent<Movement>()?.SetCrouch(true);
        entity.GetComponent<AnimationComponent>()?.SetCrouch(true);
    }
}

public class StandUpCommand : ICommand
{
    public void Execute(EntityObject entity)
    {
        entity.GetComponent<PlayerCrouch>()?.SetCrouch(false);
        entity.GetComponent<Movement>()?.SetCrouch(false);
        entity.GetComponent<AnimationComponent>()?.SetCrouch(false);
    }
}