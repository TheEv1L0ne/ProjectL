using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers.ObserverPattern;
using UnityEngine;
using UnityEngine.UI;

public class PopupPauseGame : PopupBase
{
    [SerializeField] private Button resumeGameBtn;
    [SerializeField] private Button restartGameBtn;

    private void Awake()
    {
        resumeGameBtn.onClick.AddListener(() =>
        {
            var observerData = new ObserverData();
            observerData.AddData("state", GameState.PLAYING);
            ObserverManager.Notify(observerData.Data, ODType.Game);
            
            Destroy(this.gameObject);
        });

        restartGameBtn.onClick.AddListener(() =>
        {
            var observerData = new ObserverData();
            observerData.AddData("state", GameState.RESTART);
            ObserverManager.Notify(observerData.Data, ODType.Game);

            Destroy(this.gameObject);
        });
    }
}