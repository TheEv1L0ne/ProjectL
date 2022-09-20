using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private Camera _UICamera;

    public Camera GameCamera => _gameCamera;
    
    protected override void OnAwake()
    {
        base.OnAwake();

        if (_gameCamera == null)
        {
            _gameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        
        if (_UICamera == null)
        {
            _UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        }
    }
}
