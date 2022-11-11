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
           ObserverManager.Notify(new ODType[] {ODType.Game}, GameState.PLAYING.ToString());
           Destroy(this.gameObject);
        });
    }
}
