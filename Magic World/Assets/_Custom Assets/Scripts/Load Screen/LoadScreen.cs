using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    public Slider loadBar;

    public bool isLoading = false;
    public float loadAmount = 1;

    public IEnumerator LoadLevel(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        isLoading = false;

        while (!operation.isDone) {
            loadBar.value = operation.progress/0.9f;

            yield return null;
        }

        isLoading = true;
    }
    


}
