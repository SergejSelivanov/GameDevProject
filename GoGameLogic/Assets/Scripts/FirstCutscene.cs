using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{
    float timer = 0;
    // Start is called before the first frame update

    IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(2);
        gameObject.transform.Find("Dialogue").gameObject.SetActive(true);
        yield return null;
    }

    void Start()
    {
        StartCoroutine(StartDialog());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (gameObject.transform.Find("Dialogue").gameObject.activeSelf == false && timer > 2.2f)
            FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>().LoadNextLevel();
    }
}
