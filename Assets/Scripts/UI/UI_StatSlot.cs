using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private UI ui;

    public string statName;
    public StatType statType;
    public TextMeshProUGUI statValueText;
    public TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDesCription;
    private void OnValidate()
    {
        gameObject.name = "Stat -" + statName;

        if (statNameText != null)
        {
            statNameText.text = statName;
        }
    }
    private void Start()
    {
        UpdateStatValueUI();
        ui = GetComponentInParent<UI>();
    }
    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStat(statType).GetValue().ToString();

            if (statType == StatType.health)
            {
                statValueText.text = playerStats.GetStat(statType).GetValue().ToString();
            }

            if(statType == StatType.damage ) 
            {
                statValueText .text =( playerStats.damage.GetValue() + playerStats .strength.GetValue()).ToString();
            }
            if (statType == StatType.critPower)
                statValueText.text = (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();

            if (statType == StatType.critChance)
                statValueText.text = (playerStats.critChance.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType == StatType.evasion)
                statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();

            if (statType == StatType.magicRes)
                statValueText.text = (playerStats.magicResistance.GetValue() + (playerStats.intelligence.GetValue() * 3)).ToString();

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowStatToolTip(statDesCription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideStatToolTip();
    }
}
