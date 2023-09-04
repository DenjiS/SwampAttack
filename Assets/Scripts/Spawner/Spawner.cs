using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private int _spawnedEnemies;
    private float _timeAfterLastSpawn;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn > _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawnedEnemies++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawnedEnemies, _currentWave.Count);
        }

        if (_spawnedEnemies >= _currentWave.Count)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawnedEnemies = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Died += OnEnemyDied;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;

        _player.AddMoney(enemy.Reward);
    }
}
