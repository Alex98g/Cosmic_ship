
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrab : XRGrabInteractable
{
    public bool KeepSocketPose = false;
    public bool grabbable = true;

    public void Start()
    {
        interactionManager = FindObjectOfType<XRInteractionManager>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        MatchAttachmentPoints(args.interactor);
        
    }
    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool isDirect = interactor is XRDirectInteractor || (KeepSocketPose && (interactor is XRSocketInteractor));
        attachTransform.position = isDirect ? interactor.attachTransform.position : transform.position;
        attachTransform.rotation = isDirect ? interactor.attachTransform.rotation : transform.rotation;
    }
    
}
