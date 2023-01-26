using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animacion MouseOver")]
    [SerializeField] private float scaleSize;
    [SerializeField] private float timeMouseOver;
    [SerializeField] private GameObject shopText;
    private Vector3 scaleSizeVector;

    private void Update()
    {
        scaleSizeVector = new Vector3(scaleSize, scaleSize, transform.localScale.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Animacion MouseOver
        LeanTween.scale(this.gameObject, scaleSizeVector, timeMouseOver).setEaseInOutSine();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Animacion MouseOver
        LeanTween.scale(this.gameObject, new Vector3(2f, 2f, 2f), timeMouseOver).setEaseInOutSine();
    }
}
