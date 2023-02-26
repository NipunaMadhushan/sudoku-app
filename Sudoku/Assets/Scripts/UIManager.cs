using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class UIManager : MonoBehaviour
{
    // Panels
    public GameObject startPanel;
    public GameObject mainMenuPanel;
    public GameObject newGamePanel;
    public GameObject boardPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    // Continue button
    public GameObject continueButton;

    public static UIManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        // Go to main main menu
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void onNewGameButtonClicked() {
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(true);
        boardPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void onExitButtonClicked() {
        Debug.Log("Game is exiting");
        Application.Quit();
    }

    public void onCancelButtonClicked() {
        mainMenuPanel.SetActive(true);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void onEasyButtonClicked() {
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        BoardUI.Instance.difficultyLevel = DifficultyLevel.EASY;
        Debug.Log("Difficulty Level: " + BoardUI.Instance.difficultyLevel);

        BoardUI.Instance.createBoard();
    }

    public void onMediumButtonClicked() {
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        BoardUI.Instance.difficultyLevel = DifficultyLevel.MEDIUM;
        Debug.Log("Difficulty Level: " + BoardUI.Instance.difficultyLevel);

        BoardUI.Instance.createBoard();
    }

    public void onHardButtonClicked() {
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        BoardUI.Instance.difficultyLevel = DifficultyLevel.HARD;
        Debug.Log("Difficulty Level: " + BoardUI.Instance.difficultyLevel);

        BoardUI.Instance.createBoard();
    }

    public void onExpertButtonClicked() {
        startPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        boardPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        BoardUI.Instance.difficultyLevel = DifficultyLevel.EXPERT;
        Debug.Log("Difficulty Level: " + BoardUI.Instance.difficultyLevel);

        BoardUI.Instance.createBoard();
    }

    public void onPauseButtonClicked() {
        BoardUI.Instance.isGamePaused = true;
        BoardUI.Instance.updatePausePanel();
        pausePanel.SetActive(true);
        Debug.Log("Game Paused");
    }

    public void onResumeGameButtonClicked() {
        BoardUI.Instance.isGamePaused = false;
        pausePanel.SetActive(false);
        Debug.Log("Game Resumed");
    }

    public void onBackButtonClicked() {
        continueButton.SetActive(true);
        mainMenuPanel.SetActive(true);
        boardPanel.SetActive(false);
    }

    public void onContinueButtonClicked() {
        mainMenuPanel.SetActive(false);
        boardPanel.SetActive(true);
        continueButton.SetActive(false);
    }

    public void onGameOver() {
        BoardUI.Instance.updateGameOverPanel();
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }
}
