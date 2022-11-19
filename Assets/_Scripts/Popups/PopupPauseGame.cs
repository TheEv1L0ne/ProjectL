using System;
using System.Collections;
using System.Collections.Generic;
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
            ObserverManager.Notify(GameState.PLAYING.ToString(), ODType.Game);
            Destroy(this.gameObject);
        });

        restartGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.Notify(GameState.RESTART.ToString(), ODType.Game);
            Destroy(this.gameObject);
        });
    }
}