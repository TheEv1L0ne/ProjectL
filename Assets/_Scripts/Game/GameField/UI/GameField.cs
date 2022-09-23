using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameField : MonoBehaviour
{

    [SerializeField] private GameFieldRow[] rows;
    
    private Action<int,int> _action;
    
    public void InitGameField(Action<int,int> action)
    {
        _action = action;
        foreach (var gameFieldRow in rows)
        {
            gameFieldRow.InitRows(OnFieldClicked);
        } 
    }

    private void OnFieldClicked(int x, int y)
    {
        _action.Invoke(x, y);
    }
}
