using UnityEngine;

public class PlayerCrouch : Component
{
    public bool IsCrouching { get; private set; }

    public void SetCrouch(bool crouch)
    {
        IsCrouching = crouch;
    }
}
