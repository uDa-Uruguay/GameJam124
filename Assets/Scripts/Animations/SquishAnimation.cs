using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishAnimation : MonoBehaviour
{
    private float originalScaleX;
    private float originalScaleY;

    [SerializeField] private float targetScaleX = 2.7f;
    [SerializeField] private float targetScaleY = 7.5f;
    [SerializeField] private float time = 1f;
    private Vector3 _targetVector;
    private Vector3 _originalVector;

    private void OnEnable()
    {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;

        _originalVector = new Vector3(originalScaleX, originalScaleY, transform.localScale.z);
        _targetVector = new Vector3(targetScaleX, targetScaleY, transform.localScale.z);

        StartCoroutine(startAnimation());
    }
    private void FixedUpdate()
    {
        _targetVector = new Vector3(targetScaleX, targetScaleY, transform.localScale.z);
    }

    private IEnumerator startAnimation()
    {
        LeanTween.scale(this.gameObject, _targetVector, time).setEaseOutSine();
        yield return new WaitForSeconds(time);
        LeanTween.scale(this.gameObject, _originalVector, time).setEaseInSine();
        yield return new WaitForSeconds(time);
        StartCoroutine(startAnimation());
    }
}
