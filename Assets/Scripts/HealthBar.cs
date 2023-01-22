using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public float maxHealth;

    private void Awake()
    {
        fill.color = gradient.Evaluate(1f);
    }
    private void Update()
    {
        slider.maxValue = maxHealth;
    }

    public void UpdateHealth(float Health)
    {
        slider.value = Health + 0.3f;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
