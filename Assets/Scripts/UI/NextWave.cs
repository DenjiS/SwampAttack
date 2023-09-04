using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _spawner.AllEnemySpawned += OnAllEnemiesSpawned;
        _button.onClick.AddListener(OnNextWaveButtonClicked);
    }

    private void OnDisable()
    {
        _spawner.AllEnemySpawned -= OnAllEnemiesSpawned;
        _button.onClick.RemoveListener(OnNextWaveButtonClicked);
    }

    private void OnAllEnemiesSpawned()
    {
        _button.gameObject.SetActive(true);
    }

    private void OnNextWaveButtonClicked()
    {
        _spawner.NextWave();
        _button.gameObject.SetActive(false);
    }
}
