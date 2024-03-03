using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
     public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }

}

public class HapticFeedback : MonoBehaviour
{
    public Haptic hapticOnActivated;
    public Haptic hapticOnHoverEntered;
    public Haptic hapticOnHoverExited; 
    public Haptic hapticOnSelectEntered; 
    public Haptic hapticOnSelectExited; 
    
    
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticOnSelectEntered.TriggerHaptic);
        interactable.selectExited.AddListener(hapticOnSelectExited.TriggerHaptic);
        interactable.hoverEntered.AddListener(hapticOnHoverEntered.TriggerHaptic);
        interactable.hoverExited.AddListener(hapticOnHoverExited.TriggerHaptic);
            
    }

    

    
}
