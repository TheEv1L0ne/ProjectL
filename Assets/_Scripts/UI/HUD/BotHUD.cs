using _Scripts.Popups;
using UnityEngine;
using UnityEngine.UI;

public class BotHUD : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button undoButton;
    
    private void Awake()
    {
        settingsButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PAUSE);
            
            ObserverManager.AddData("popupParams", new PauseGameParams());
            ObserverManager.AddData("popupShowAction", PopupAction.Later);
            
            ObserverManager.Notify( ODType.Game, ODType.UI);
        });
        
        undoButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("undo");
            ObserverManager.Notify( ODType.Game);
        });
    }
}
