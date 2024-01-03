using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; 
    private float spawnRate = 1;
    private int score;
    public int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    private AudioSource bgMusic;
    public Slider controlAudio;
    public bool isPause = false;
    public GameObject pauseObj;

    private void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.volume = 1;
        controlAudio.value = 1;
    }
    private void Update()
    {
        bgMusic.volume = controlAudio.value;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPause)
            {
                OnPause();
            }
            else
            {
                OnRemuse();
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
    }
    // Start is called before the first frame update
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        score = 0;
        lives = 3;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLive();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLive()
    {
        liveText.text = "Lives: " + lives;
    }
    public void OnSetLives()
    {
        lives -= 1;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        UpdateLive();
    }

    public void GameOver()
    {
        isGameActive = false; 
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        UpdateLive();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnPause()
    {
        Time.timeScale = 0;
        isPause = true;
        pauseObj.gameObject.SetActive(true);
    }
    void OnRemuse()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseObj.gameObject.SetActive(false);
    }
}
