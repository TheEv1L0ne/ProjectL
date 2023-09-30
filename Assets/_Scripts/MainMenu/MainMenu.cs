using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{ 
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI energyText;
    
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("startLevel");
            ObserverManager.Notify(ODType.Game);
        });
    }
}
