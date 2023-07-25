using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _playMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
    }
}
