using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameField gameField;

    private void Start()
    {
        gameField.InitGameField((i, i1) =>
        {
            Debug.Log($"--->> [{i},{i1}] ");
        });
    }
}
