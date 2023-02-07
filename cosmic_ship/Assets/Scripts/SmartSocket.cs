
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SmartSocket : XRSocketInteractor
{
    public GameObject targetObject = null;
    public bool disableCollider;
    public bool freeze;

    public GameObject selectedObject;
    // Start is called before the first frame update
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
       
        var gameObject = args.interactableObject.transform.gameObject;
      
        base.OnSelectEntering(args);
        selectedObject = gameObject;
        if (freeze)
        {
            gameObject.GetComponent<OffsetGrab>().interactionLayers = 4;
        }
        if (disableCollider)
        {
            foreach (var collider in args.interactableObject.colliders)
            {
                collider.isTrigger = true;
            }
        }
            
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        var gameObject = args.interactableObject.transform.gameObject;

        base.OnSelectExiting(args);
        selectedObject = null;
        if (disableCollider)
        {
            foreach (var collider in args.interactableObject.colliders)
            {
                collider.isTrigger = false;
            }
        }
    }   
    
}
