using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : Interactable
{
    [Header("Datos del NPC")]
    public string NPCName;
    [TextArea(3, 10)]
    public string[] Dialog;

    [Header("Parametros del elemento interactuable")]
    public GameObject InteractElement;
    public Transform InteractInstanciationPosition;
    private float interactElementCounter = 0f;
    private bool interactShowing = false;
    private GameObject interactElement;

    [Header("Parametros del elemento de dialogo")]
    public GameObject DialogElement;
    public Transform DialogInstanciationPosition;
    private float dialogElementCounter = 0f;
    private bool dialogShowing = false;
    private GameObject dialogElement;

    public override void onRaycastEnter()
    {
        if (dialogShowing)
        {
            dialogElementCounter = Timeout;
            return;
        }

        if (interactElement == null)
        {
            interactElement = Instantiate(InteractElement, InteractInstanciationPosition);
        }

        InteractableManager interactableManager = interactElement.gameObject.GetComponentInChildren<InteractableManager>();
        interactableManager.SetInteractableName(NPCName);
        interactableManager.StartInteractable();

        interactShowing = true;

        interactElementCounter = Timeout;
        
        
    }
    public override void onInteract()
    {
        if (dialogElement == null)
        {
            dialogElement = Instantiate(DialogElement, DialogInstanciationPosition);
        }

        DialogueManager dialogManager = dialogElement.gameObject.GetComponentInChildren<DialogueManager>();

        if (!dialogShowing)
        {
            dialogShowing = true;
            dialogElementCounter = Timeout;
            stopInteractableElement();
            dialogManager.StartDialogue(Dialog);
        }
        else
        {
            if (dialogManager.DisplayNexSentence()) dialogShowing = false;
        }

    }

    private void stopInteractableElement()
    {
        interactElement.gameObject.GetComponentInChildren<InteractableManager>().EndInteractable();
        interactShowing = false;
    }
    private void stopDialogElement()
    {
        dialogElement.gameObject.GetComponentInChildren<DialogueManager>().EndDialogue();
        dialogShowing = false;
    }

    private void Awake()
    {
        if (InteractElement == null || DialogElement == null) throw new System.Exception("Elements can't be null");
    }
    private void FixedUpdate()
    {
        if (interactShowing && (interactElementCounter -= Time.deltaTime) < 0)
        {
            stopInteractableElement();
        }

        if (dialogShowing && (dialogElementCounter -= Time.deltaTime) < 0)
        {
            stopDialogElement();
        }
    }

}
