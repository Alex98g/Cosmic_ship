using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryMenu : MonoBehaviour
{
    private XRController Controller;
    private bool ButtonState = false;
    private bool SelectButtonState = false;
    private bool menuOpened = false;

    public int selected = 0;
    public Material Selected;
    public Material Default;
    public List<GameObject> models;
    public List<GameObject> prefabs;
    void Start()
    {
        Controller = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool buttonPressed;
        bool buttonSelectPressed;
        Controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed);
        Controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out buttonSelectPressed);

        if (buttonPressed != ButtonState)
        {
            ButtonState = buttonPressed;
            if(ButtonState)
                OnPress();
        }
        if (buttonSelectPressed != SelectButtonState)
        {
            SelectButtonState = buttonSelectPressed;
            if (SelectButtonState)
                OnPressSelect();
        }
    }

    void OnPressSelect()
    {
        if(menuOpened)
        {
            
            foreach (Renderer renderer in models[selected].GetComponentsInChildren<Renderer>())
            {
                renderer.material = Default;
            }
            selected++;
            selected %= prefabs.Count;
            foreach (Renderer renderer in models[selected].GetComponentsInChildren<Renderer>())
            {
                renderer.material = Selected;
            }
        }
    }

    void OnPress()
    {
        menuOpened = !menuOpened;
        if(menuOpened)
        {
            
           prefabs[selected].SetActive(false);

            foreach (GameObject model in models)
                model.SetActive(true);

        }
        else
        {
            prefabs[selected].SetActive(true);
            foreach (GameObject model in models)
                model.SetActive(false);
        }

    }
}
