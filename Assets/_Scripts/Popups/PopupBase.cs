using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    public void DestroyPopup()
    {
        Destroy(this.gameObject);
    }
}