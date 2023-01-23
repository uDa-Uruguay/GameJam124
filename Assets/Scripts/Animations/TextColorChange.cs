using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float firstInterval = 0.1f;
    [SerializeField] private float secondInterval = 0.2f;

    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    private void OnEnable()
    {
        tmp.color = color1;

        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(firstInterval);
        tmp.color = color2;
        yield return new WaitForSeconds(secondInterval);
        tmp.color = color1;
        yield return new WaitForSeconds(firstInterval);
        StartCoroutine(Animation());
    }
}
