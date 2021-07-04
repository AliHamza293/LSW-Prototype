using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> setences;
    public static DialogueManager instance;
    public Animator anim;
    void Start()
    {
        instance = this;
        setences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("IsOpen", true);
        Debug.Log("Start Conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        setences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            setences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(setences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = setences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter  in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    private void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
        Debug.Log("End Of Conversation");
        GameManager.instance.OpenQuestionPanel();
    }
}
