﻿using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum InteractiveType { PICKABLE, INTERACT_ONCE, INTERACT_MULTIPLE, INDIRECT };

    public bool                 isActive;
    public bool                 interactedWith;

    public InteractiveType      type;

    public string               inventoryName;
    public Sprite               inventoryIcon;

    public string               pickupText;
    public string               pickedupText;
    public string               requirementText;
    public string               interactText;
    public string               interactedText;


    public InteractableObject[] inventoryRequirements;
    public InteractableObject[] activationChain;
    public InteractableObject[] interactionChain;

    private Animator           animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        interactedWith = false;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Interact()
    {
        if (animator != null)
            animator.SetTrigger("Interacted");

        if (isActive)
        {
            ProcessActivationChain();
            ProcessInteractionChain();

            if (type == InteractiveType.INTERACT_ONCE)
            {
                GetComponent<Collider>().enabled = false;
            }
        }
        interactedWith = true;
    }

    private void ProcessActivationChain()
    {
        if (activationChain != null)
        {
            for (int i = 0; i < activationChain.Length; ++i)
                activationChain[i].Activate();
        }
    }

    private void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }
}
