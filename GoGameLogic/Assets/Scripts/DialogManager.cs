﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
   // public Animator animator;
    [TextArea(3, 10)]
    public string[] sentencesInput;

    public Text dialogText;
    public GameObject dialog;

    private Queue<string> sentences;

    void Start()
    {
        Time.timeScale = 0.99f;
        sentences = new Queue<string>();
        foreach (string sentence in sentencesInput)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            //Debug.Log("kuku");
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogText.text = sentence;
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
           // yield return new WaitForSeconds(0.01f);
            yield return null;
        }
    }

    void EndDialog()
    {
        //animator.SetBool("IsClosed", true);
        dialog.SetActive(false);
        Time.timeScale = 1;
       // if (SceneManager.GetActiveScene().name == "Cutscene_outsidebank")

    }

}
