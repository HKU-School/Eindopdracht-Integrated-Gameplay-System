using UnityEngine;

public static class WorldQuery
{
    public static bool HasLineOfSight(Vector3 from, Vector3 to, LayerMask mask)
    {
        Vector3 dir = to - from;
        float dist = dir.magnitude;

        return !Physics.Raycast(from, dir.normalized, dist, mask);
    }    
}
