using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{
    float timer = 0;

    IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(2); //wait for animation
        gameObject.transform.Find("Dialogue").gameObject.SetActive(true);
        yield return null;
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Siren");
        StartCoroutine(StartDialog());
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (gameObject.transform.Find("Dialogue").gameObject.activeSelf == false && timer > 2.2f) // after dialog has ended start next level
            FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>().LoadNextLevel();
    }
}
