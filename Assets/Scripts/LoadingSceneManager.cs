using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    public string sceneName;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    IEnumerator LoadAsynSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            time += Time.time;
            slider.value = time / 10f;
            if (time > 10)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
