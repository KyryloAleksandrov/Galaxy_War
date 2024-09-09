using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    [SerializeField] private MapConfig MapConfig;
    [SerializeField] private UnitConfig UnitConfig;
    [SerializeField] private LayerMasksConfig layerMasksConfig;
    [SerializeField] private PlayerConfig playerConfig;
    void Awake()
    {
        ProjectContext.Instance.Initialize(MapConfig, UnitConfig, layerMasksConfig, playerConfig);
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
