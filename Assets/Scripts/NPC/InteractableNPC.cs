using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class InteractableNPC : Interactable
{
    [Header("Datos del NPC")]
    public GameNPC npcData;

    [Header("Parametros del elemento interactuable")]
    public GameObject InteractElement;
    public Transform InteractInstanciationPosition;
    private float _interactableTimeout = 0f;
    private bool _interactableShowing = false;
    private GameObject _interactableElement;

    [Header("Parametros del elemento de dialogo")]
    public GameObject DialogElement;
    public Transform DialogInstanciationPosition;
    private float _dialogTimeout = 0f;
    private bool _dialogShowing = false;
    private GameObject _dialogElement;

    public override void onRaycastEnter()
    {
        if (_dialogShowing)
        {
            _dialogTimeout = Timeout;
            return;
        }

        if (_interactableElement == null)
        {
            _interactableElement = Instantiate(InteractElement, InteractInstanciationPosition);
        }

        InteractableManager interactableManager = _interactableElement.gameObject.GetComponentInChildren<InteractableManager>();
        interactableManager.SetInteractableName(npcData.Name);
        interactableManager.StartInteractable();

        _interactableShowing = true;

        _interactableTimeout = Timeout;
        
        
    }
    public override void onInteract()
    {
        if (_dialogElement == null)
        {
            _dialogElement = Instantiate(DialogElement, DialogInstanciationPosition);
        }

        DialogueManager dialogManager = _dialogElement.gameObject.GetComponentInChildren<DialogueManager>();

        if (!_dialogShowing)
        {
            _dialogShowing = true;
            _dialogTimeout = Timeout;
            stopInteractableElement();
            dialogManager.StartDialogue(npcData.Dialog);
        }
        else
        {
            if (dialogManager.DisplayNexSentence()) _dialogShowing = false;
        }

    }

    private void stopInteractableElement()
    {
        _interactableElement.gameObject.GetComponentInChildren<InteractableManager>().EndInteractable();
        _interactableShowing = false;
    }
    private void stopDialogElement()
    {
        _dialogElement.gameObject.GetComponentInChildren<DialogueManager>().EndDialogue();
        _dialogShowing = false;
    }

    private void Awake()
    {
        if (InteractElement == null || DialogElement == null)
        {
            Debug.LogWarning(" * Error: Interactable elements should not be null!");
            return;
        }
        GetComponent<SpriteRenderer>().sprite = npcData.defaultNpcSprite;
    }
    private void Start()
    {
        InitializeAnimations();
    }
    private void FixedUpdate()
    {
        if (_interactableShowing && (_interactableTimeout -= Time.deltaTime) < 0)
        {
            stopInteractableElement();
        }

        if (_dialogShowing && (_dialogTimeout -= Time.deltaTime) < 0)
        {
            stopDialogElement();
        }
    }
    private void InitializeAnimations()
    {
        npcData.Animator = gameObject.AddComponent<Animator>();

        PlayableGraph playableGraph = PlayableGraph.Create();
        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(playableGraph, npcData.IdleFront);
        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "idleFront", npcData.Animator);
        playableOutput.SetSourcePlayable(clipPlayable);
        playableGraph.Play();
    }
}
