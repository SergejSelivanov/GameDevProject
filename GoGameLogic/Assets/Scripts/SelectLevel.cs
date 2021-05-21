using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public void Select(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
