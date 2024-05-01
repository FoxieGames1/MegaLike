using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float spawnInterval = 2.0f;
    public int maxEnemies = 5;
    private int currentEnemies = 0;
    public Vector2 moveDirection = Vector2.left;

    private void Start()
    {
        //InvokeRepeating("SpawnRandomEnemy", 0.0f, spawnInterval);
    }

    private void SpawnRandomEnemy()
    {
        if (currentEnemies >= maxEnemies) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        SpawnEnemy(randomIndex);
    }

    private void SpawnEnemy(int enemyTypeIndex)
    {
        if (enemyTypeIndex < 0 || enemyTypeIndex >= enemyPrefabs.Length)
            return;

        GameObject selectedEnemyPrefab = enemyPrefabs[enemyTypeIndex];

        GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            // Establecer la dirección de movimiento desde el inspector
            //enemyMovement.SetMoveDirection(moveDirection);
        }

        currentEnemies++;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
