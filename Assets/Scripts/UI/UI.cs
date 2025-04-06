using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("End Screen")]
    public UI_FadeScreen fadeScreen;
    public GameObject endText;
    public GameObject restartButton;
    [Space]

    public GameObject characterUI;
    public GameObject craftUI;
    public GameObject optionUI;
    public GameObject skillTreeUI;
    public GameObject inGameUI;

    public UI_SkillToolTip skillToolTip;
    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_CraftWindow craftWindow;

    public UI_VolueSlider[] volumeSetting;


    private void Awake()
    {
        SwitchTo(skillTreeUI); //we need this to assign on skill tree slots before we assign events on skill scripts
        fadeScreen.gameObject.SetActive(true);
    }

    void Start()
    {
        SwitchTo(inGameUI);
        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        // itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchWithKeyTo(characterUI);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchWithKeyTo(inGameUI);
        }

    }
    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;// we need this to keep fade screen game object active

            if (fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }
        if (_menu != null)
        {
            AudioManager.instance.PlaySFX(5, null);
            _menu.SetActive(true);
        }
        if (GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
    }
    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }
    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }

        SwitchTo(inGameUI);
    }

    public void SwitchOnEndScreen()
    {

        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCorutione());
    }

    IEnumerator EndScreenCorutione()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volunmeSetting)
        {
            foreach (UI_VolueSlider item in volumeSetting)
            {
                if (item.parametr == pair.Key)
                    item.LoadSlider(pair.Value);
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volunmeSetting.Clear();

        foreach (UI_VolueSlider item in volumeSetting)
        {
            _data.volunmeSetting.Add(item.parametr, item.slider.value);
        }
    }
}
