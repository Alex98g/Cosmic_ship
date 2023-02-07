using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject Menu;
    public GameObject Sure;
    public GameObject Settings;
    public GameObject Loader;
    public AudioSource sound_background;
    public Slider MVolume;
    public Slider EVolume;

    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;

    public void onSureExit()
    {
        Menu.SetActive(false);
        Settings.SetActive(false);
        Sure.SetActive(true); 
    }

    public void onSettings()
    {
        Menu.SetActive(false);
        Sure.SetActive(false);
        Settings.SetActive(true);
    }

    public void yesExit()
    {
        Application.Quit();
    }
    public void noExit()
    {
        Settings.SetActive(false);
        Sure.SetActive(false);
        Menu.SetActive(true);
    }
    public void toMainMenu()
    {
        Settings.SetActive(false);
        Sure.SetActive(false);
        Menu.SetActive(true);
    }


    public void onEnablebackground()
    {
        MVolume.onValueChanged.AddListener(delegate { changeVolumebackground(MVolume.value); });
    }
    public void changeVolumebackground(float sliderValue)
    {
        sound_background.volume = sliderValue;

    }
    
    public void HideMainMenu()
    {
        CanvasMenu.gameObject.SetActive(false);
    }

    public void LoadLever(int sceneIndex)
    {
        CanvasMenu.SetActive(false);
        Settings.SetActive(false);
        loadingScreen.SetActive(true);
        progressText.text = "0 %";
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSeconds(5.0f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        // loadingScreen.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log(progress);
            progressText.text = (progress * 100f).ToString() + "%";
            yield return null;

        }

    }
}
