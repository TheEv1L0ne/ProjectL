using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using _Scripts.Game.GameField.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>, IObserver
{
    #region Editor References

    [SerializeField] private GameField gameField;

    #endregion

    #region Private Fields

    private GameFieldMatrix _matrix;
    private ClickPattern _clickPattern;
    private int _numberOfMoves;
    private GameState _state;


    private List<Vector2Int> _movesPlayed =  new();

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
        gameField.InitGameField(FieldClicked);
        SetUpGame(new PlusPattern());
    }

    private void SetUpGame(ClickPattern pattern)
    {
        _matrix = new GameFieldMatrix();
        _clickPattern = pattern ?? new PlusPattern();
        gameField.ResetFieldRows();
        InitGame();
    }
    
    private void InitGame()
    {
        _state = GameState.PLAYING;
        _numberOfMoves = 20;
        
        var usedIds = new List<int>();
        
        for (var i = 0; i < _numberOfMoves; i++)
        {
            int x;
            int y;

            do
            {
                x = Random.Range(0, _matrix.RowsCount);
                y = Random.Range(0, _matrix.ColumnsCount);
            } while (usedIds.Contains(x * y));
            
            usedIds.Add(x*y);
            
            FieldState(x, y);
        }
        
        ObserverManager.AddData("moves", _numberOfMoves);
        ObserverManager.Notify( ODType.UI);
    }

    private void FieldClicked(int i, int j)
    {
        if (_state != GameState.PLAYING) return;

        if (_numberOfMoves <= 0) return;

        FieldState(i, j);
        _movesPlayed.Add(new Vector2Int(i,j));

        _numberOfMoves--;

        ObserverManager.AddData("moves", _numberOfMoves);
        ObserverManager.Notify( ODType.UI);

        if (_matrix.IsSolved())
        {
            //TODO: SHOW FINISH WINDOW
            Debug.Log($"WIN");
            _state = GameState.WIN;
        }
        else if (_numberOfMoves == 0)
        {
            //TODO: SHOW FAILED WINDOW
            Debug.Log($"LOSE");
            _state = GameState.LOSE;
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

    private void UndoMove()
    {
        if(_state != GameState.PLAYING) return;
        
        if (_movesPlayed.Count <= 0) return;
        
        var i = _movesPlayed[^1].x;
        var j = _movesPlayed[^1].y;
            
        FieldState(i, j);
        _numberOfMoves++;
            
        _movesPlayed.RemoveAt(_movesPlayed.Count-1);
            
        ObserverManager.AddData("moves", _numberOfMoves);
        ObserverManager.Notify( ODType.UI);
    }
    
    public void UpdateState( Dictionary<string, object> data, params object[] receivers)
    {
        if (!receivers.Contains(ODType.Game)) return;

        ClickPattern p = null;
        
        if (data.TryGetValue("pattern", out var pattern))
        {
            p = (ClickPattern)pattern;
        }

        if (data.TryGetValue("state", out var state))
        {
            _state = (GameState) (int) state;
            
            if(_state == GameState.RESTART)
                SetUpGame(p);
        }

        if (data.TryGetValue("undo", out _))
        {
            UndoMove();
        }
    }
}