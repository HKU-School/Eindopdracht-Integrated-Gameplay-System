using UnityEngine;

public enum PatrolAxis
{
    Horizonatal,
    Vertical
}

public class PatrolState : IState
{
    private readonly EntityObject _entity;
    private readonly EnemyStateMachine _stateMachine;
    private readonly EnemySight _sight;
    private readonly GameManager _gameManager;

    private Vector3 _moveDir;

    private readonly float _speed = 2f;

    private bool _isWaiting;
    private float _waitTimer;
    private float _waitDuration = 2f;

    private Transform[] _waypoints;
    private int _currentIndex = 0;

    public PatrolState(EntityObject entity, EnemyStateMachine stateMachine, EnemySight sight, GameManager gameManager, Transform[] waypoints)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _sight = sight;
        _gameManager = gameManager;
        _waypoints = waypoints;
    }

    public void Enter() 
    {
        _currentIndex = 0;
        _isWaiting = false;
        _waitTimer = 0f;

        Debug.Log($"Waypoints: {_waypoints.Length}");
    }

    public void Update()
    {
        Wait();
        Move();
        Rotate();
        Arrvival();
        CheckSight();
    }

    private void Wait()
    {
        if (!_isWaiting)
        {
            return;
        }
        _entity.GetComponent<EnemyAnimationComponent>()?.SetSpeed(0f);
       
        _waitTimer += Time.deltaTime;

        if (_waitTimer >= _waitDuration)
        {
            _isWaiting = false;
            NextWaypoint();
        }
    }
    private void Move()
    {
        if (_isWaiting)
        {
            return;
        }

        Transform target = _waypoints[_currentIndex];

        Vector3 oldPas = _entity.Transform.position;

        Vector3 newPos = Vector3.MoveTowards(oldPas, target.position, _speed * Time.deltaTime);

        _moveDir = (newPos - oldPas).normalized;
        _entity.Transform.position = newPos;

        _entity.GetComponent<EnemyAnimationComponent>()?.SetSpeed(1f);
    }
    private void Rotate()
    {
        //Add when time visual in game not only debug.
        Debug.DrawRay(_entity.Transform.position, _entity.Transform.forward * 2f, Color.blue);
        Debug.DrawRay(_entity.Transform.position, (_waypoints[_currentIndex].position - _entity.Transform.position).normalized * 2f, Color.red);

        Vector3 dir = _waypoints[_currentIndex].position - _entity.Transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
        {
            return;
        }

        Quaternion rot = Quaternion.LookRotation(dir);

        Vector3 currentEuler = _entity.Transform.rotation.eulerAngles;
        Vector3 targetEuler = rot.eulerAngles;

        float smoothY = Mathf.LerpAngle(currentEuler.y, targetEuler.y, 10f * Time.deltaTime);
        _entity.Transform.rotation = Quaternion.Euler(0f, smoothY, 0f);
    }
    private void Arrvival()
    {
        Transform target = _waypoints[_currentIndex];

        float dist = Vector3.Distance(_entity.Transform.position, target.position);

        if (dist < 0.2f && !_isWaiting)
        {
            _isWaiting = true;
            _waitTimer = 0f;
        }
    }
    private void NextWaypoint()
    {
        _currentIndex++;

        if (_currentIndex >= _waypoints.Length)
        {
            _currentIndex = 0;
        }
    }
    private void CheckSight()
    {
        bool isCrouching = _gameManager.Player.GetComponent<PlayerCrouch>()?.IsCrouching ?? false;
       
        if (_sight.CanSeePlayer(isCrouching))
        {
            _gameManager.SetState(GameState.GameOver);
        }
    }

    public void Exit() { }
}
