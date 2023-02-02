using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargerBar : MonoBehaviour
{
    Image imageController;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public float maxCharge;

    private void Awake()
    {
        fill.color = gradient.Evaluate(1f);
        imageController = this.GetComponent<Image>();

        StartCoroutine(getMaxHealth());
    }

    public void UpdateHealth(float Health)
    {
        //slider.value = Health + 0.3f;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public IEnumerator getMaxHealth()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
