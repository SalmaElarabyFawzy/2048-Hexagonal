using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CellsManager cellsManager;

    [Header("Game Over")] 
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Button reTryButton;

    [Header("Winning")]
    [SerializeField] private GameObject win;

    [Header("Score")] 
    [SerializeField] private TMP_Text scoreTxt;
    private int score = 0;
    [Header("Buttons")]
    [SerializeField] private Button newGame;
    [SerializeField] private Button exit;
    private void Awake()
    {
        gameManager.OnGameOver += GameOver;
        gameManager.OnWinning += Win;
        cellsManager.OnScoreChangeEvent += Score;
    }
    private void Score(object sender, CellsManager.ScoreChangeEventArgs e)
    {
        score += e.score;
        scoreTxt.text = score.ToString();
    }

    private void Win(object sender, EventArgs e)
    {
        win.SetActive(true);
    }
    private void GameOver(object sender, EventArgs e)
    {
        gameOver.SetActive(true);
    }
    private void Start()
    {
        reTryButton.onClick.AddListener(() =>
        {
            NewGame();
        });
        newGame.onClick.AddListener(() =>
        {
            NewGame();
        });
        exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    public void NewGame()
    {
        win.SetActive(false);
        gameOver.SetActive(false);
        scoreTxt.text = "0";
        gameManager.NewGame();
    }
}
