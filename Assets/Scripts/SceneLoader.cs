using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Загрузить сцену, например, "SampleScene"
    void Start()
    {
        LoadScene("SampleScene");
    }

	//Начать асинхронную загрузку сцены по имени
	public void LoadScene (string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync ( string sceneName )
    {
        AsyncOperation LoadingScene = SceneManager.LoadSceneAsync(sceneName);
        
        //Пока не загрузился уровень
        while ( !LoadingScene.isDone )
        {
            //Прогресс загрузки для UI
            int progress = Mathf.RoundToInt(LoadingScene.progress * 100);
			
            //Отображение прогресса в консоль отладки
            //Debug.Log(LoadingScene.progress);

            yield return null;
        }
    }
}
