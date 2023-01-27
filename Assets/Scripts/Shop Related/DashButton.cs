using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
{
    Button thisButton;
    [SerializeField] GameObject shopUI;
    private void OnEnable()
    {
        thisButton = this.GetComponent<Button>();
    }
    public void BuyDash()
    {
        CurrentStats.current.haveDash = true;
        thisButton.interactable = false;

        ShopButtonManager.isShopOpen = false;
        shopUI.SetActive(false);
        Time.timeScale = 1f;

        GameEvents.current.DashBough();
    }
}
