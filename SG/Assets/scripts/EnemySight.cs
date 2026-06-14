using UnityEngine;

public class EnemySight
{
    private readonly EntityObject _enemy;
    private readonly EntityObject _player;

    private float _range = 5f;
    private float _angle = 60f;

    private LayerMask _obstackleMask;

    public EnemySight(EntityObject enemy, EntityObject player, LayerMask obstacleMask)
    {
        _enemy = enemy;
        _player = player;
        _obstackleMask = obstacleMask;
    }

    //Check if enemy can see player
    public bool CanSeePlayer(bool isCrouching)
    {
        Vector3 dir = (_player.Transform.position - _enemy.Transform.position);
        float distance = dir.magnitude;

        float maxDistance = isCrouching ? _range * 0.5f : _range;

        if (distance > _range)
        {
            return false;
        }

        dir.Normalize();

        float angle = Vector3.Angle(_enemy.Transform.forward, dir);

        if (angle > _angle * 0.5f)
        {
            return false;
        }

        Vector3 origin = _enemy.Transform.position + Vector3.up * 1.5f;

        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, distance, _obstackleMask))
        {
            GameObject hitObj = hit.collider.gameObject;
            int lowWallLayer = LayerMask.NameToLayer("LowWall");
            int wallLayer = LayerMask.NameToLayer("Wall");

            if (hitObj.layer == wallLayer)
            {
                return false;
            }

            if (hitObj.layer == lowWallLayer && !isCrouching)
            {
                return false;
            }

            //Crouch && lowwal 
            return false;
        }

        return true;
    }
}
