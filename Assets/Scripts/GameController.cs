using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    GameObject player;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    List<Transform> enemyPositions;

    public bool gameFinished;
    int score;
    int enemyCount;

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        scoreText.text = "Score: " + score;    
    }

    void SpawnEnemies()
    {
        foreach(Transform spot in enemyPositions)
        {
            Instantiate(enemy, spot.position, new Quaternion());
            enemyCount++;
        }
    }
    public void KillEnemy()
    {
        enemyCount--;
        score += 10;
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
}
