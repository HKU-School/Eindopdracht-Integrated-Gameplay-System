using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class EnemyStateMachine : Component
{
    private IState _currentState;

    private readonly EnemySight _sight;
    private readonly EntityObject _enemy;
    private readonly EntityObject _player;
    private readonly GameManager _gameManager;

    private Transform[] _waypoints;


    public EnemyStateMachine(EntityObject enemy, EntityObject player, GameManager gameManager, Transform[] waypoints)
    {
        _enemy = enemy;
        _player = player;
        _gameManager = gameManager;
        _waypoints = waypoints;

        _sight = new EnemySight(enemy, player, LayerMask.GetMask("Wall", "LowWall"));
    }

    public override void Init()
    {
        ChangeState(new PatrolState(_enemy, this, _sight, _gameManager, _waypoints));
    }
    public override void Update()
    {
        _currentState?.Update();
    }
    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
