using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText, highScoreText;

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        scoreText.text = "Score:\n" + GameManager.score;
        highScoreText.text = "High Score:\n" + GameManager.highScore;
    }
    public void Restart()
    {
        GameManager.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
