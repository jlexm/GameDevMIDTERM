using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isGameOver;
    public AudioSource musicSource;

    public GameObject gameOverScreen;

    private void Awake()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        musicSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            musicSource.volume = 0f;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnRestartButtonClick()
    {
        RestartLevel();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OnMenuButtonClick()
    {
        MainMenu();
    }


    public void OnQuitClick()
    {
       Application.Quit(); 
    }

}
