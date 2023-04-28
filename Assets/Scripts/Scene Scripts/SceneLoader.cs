using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        LoadScene("SampleScene");
    }

	public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync ( string sceneName )
    {
        AsyncOperation LoadingScene = SceneManager.LoadSceneAsync(sceneName);
        
        while ( !LoadingScene.isDone )
        {
            int progress = Mathf.RoundToInt(LoadingScene.progress * 100);
            yield return null;
        }
    }
}