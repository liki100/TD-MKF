using System;
using UnityEngine;

public class EntryPointMenu : MonoBehaviour
{
    [SerializeField] private Camera _cameraPrefab;
    [SerializeField] private GUIHolder _guiHolderPrefab;
    
    private EventBus _eventBus;
    private Camera _camera;
    private GUIHolder _guiHolder;

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

        DialogManager.ShowDialog<MenuDialog>();
    }
}
