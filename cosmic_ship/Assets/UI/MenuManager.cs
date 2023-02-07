using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Sure_Exit;
    public GameObject Sure_Restart;
    public GameObject Settings;
    public GameObject CanvasMenu;
	public GameObject XROrigin;
    public GameObject RightHandControlle;
    public GameObject RightHandControllerMenu;
    public GameObject CanvasohjMenu;
    public AudioSource sound_background;
    public List<AudioSource> sound_effect;
    public Slider MVolume;
    public Slider EVolume;
    private XRController Controller;
    private bool ButtonState = false;
	public bool menuOpened = false;
    public Vector3 vector3;
	
	

    void Start()
    {
        Controller = GetComponent<XRController>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
		if (menuOpened == false)
		{
			bool buttonPressed;
        
			Controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);
			if (buttonPressed != ButtonState)
			{
				ButtonState = buttonPressed;
				if (ButtonState)
				{
					OnPress();
					
				}
					
			}
		}  
    }
    void OnPress()
    {
        vector3 = XROrigin.transform.localPosition;
        CanvasohjMenu.SetActive(true);
        XROrigin.transform.localPosition = new Vector3(-7.773147e-10f, 0, 2.5f);
        menuOpened = true;
        RightHandControllerMenu.SetActive(true);
        RightHandControlle.SetActive(false);
    }
        public void onSureExit()
    {
        Settings.SetActive(false);
        CanvasMenu.SetActive(false);
        Sure_Restart.SetActive(false);
        Sure_Exit.SetActive(true);
    }

    public void onSureRestart()
    {
        Menu.SetActive(false);
        Settings.SetActive(false);
        Sure_Exit.SetActive(false);
        Sure_Restart.SetActive(true);
    }

    public void onSettings()
    {
        CanvasMenu.SetActive(false);
        Sure_Exit.SetActive(false);
        Sure_Restart.SetActive(false);
        Settings.SetActive(true);
    }
    
    public void yesExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void noExit()
    {
        Sure_Exit.SetActive(false);
        Sure_Restart.SetActive(false);
        Settings.SetActive(false);
        CanvasMenu.SetActive(true);
    }
    public void toMainMenu()
    {
        Sure_Exit.SetActive(false);
        Sure_Restart.SetActive(false);
        Settings.SetActive(false);
        CanvasMenu.SetActive(true);
    }
	public void BtnBack()
    {
        CanvasohjMenu.SetActive(false);
		menuOpened = false;
        RightHandControlle.SetActive(true);
        RightHandControllerMenu.SetActive(false);
        XROrigin.transform.localPosition = vector3;
        XROrigin.SetActive(true);

		
    }
	

    public void yesRestart()
    {
        RightHandControlle.SetActive(true);
        RightHandControllerMenu.SetActive(false);
        SceneManager.LoadScene("Main");
    }
    public void noRestart()
    {
        Sure_Exit.SetActive(false);
        Sure_Restart.SetActive(false);
        Settings.SetActive(false);
        CanvasMenu.SetActive(true);
    }

    public void onEnablebackground()
    {
        MVolume.onValueChanged.AddListener(delegate { changeVolumebackground(MVolume.value); });
    }
    public void changeVolumebackground(float sliderValue)
    {
        sound_background.volume = sliderValue;

    }
    public void changeVolumeeffect()
    {
        EVolume.onValueChanged.AddListener(delegate { changeVolumebackground(EVolume.value); });
    }
    public void changeVolumeeffect(float sliderValue)
    {
        for (int i = 0; i < sound_effect.Count; i++)
        {
            sound_effect[i].volume = sliderValue;
        }

    }
}

