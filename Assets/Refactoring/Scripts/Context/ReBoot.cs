using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReBoot : MonoBehaviour
{
    void Awake()
    {
        ReProjectContext.Instance.Initialize();
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("ReGameplay", LoadSceneMode.Single);
    }
}
