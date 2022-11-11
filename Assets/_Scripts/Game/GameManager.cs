using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using _Scripts.Game.GameField.UI;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>, IObserver
{
    #region Editor References

    [SerializeField] private GameField gameField;

    #endregion

    #region Private Fields

    private GameFieldMatrix _matrix;
    private ClickPattern _clickPattern;
    private int _numberOfMoves = 20;
    private GameState _state;

    #endregion

    private void OnEnable()
    {
        ObserverManager.Attach(Instance);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(Instance);
    }

    private void Start()
    {
        _matrix = new GameFieldMatrix();
        _clickPattern = new ClickPattern();
        
        gameField.InitGameField(FieldClicked);

        InitGame();
    }

    private void FieldClicked(int i, int j)
    {
        if(_state != GameState.PLAYING) return;
        
        if (_numberOfMoves <= 0) return;
        
        FieldState(i,j);

        Debug.Log($"_numberOfMoves --->> {_numberOfMoves}");
        _numberOfMoves--;
            
        ObserverManager.Notify(new ODType[]{ODType.UI}, _numberOfMoves.ToString());
            
        if (_matrix.IsSolved())
        {
            //TODO: SHOW FINISH WINDOW
            Debug.Log($"WIN");
        }
        else if (_numberOfMoves == 0)
        {
            //TODO: SHOW FAILED WINDOW
            Debug.Log($"LOSE");
        }
    }

    private void FieldState(int i, int j)
    {
        var x = i;
        var y = j;

        //Update game logic part 
        var offsets = _clickPattern.ListOfCoordsOffset();
        var listOfCoords = offsets.Select(offset => new Vector2Int(x + offset.x, y + offset.y)).ToList();
        var listOfValidCoords = _matrix.GetValidFields(listOfCoords);
        _matrix.SetNodeStates(listOfValidCoords);

        //Update Interface part
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
        _state = GameState.PLAYING;
        
        for (int i = 0; i < _numberOfMoves; i++)
        {
            var x = Random.Range(0, _matrix.RowsCount);
            var y = Random.Range(0, _matrix.ColumnsCount);
            
            FieldState(x,y);
        }
    }

    public void UpdateState(ODType[] type, string data)
    {
        if (!type.Contains(ODType.Game)) return;

        _state = Enum.Parse<GameState>(data);
    }
}
