using _Scripts.Game.GameField;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopupPauseGame : PopupBase
{
    [SerializeField] private Button resumeGameBtn;
    [SerializeField] private Button startPlusGameBtn;
    [SerializeField] private Button startXGameBtn;

    private void Awake()
    {
        resumeGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PLAYING);
            ObserverManager.Notify(ODType.Game);
            
            Destroy(this.gameObject);
        });

        startPlusGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.RESTART);
            ObserverManager.AddData("pattern", new PlusPattern());
            ObserverManager.Notify(ODType.Game);

            Destroy(this.gameObject);
        });
        
        startXGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.RESTART);
            ObserverManager.AddData("pattern", new XPattern());
            ObserverManager.Notify(ODType.Game);

            Destroy(this.gameObject);
        });
    }
}