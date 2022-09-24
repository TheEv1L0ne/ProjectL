using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameField gameField;

    private GameFieldMatrix _matrix;
    private ClickPattern _clickPattern;

    private void Start()
    {
        _matrix = new GameFieldMatrix();
        _clickPattern = new ClickPattern();
        
        gameField.InitGameField((i, j) =>
        {
            var x = i;
            var y = j;

            var offsets = _clickPattern.ListOfCoordsOffset();
            var listOfCoords = offsets.Select(offset => new Vector2Int(x + offset.x, y + offset.y)).ToList();
            var listOfValidCoords = _matrix.GetValidFields(listOfCoords);
            _matrix.SetNodeStates(listOfValidCoords);

            foreach (var variablCoord in listOfValidCoords)
            {
                Debug.Log($"--->> [{variablCoord.x},{variablCoord.y}] ");
            }

        });
    }
}
