using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using _Scripts.Game.GameField.UI;
using _Scripts.Helpers.ObserverPattern;
using Newtonsoft.Json.Linq;
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
        gameField.ResetFieldRows();

        InitGame();
    }

    private void Reset()
    {
        _matrix = new GameFieldMatrix();
        _clickPattern = new ClickPattern();
        gameField.ResetFieldRows();
        InitGame();
    }

    private void FieldClicked(int i, int j)
    {
        if (_state != GameState.PLAYING) return;

        if (_numberOfMoves <= 0) return;

        FieldState(i, j);

        _numberOfMoves--;
        var observerData = new ObserverData();
        observerData.AddData("moves", _numberOfMoves);
        ObserverManager.Notify(observerData.Data, ODType.UI);

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

    private void InitGame()
    {
        _state = GameState.PLAYING;
        _numberOfMoves = 20;

        for (int i = 0; i < _numberOfMoves; i++)
        {
            var x = Random.Range(0, _matrix.RowsCount);
            var y = Random.Range(0, _matrix.ColumnsCount);

            FieldState(x, y);
        }
        
        var observerData = new ObserverData();
        observerData.AddData("moves", _numberOfMoves);
        ObserverManager.Notify(observerData.Data, ODType.UI);
    }

    public void UpdateState(JObject data, params object[] receivers)
    {
        if (!receivers.Contains(ODType.Game)) return;

        if(data.TryGetValue("state", out var state))
        {
            switch ((int)state)
            {
                case (int)GameState.RESTART:
                    Reset();
                    break;
                case (int)GameState.PAUSE:
                    _state = GameState.PAUSE;
                    break;
                case (int)GameState.WIN:
                    break;
                case  (int)GameState.PLAYING:
                    _state = GameState.PLAYING;
                    break;;
            }
        }
        
    }
}