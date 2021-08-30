using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
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
        if (sentences.Count == 0) //if there is no more sentences
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) 
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter; //to type by one letter
            yield return null;
        }
    }

    void EndDialog()
    {
        dialog.SetActive(false);
        Time.timeScale = 1;
        //FindObjectOfType<MatchWidth>().GetBlackBars();
    }
}
