using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        LoadScene(1);
    }

    public void LoadScene(int id)
    {
        StartCoroutine(LoadSceneAsync(id));
    }

    IEnumerator LoadSceneAsync(int id)
    {
        var LoadingScene = SceneManager.LoadSceneAsync(id);

        while (!LoadingScene.isDone)
        {
            var progress = Mathf.RoundToInt(LoadingScene.progress * 100);
            yield return null;
        }
    }
}
