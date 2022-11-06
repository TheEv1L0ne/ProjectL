using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Scripts.Game.GameField;
using _Scripts.Game.GameField.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameField gameField;

    private GameFieldMatrix _matrix;
    private ClickPattern _clickPattern;

    private void Start()
    {
        _matrix = new GameFieldMatrix();
        _clickPattern = new ClickPattern();
        
        gameField.InitGameField(FieldClicked);

        InitGame();
    }

    private void FieldClicked(int i, int j)
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
    }

    private void InitGame()
    {
        for (int i = 0; i < 20; i++)
        {
            var x = Random.Range(0, _matrix.RowsCount);
            var y = Random.Range(0, _matrix.ColumnsCount);
            
            FieldClicked(x,y);
        }
    }
}
