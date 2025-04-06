using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats mystats => GetComponentInParent<CharacterStats>();
    private RectTransform myTransform;
    private Slider slider;
    private void Start()
    {
        myTransform = GetComponent<RectTransform>();

        slider = GetComponentInChildren<Slider>();

        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        slider.maxValue = mystats.GetMaxHealthValue();
        slider.value = mystats.currentHealth;

    }
    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        mystats.onHealthChanged += UpdateHealthUI;

    }
    private void OnDisable()
    {
        if (entity != null)
            entity.onFlipped -= FlipUI;

        if (mystats != null)
            mystats.onHealthChanged -= UpdateHealthUI;
    }
    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}
