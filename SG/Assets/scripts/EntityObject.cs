using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityObject
{
    private readonly List<Component> _components = new();

    public Transform Transform { get; }

    public EntityObject(Transform transform)
    {
        Transform = transform;
    }

    //Add component and initialzie 
    public void AddComponent(Component component)
    {
        component.SetEntity(this);
        _components.Add(component);
        component.Init();
    }

    //Get component by type
    public T GetComponent<T>() where T : Component
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    public void Update()
    {
        foreach (var component in _components)
        {
            component.Update();
        }
    }
}
