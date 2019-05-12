using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject sentenceTextGO;
    public GameObject interactableImageGO;
    public Animator animator;

    private Queue<string> sentences;
    private TextMeshProUGUI sentenceText;

    public void StartDialogue(string[] dialog)
    {
        interactableImageGO.SetActive(true);
        animator.SetBool("isActive", true);

        sentences.Clear();
        foreach (string sentence in dialog)
        {
            sentences.Enqueue(sentence);
        }

        //El primer Display First Sentence se ejecuta desde el animador
    }
    public void EndDialogue()
    {
        sentenceText.text = "";
        interactableImageGO.SetActive(false);
        animator.SetBool("isActive", false);
    }
    public bool DisplayNexSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return false;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;
        }
    }

    private void Awake()
    {
        sentenceText = sentenceTextGO.GetComponent<TextMeshProUGUI>();
        sentences = new Queue<string>();
    }

}