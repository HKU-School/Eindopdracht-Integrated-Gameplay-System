using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private List<EntityObject> _entities = new();
    private GameManager _gameManager;
    private bool _isGameRunning;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform[] _enemyPaths;

    private EntityObject _player;

    private float _xRot; 
    private float _yRot;
    [SerializeField] private float _mouseSensitivity = 3f;

    [SerializeField] private Transform _exit;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _gameOverPanel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Make GameManager
        _gameManager = new GameManager();

        _startPanel.SetActive(true);
        _winPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _isGameRunning = false;
    }
    //Start game button
    public void StartGame()
    {
        _startPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SpawnGame();
        _gameManager.SetState(GameState.Playing);
        _isGameRunning = true;
    }

    private void SpawnGame()
    {
        
        _entities.Clear();

        //Make player
        _player = EntityFactory.CreatePlayer();
        _gameManager.SetPlayer(_player);
        _entities.Add(_player);

        //make enemy
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGameRunning)
        {
            return;
        }
        if (_player == null)
        {
            return;
        }

        MouseInput();
        Camera();

        if (_gameManager.State != GameState.Playing)
        {
            HandleEndState();
            return;
        }

        foreach (var entity in _entities)
        {
            entity.Update();
        }

        CheckWinCondition();
    }

    private void MouseInput()
    {
        _yRot += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _xRot -= Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _xRot = Mathf.Clamp(_xRot, -30f, 60f);
    }
    private void Camera()
    {
        Vector3 targetPos = _player.Transform.position;

        Quaternion rotation = Quaternion.Euler(_xRot, _yRot, 0);

        Vector3 offset = rotation * new Vector3(0, 1, -5);

        _camera.transform.position = targetPos + offset;
        _camera.transform.rotation = rotation;
    }

    private void SpawnEnemies()
    {
        foreach (Transform path in _enemyPaths)
        {
            Transform[] waypoints = new Transform[path.childCount];

            for (int i = 0; i < path.childCount; i++)
            {
                waypoints[i] = path.GetChild(i);
            }

            EntityObject enemy = EntityFactory.CreateEnemy(_player, _gameManager, path.GetChild(0).position, waypoints);

            _entities.Add(enemy);
        }
    }

    private void CheckWinCondition()
    {
        float dist = Vector3.Distance(_player.Transform.position, _exit.position);

        if (dist < 1.5f)
        {
            _gameManager.SetState(GameState.Win);
        }
    }

    private void HandleEndState()
    {
        _isGameRunning = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_gameManager.State == GameState.Win)
        {
            _winPanel.SetActive(true);
        }

        if (_gameManager.State == GameState.GameOver)
        {
            _gameOverPanel.SetActive(true);
        }
    }

    //Buttons
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
