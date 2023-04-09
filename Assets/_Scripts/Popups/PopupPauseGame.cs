using _Scripts.Game.GameField;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class PopupPauseGame : PopupBase
{
    #region Editor References

    [SerializeField] private Button resumeGameBtn;
    [SerializeField] private Button startPlusGameBtn;
    [SerializeField] private Button startXGameBtn;
    [SerializeField] private Button startLGameBtn;
    [SerializeField] private Button startTGameBtn;
    [SerializeField] private Button startHGameBtn;
    [SerializeField] private Button startOGameBtn;

    [Space]
    [SerializeField] private TMP_InputField numberOfMoves;

    #endregion

    #region Privates

    private int _numberOfMoves;

    #endregion
    
    private void Awake()
    {
        //Making sure that only integer numbers can be used
        numberOfMoves.inputType = TMP_InputField.InputType.Standard;
        numberOfMoves.contentType = TMP_InputField.ContentType.IntegerNumber;
        
        resumeGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PLAYING);
            ObserverManager.Notify(ODType.Game);

            DestroyPopup();
        });

        startPlusGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new PlusPattern());
        });
        
        startXGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new XPattern());
        });
        
        startLGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new LPattern());
        });
        
        startHGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new HPattern());
        });
        
        startTGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new TPattern());
        });
        
        startOGameBtn.onClick.AddListener(() =>
        {
            SetGameWithPattern(new OPattern());
        });
        
        numberOfMoves.onValueChanged.AddListener(arg =>
        {
            _numberOfMoves = int.Parse(arg);
        });
    }

    private void SetGameWithPattern(ClickPattern pattern)
    {
        ObserverManager.AddData("state", GameState.RESTART);
        ObserverManager.AddData("pattern", pattern);
        ObserverManager.AddData("numberOfMoves", _numberOfMoves);
        ObserverManager.Notify(ODType.Game);

        DestroyPopup();
    }
}