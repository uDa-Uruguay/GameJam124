using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [Header("Y movement")]
    [SerializeField] private bool yMovementEnable = true;
    [SerializeField] private float upperY;
    [SerializeField] private float bottomY;
    //[SerializeField] private float timeMovement;
    [SerializeField] private float timeInterval;
    [SerializeField] private float secondTimeInterval;

    [Header("Stretch")]
    [SerializeField] private float normal;

    private void Awake()
    {
        if (yMovementEnable) StartCoroutine(AnimationY());
    }


    private IEnumerator AnimationY()
    {
        if (yMovementEnable)
        {
            LeanTween.moveLocalY(this.gameObject, bottomY, secondTimeInterval).setEaseInOutSine();
            yield return new WaitForSeconds(secondTimeInterval);
            LeanTween.moveLocalY(this.gameObject, upperY, timeInterval).setEaseInOutSine();
            yield return new WaitForSeconds(timeInterval);
            StartCoroutine(AnimationY());
        }
    }
}
