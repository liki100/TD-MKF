using System;
using UnityEngine;

public class EntryPointGame : MonoBehaviour
{
    [SerializeField] private Camera _cameraPrefab;
    [SerializeField] private GUIHolder _guiHolderPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private SpaceStation _spaceStationPrefab;
    [SerializeField] private RandomWave _spawnerPrefab;
    
    private EventBus _eventBus;
    private Camera _camera;
    private GUIHolder _guiHolder;
    private Player _player;
    private SpaceStation _spaceStation;
    private RandomWave _spawner;

    private void Awake()
    {
        _eventBus = new EventBus();
        
        InstantiatePreloadedAssets();
        RegisterServices();
        Init();
    }

    private void InstantiatePreloadedAssets()
    {
        _camera = Instantiate(_cameraPrefab);
        _guiHolder = Instantiate(_guiHolderPrefab);
        _player = Instantiate(_playerPrefab);
        _spaceStation = Instantiate(_spaceStationPrefab);
        _spawner = Instantiate(_spawnerPrefab);
    }

    private void RegisterServices()
    {
        ServiceLocator.Init();
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(_guiHolder);
    }
    
    private void Init()
    {
        var canvas = _guiHolder.GetComponent<Canvas>();
        canvas.worldCamera = _camera;

        var weapon = _player.GetComponent<Сannon>();
        var playerRotator = _player.GetComponentInChildren<PlayerRotator>();
        
        _player.Init();
        weapon.Init();
        playerRotator.Init(_camera);
        
        _spaceStation.Init();
        _spawner.Init();
    }
}
