using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private bool _loading = false;

    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Main Camera")
        Load();
    }

    public void Load()
    {
        if (_loading) return;
        _loading = true;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
