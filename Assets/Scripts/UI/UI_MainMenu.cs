using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public string sceneName = "MainScene";
    public GameObject continueButton;
    public UI_FadeScreen fadeScreen;
    private void Start()
    {
        if(SaveManager.instance.HasSaveData () == false)
            continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        // SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
       // SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
    }
    
    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);

    }
}
