using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCutscene : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;
    public GameObject light;
    bool IsStarted = false;

    IEnumerator StartCutscene()
    {
        npc.GetComponent<Animator>().SetTrigger("IsReady"); //start npc animation
        FindObjectOfType<AudioManager>().Play("Running");
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<Animator>().applyRootMotion = true;
        player.GetComponent<Animator>().SetTrigger("IsReady"); //start player animation
        yield return new WaitForSeconds(2.9f);
        FindObjectOfType<AudioManager>().Play("Breaking");
        light.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
        yield return null;
    }

    void Update()
    {
        if (gameObject.transform.Find("Dialogue").gameObject.activeSelf == false && IsStarted == false) //if dialog is closed
        {
            IsStarted = true;
            StartCoroutine(StartCutscene());
        }
    }
}
