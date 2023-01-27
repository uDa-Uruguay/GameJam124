using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Icon color sprites")]
    [SerializeField] Sprite noColorSP;
    [SerializeField] Sprite colorSP;
    private Image _image;

    [Header("Animacion MouseOver")]
    [SerializeField] private float scaleSize;
    [SerializeField] private float timeMouseOver;
    [SerializeField] private GameObject shopText;
    private Vector3 scaleSizeVector;

    [Header("Shop Manager")]
    [SerializeField] GameObject shopUI;
    public bool shopEnable = false;
    public static bool isShopOpen = false;

    private void Start()
    {
        _image = this.GetComponent<Image>();

        GameEvents.current.onShopAvailable += EnableShop;
        GameEvents.current.onShopDisable += DisableShop;
    }
    private void Update()
    {
        scaleSizeVector = new Vector3(scaleSize, scaleSize, transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen)
        {
            isShopOpen = false;
            shopUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void EnableShop()
    {
        shopEnable = true;
        _image.sprite = colorSP;
        StartCoroutine(PopUpAnimation());
    }

    public void DisableShop()
    {
        shopEnable = false;
        _image.sprite = noColorSP;
    }

    private IEnumerator PopUpAnimation()
    {
        LeanTween.scale(this.gameObject, scaleSizeVector, timeMouseOver).setEaseInOutSine();
        yield return new WaitForSeconds(timeMouseOver);
        LeanTween.scale(this.gameObject, new Vector3(2f, 2f, 2f), timeMouseOver).setEaseInOutSine();
    }

    // Mouse handlers
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Animacion MouseOver
        if (!shopEnable) return;
        LeanTween.scale(this.gameObject, scaleSizeVector, timeMouseOver).setEaseInOutSine();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Animacion MouseOver
        LeanTween.scale(this.gameObject, new Vector3(2f, 2f, 2f), timeMouseOver).setEaseInOutSine();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(shopEnable)
        {
            isShopOpen = true;
            Time.timeScale = 0f; // Esto pausa el juego.
            shopUI.SetActive(true);
            GameEvents.current.ShopDisable();
        }
    }
}
