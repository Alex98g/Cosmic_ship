using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text destroyed;

    private void Start()
    {
        destroyed.text = "Meteors: " + CrossScene.destroyed.ToString();
    }
    public void Resetart()
    {
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
