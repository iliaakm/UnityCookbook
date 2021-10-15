using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        LoadSceneAsync();
    }

    void LoadSceneAsync()
    {
        var operation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        print("Starting load");
        operation.allowSceneActivation = false;
        StartCoroutine(WaitForLoading(operation));
    }

    IEnumerator WaitForLoading(AsyncOperation operation)
    {
        while(operation.progress < 0.9f)
        {
            yield return null;
        }

        print("Loading Complete");
        operation.allowSceneActivation = true;
        yield return new WaitForSeconds(2);
        UnloadSceneAsync();
    }

    void UnloadSceneAsync()
    {
        var unloadOperation = SceneManager.UnloadSceneAsync("Game");
        StartCoroutine(WaitForUnloading(unloadOperation));
    }

    IEnumerator WaitForUnloading(AsyncOperation operation)
    {
        yield return new WaitUntil(() => operation.isDone);

        Resources.UnloadUnusedAssets();
    }
}
