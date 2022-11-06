using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private HUD hud;

    private void OnEnable()
    {
        GameManager.NoOfMovesChanged += NoOfMovesChanged;
    }

    private void OnDisable()
    {
        GameManager.NoOfMovesChanged -= NoOfMovesChanged;
    }

    private void NoOfMovesChanged(int moves)
    {
        hud.SetMoves(moves.ToString());
    }
}
