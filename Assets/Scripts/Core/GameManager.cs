using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("End State UI")]
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int pointsPerEnemy = 100;

    private int score = 0;
    private int enemiesRemaining;
    private bool gameEnded = false;

    public bool IsGamePlaying()
    {
        return !gameEnded;
    }

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (winText != null)
        {
            winText.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(false);
        }
        UpdateScoreText();
        enemiesRemaining = FindObjectsOfType<EnemyStats>().Length;
        Debug.Log("Enemies remaining: " + enemiesRemaining);
    }

    private void Update()
    {
        if (gameEnded && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void EnemyDefeated()
    {
        if (gameEnded)
        {
            return;
        }

        enemiesRemaining--;

        score += pointsPerEnemy;
        UpdateScoreText();

        Debug.Log("Enemies remaining: " + enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            WinGame();
        }
    }

    public void LoseGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (loseText != null)
        {
            loseText.SetActive(true);
        }

        Debug.Log("Game lost.");
    }

    private void WinGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (winText != null)
        {
            winText.SetActive(true);
        }

        Debug.Log("Game won.");
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}