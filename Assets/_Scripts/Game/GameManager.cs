using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using _Scripts.Game.GameField.UI;
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

            List<GameFieldNodeData> nodeDataList = new List<GameFieldNodeData>();
            foreach (var validCoord in listOfValidCoords)
            {
                var data = new GameFieldNodeData
                {
                    index = validCoord,
                    state = _matrix.GetNodeState(validCoord)
                };

                nodeDataList.Add(data);
            }
            
            gameField.ChangeFieldState(nodeDataList);

            foreach (var validCoord in listOfValidCoords)
            {
                Debug.Log($"--->> [{validCoord.x},{validCoord.y}] ");
            }

        });
    }
}
