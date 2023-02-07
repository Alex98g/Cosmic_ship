using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject CanvasMenu;
    public Slider slider;
    public TMP_Text progressText;
    public void LoadLever(int sceneIndex)
    {
        CanvasMenu.SetActive(false);
        loadingScreen.gameObject.SetActive(true);
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

