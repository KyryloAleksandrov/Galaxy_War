using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    [SerializeField] private MapConfig MapConfig;
    [SerializeField] private UnitConfig UnitConfig;
    void Awake()
    {
        ProjectContext.Instance.Initialize(MapConfig, UnitConfig);
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
