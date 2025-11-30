using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenu;
    public GameObject selectTime;
    public GameObject howToPlay;
    public GameObject pausePanel;
    public GameObject gameplayUI;

    [Header("Win Panels")]
    public GameObject player1WinPanel;
    public GameObject player2WinPanel;

    [Header("Game Settings")]
    public int selectedTime = 60;
    public bool isMultiplayer = false;

    void Start()
    {
        ShowMainMenu();
        Time.timeScale = 1;
    }

    void HideAll()
    {
        mainMenu.SetActive(false);
        selectTime.SetActive(false);
        pausePanel.SetActive(false);
        howToPlay.SetActive(false);
        gameplayUI.SetActive(false);
        player1WinPanel.SetActive(false);
        player2WinPanel.SetActive(false);
    }

    public void SelectSinglePlayer()
    {
        isMultiplayer = false;
        ShowSelectTime();
    }

    public void SelectMultiplayer()
    {
        isMultiplayer = true;
        ShowSelectTime();
    }

    public void ShowMainMenu()
    {
        HideAll();
        mainMenu.SetActive(true);
    }

    public void ShowSelectTime()
    {
        HideAll();
        selectTime.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        HideAll();
        howToPlay.SetActive(true);
    }

    public void StartGame()
    {
        HideAll();
        gameplayUI.SetActive(true);
        Time.timeScale = 1;

        GameManager.Instance.multiplayer = isMultiplayer;

        ResetScore();
        HideAllWinScreens();

        GameManager.Instance.timer.StartTimer(selectedTime);
        GameManager.Instance.StartGame();
    }

    public void SetTime(int t)
    {
        selectedTime = t;
        ResetScore();
        ShowHowToPlay();
    }

    void ResetScore()
    {
        GameManager.Instance.scoreLeft = 0;
        GameManager.Instance.scoreRight = 0;
        GameManager.Instance.scoreLeftText.text = "0";
        GameManager.Instance.scoreRightText.text = "0";
    }

    public void ShowPlayer1Win()
    {
        HideAll();
        player1WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowPlayer2Win()
    {
        HideAll();
        player2WinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HideAllWinScreens()
    {
        player1WinPanel.SetActive(false);
        player2WinPanel.SetActive(false);
    }
}
