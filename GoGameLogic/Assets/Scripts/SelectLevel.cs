using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button[] levelButtons;

    private void Awake()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().color = new Color(0.57f, 0.57f, 0.57f);

            }
        }
    }

    public void Select(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
