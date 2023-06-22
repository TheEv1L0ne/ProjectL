using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingDots : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt;
    [SerializeField]
    private float intervalSeconds;
    private YieldInstruction intervalToWait;
    private string _cachedTxt;
    IEnumerator Start()
    {
        _cachedTxt = txt.text;
        if (intervalSeconds <= 0)
        {
            intervalToWait = new WaitForEndOfFrame();
        }
        else
        {
            intervalToWait = new WaitForSeconds(intervalSeconds);
        }
        while (true)
        {
            txt.text = " " + _cachedTxt + ".";
            yield return intervalToWait;
            txt.text = "  " + _cachedTxt + "..";
            yield return intervalToWait;
            txt.text = "   " + _cachedTxt + "...";
            yield return intervalToWait;
        }
    }
}
