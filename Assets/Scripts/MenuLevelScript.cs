using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelScript : MonoBehaviour {

    public void loadLevel()
    {
        StartCoroutine(LoadLevelAsync(1));
    }

    IEnumerator LoadLevelAsync(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

    }
}
