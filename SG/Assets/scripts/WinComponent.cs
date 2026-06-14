using UnityEngine;

public class WinComponent : Component
{
    private readonly EntityObject _player;
    private readonly GameManager _gameManager;
    private const float Range = 1.5f;

    public WinComponent(EntityObject player, GameManager gameManager)
    {
        _player = player;
        _gameManager = gameManager;
    }

    public override void Update()
    {
        float distance = Vector3.Distance(Entity.Transform.position, _player.Transform.position);
        if (distance < Range)
        {
            _gameManager.SetState(GameState.Win);
        }
    }
}
