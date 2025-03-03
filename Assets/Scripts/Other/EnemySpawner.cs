using System.Collections.Generic; // ƒл€ работы List с GameObject
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private float _spavnTimer = 2f;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private Transform[] _spawnPoints;

    EnemyEntity _enemyEntity;

    private int _currentEnemies = 0;

    private void Start()
    {
        /*
         * ¬ызывает указанный метод через указанный промежуток времени
         * InvokeRepeating(тип_данных(метод), интервал_до_первого_вызова, промежуток_повторени€);
         */
        InvokeRepeating(nameof(SpawnEnamy), 0f, _spavnTimer);
    }

    private void SpawnEnamy()
    {
        if (_currentEnemies >= _maxEnemies) return;
        GameObject enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        Transform spawnPoint = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        _currentEnemies++;
        EnemyEntity enemyEntity = enemy.GetComponent<EnemyEntity>();
        if (enemyEntity != null ) enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        _currentEnemies--;
    }
}
