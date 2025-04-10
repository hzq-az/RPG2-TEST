using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillCost;


    public void ShowToolTip(string _skillDescription, string _skillName, int _price)
    {
        skillText.text = _skillDescription;
        skillName.text = _skillName;
        skillCost.text = "Cost: " + _price;

        AdjustPosition();

        gameObject.SetActive(true);
    }
    public void HideToolTip() => gameObject.SetActive(false);


}
