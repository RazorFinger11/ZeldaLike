using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAditiveScene : MonoBehaviour
{
    [SerializeField] string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}
