using UnityEngine;

public abstract class Component
{
    public EntityObject Entity { get; private set; }

    public void SetEntity(EntityObject entity)
    {
        Entity = entity;
    }

    public virtual void Init() { }
    public virtual void Update() { }
}
