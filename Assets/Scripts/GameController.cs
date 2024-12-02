using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    GameObject secretWinScreen;
    [SerializeField]
    GameObject loseScreen;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    GameObject pointsGainedCanvas;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    List<Transform> enemyPositions;
    [SerializeField]
    int enemyCount;
    [SerializeField]
    Transform enemySpawnPositionMin;
    [SerializeField]
    Transform enemySpawnPositionMax;
    [SerializeField]
    float spawnDistanceFromPlayer;
    [SerializeField]
    LayerMask obstacleLayer;
    [HideInInspector]
    public bool gameFinished;

    int score;

    void Start()
    {
        SpawnEnemiesRandom();
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    void SpawnEnemiesRandom()
    {
        Vector3 position;
        bool insideObstacle;
        for (int i = 0; i < enemyCount; i++)
        {
            do
            {
                float positionX = Random.Range(enemySpawnPositionMin.position.x, enemySpawnPositionMax.position.x);
                float positionZ = Random.Range(enemySpawnPositionMin.position.z, enemySpawnPositionMax.position.z);
                position = new Vector3(positionX, 1, positionZ);
                insideObstacle = false;
                if (Physics.OverlapSphere(position, 1, obstacleLayer).Length != 0)
                {
                    insideObstacle = true;
                }
            }
            while (Mathf.Abs(Vector3.Distance(player.transform.position, position)) <= spawnDistanceFromPlayer || insideObstacle);
            Instantiate(enemy, position, new Quaternion());
        }
    }
    
    void SpawnEnemies()
    {
        foreach (Transform spot in enemyPositions)
        {
            Instantiate(enemy, spot.position, new Quaternion());
            enemyCount++;
        }
    }

    public void KillEnemy()
    {
        enemyCount--; ;
        if (enemyCount <= 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        gameFinished = true;
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
        gameFinished = true;
    }

    public void GetSecretEnding()
    {
        secretWinScreen.SetActive(true);
        gameFinished = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore(int score, Vector3 textPosition)
    {
        this.score += score;
        var textInstance = Instantiate(pointsGainedCanvas, textPosition, new Quaternion());
        textInstance.GetComponentInChildren<TextMeshProUGUI>().text = "+" + score;
        Destroy(textInstance, 1);
    }
}
