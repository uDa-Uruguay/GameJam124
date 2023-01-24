using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTextListener : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        //GameEvents.current += activateText;
    }

    private void activateText()
    {
    }
}
