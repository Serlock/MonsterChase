using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private void Update()
    {
        scoreText.text = MonsterSpawner.monsterScore.ToString();
    }

    public void RestartGame()
    {
        MonsterSpawner.monsterScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        MonsterSpawner.monsterScore = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
