using UnityEngine;
using UnityEngine.UIElements;

public static class EntityFactory
{
    public static EntityObject CreatePlayer()
    {
        GameObject prefab = Resources.Load<GameObject>("PlayerPrefab");
        GameObject go = GameObject.Instantiate(prefab);

        go.transform.position = new Vector3(0, 0, 0);

        var player = new EntityObject(go.transform);

        player.AddComponent(new InputComponent());
        player.AddComponent(new Movement());
        player.AddComponent(new AnimationComponent());
        player.AddComponent(new PlayerCrouch());

        return player;
        //return new EntityObject(go.transform);
    }

    public static EntityObject CreateEnemy(EntityObject player, GameManager gameManager, Vector3 position, Transform[] waypoints)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("EnemyPrefab"), position, Quaternion.identity);

        EntityObject entity = new EntityObject(obj.transform);

        entity.AddComponent(new EnemyAnimationComponent());

        entity.AddComponent(new EnemyStateMachine(entity, player, gameManager, waypoints));

        return entity;
    }
}
 