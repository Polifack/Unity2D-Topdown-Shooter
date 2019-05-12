using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public string InteractableName;
    public GameObject InteractTextGO;
    public GameObject InteractImageGO;
    public Animator Anim;

    private TextMeshProUGUI inetractableText;

    public void SetInteractableName(string n)
    {
        inetractableText = InteractTextGO.GetComponent<TextMeshProUGUI>();
        inetractableText.text = n;
    }

    public void StartInteractable()
    {
        Anim.SetBool("isActive", true);
    }
    public void EndInteractable()
    {
        Anim.SetBool("isActive", false);
    }

    private void Awake()
    {
        inetractableText = InteractTextGO.GetComponent<TextMeshProUGUI>();
        inetractableText.text = InteractableName;
    }
}
