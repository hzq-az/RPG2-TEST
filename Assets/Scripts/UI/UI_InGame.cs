using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InGame : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider slider;
    [Header("Souls info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image throwswordImage;
    [SerializeField] private Image balckholeImage;
    [SerializeField] private Image flaskImage;
    //[SerializeField] private float dashCooldown;
    private SkillManager skills;
    private void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHealthChanged += UpdateHealthUI;
        }
        //dashCooldown = SkillManager.instance.dash.cooldown;
        skills = SkillManager.instance;
    }

    private void Update()
    {
        currentSouls.text = PlayerManager.instance.Getcurrency().ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift) && skills.dash.dashUnlocked)
            SetCoolDownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.Q) && skills.parry.parryUnlocked)
            SetCoolDownOf(parryImage);

        if (Input.GetKeyDown(KeyCode.F) && skills.crystal.crystalUnlocked)
            SetCoolDownOf(crystalImage);

        if (Input.GetKeyDown(KeyCode.Mouse1) && skills.sword.swordUnlocked)
            SetCoolDownOf(throwswordImage);

        if (Input.GetKeyDown(KeyCode.R) && skills.blackhole.blackHoleUnlocked)
            SetCoolDownOf(balckholeImage);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
            SetCoolDownOf(flaskImage);

        CheckCooldownOf(dashImage, skills.dash.cooldown);
        CheckCooldownOf(parryImage, skills.parry.cooldown);
        CheckCooldownOf(crystalImage, skills.crystal.cooldown);
        CheckCooldownOf(throwswordImage, skills.sword.cooldown);
        CheckCooldownOf(balckholeImage, skills.blackhole.cooldown);

        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;

    }
    private void SetCoolDownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    private void CheckCooldownOf(Image _image, float _coolDown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
    }
}
