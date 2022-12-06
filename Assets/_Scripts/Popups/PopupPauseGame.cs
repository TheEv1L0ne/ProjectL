using UnityEngine;
using UnityEngine.UI;

public class PopupPauseGame : PopupBase
{
    [SerializeField] private Button resumeGameBtn;
    [SerializeField] private Button restartGameBtn;

    private void Awake()
    {
        resumeGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PLAYING);
            ObserverManager.Notify(ODType.Game);
            
            Destroy(this.gameObject);
        });

        restartGameBtn.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.RESTART);
            ObserverManager.Notify(ODType.Game);

            Destroy(this.gameObject);
        });
    }
}