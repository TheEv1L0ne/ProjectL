using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotHUD : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button undoButton;

    public Button SettingsButton => settingsButton;
    public Button UndoButton => undoButton;
}
