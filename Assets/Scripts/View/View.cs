using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countTakenFromPool;
    [SerializeField] private TextMeshProUGUI _countCreatedInfo;
    [SerializeField] private TextMeshProUGUI _activeBombInfo;
    [SerializeField] private TextMeshProUGUI _activeCubeInfo;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += ShowInfo;
        _bombSpawner.Spawned += ShowInfo;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= ShowInfo;
        _bombSpawner.Spawned -= ShowInfo;
    }

    private void ShowInfo()
    {
        UpdateBombInfo();
        UpdateCubeInfo();
        UpdateCreatedObjects();
        UpdateCountTakenFromPool();
    }

    private string UpdateBombInfo()
    {
        return _activeBombInfo.text = _bombSpawner.CountActiveInPool().ToString();
    }

    private string UpdateCubeInfo()
    {
        return _activeCubeInfo.text = _cubeSpawner.CountActiveInPool().ToString();
    }

    private string UpdateCreatedObjects()
    {
        return _countCreatedInfo.text = (_cubeSpawner.CountAllInPool() + _bombSpawner.CountAllInPool()).ToString();
    }

    private string UpdateCountTakenFromPool()
    {
        return _countTakenFromPool.text = (_bombSpawner.Count + _cubeSpawner.Count).ToString();
    }
}