using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameStarted = false;
    public bool multiplayer = false;
    public bool isGameRunning = false;
    public bool suddenDeathActive = false;


    [Header("References")]
    public UIManager uiManager;

    [Header("Gameplay Objects")]
    public GameObject player1;
    public GameObject player2;
    public Transform ball;

    public TimeController timer;

    [Header("Score")]
    public int scoreLeft = 0;
    public int scoreRight = 0;

    public TextMeshProUGUI scoreLeftText;
    public TextMeshProUGUI scoreRightText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject pu in powerUps)
        {
            Destroy(pu);
        }

        gameStarted = true;
        isGameRunning = true;

        SetupPlayer2Controller();

        ball.position = Vector3.zero;
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();

        rbBall.velocity = Vector2.zero;
        rbBall.angularVelocity = 0f;

        player1.transform.position = new Vector3(-7f, 0, 0);
        player2.transform.position = new Vector3(7f, 0, 0);


        player1.GetComponent<PlayerController>().enabled = true;
        player2.GetComponent<PlayerController>().enabled = true;

        uiManager.HideAllWinScreens();

        ball.GetComponent<BallController>().doubleScoreActive = false;
        ball.GetComponent<BallController>().OnGameStart();
    }

    public void OnTimeUp()
    {
        isGameRunning = false;

        if (scoreLeft == scoreRight)
        {
            suddenDeathActive = true;

            ball.position = Vector3.zero;
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            player1.transform.position = new Vector3(-7f, 0, 0);
            player2.transform.position = new Vector3(7f, 0, 0);

            player1.GetComponent<PlayerController>().enabled = true;
            player2.GetComponent<PlayerController>().enabled = true;

            SetupPlayer2Controller();

            ball.GetComponent<BallController>().LaunchBall();

            isGameRunning = true;
            return;
        }

        if (scoreLeft > scoreRight){
            uiManager.ShowPlayer1Win();
        } else{
            uiManager.ShowPlayer2Win();
        }
    }

    void FreezeAll()
    {
        Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
        rbBall.velocity = Vector2.zero;
        rbBall.angularVelocity = 0f;

        player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;

        AIPaddle ai = player2.GetComponent<AIPaddle>();
        if (ai != null) ai.enabled = false;

        Time.timeScale = 0f;
    }

    void SetupPlayer2Controller()
    {
        var human = player2.GetComponent<PlayerController>();
        var ai = player2.GetComponent<AIPaddle>();

        if (multiplayer)
        {
            human.enabled = true;
            ai.enabled = false;
        }
        else
        {
            human.enabled = false;
            ai.enabled = true;
            ai.ball = ball;
        }
    }

    public bool IsMultiplayer()
    {
        return multiplayer;
    }

    public void Player1Win()
    {
        uiManager.ShowPlayer1Win();
        Time.timeScale = 0f;
    }

    public void Player2Win()
    {
        uiManager.ShowPlayer2Win();
        Time.timeScale = 0f;
    }

    public void AddScoreLeft()
    {
        int scoreToAdd = 1;

        BallController ballCtrl = ball.GetComponent<BallController>();
        if (ballCtrl != null && ballCtrl.doubleScoreActive)
        {
            scoreToAdd = 2;                   
            ballCtrl.doubleScoreActive = false;
            Debug.Log("Player 1 scored DOUBLE!");
        }

        scoreLeft += scoreToAdd;
        scoreLeftText.text = scoreLeft.ToString();
    }

    public void AddScoreRight()
    {
        int scoreToAdd = 1;

        BallController ballCtrl = ball.GetComponent<BallController>();
        if (ballCtrl != null && ballCtrl.doubleScoreActive)
        {
            scoreToAdd = 2;                 
            ballCtrl.doubleScoreActive = false;
            Debug.Log("Player 2 scored DOUBLE!");
        }

        scoreRight += scoreToAdd;
        scoreRightText.text = scoreRight.ToString();
    }

    

    public void GoalScored()
    {
        if (!suddenDeathActive)
        {
            isGameRunning = false;
            if (timer != null) timer.PauseTimer();

            ball.transform.position = Vector3.zero;
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            StartCoroutine(StartRoundAfterDelay());
        }
        else
        {
            if (scoreLeft > scoreRight)
                uiManager.ShowPlayer1Win();
            else
                uiManager.ShowPlayer2Win();

            suddenDeathActive = false;
            isGameRunning = false;

            // hentikan bola dan player
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            player1.GetComponent<PlayerController>().enabled = false;
            player2.GetComponent<PlayerController>().enabled = false;
        }
    }


    IEnumerator StartRoundAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        isGameRunning = true;

        if (timer != null)
            timer.ResumeTimer();

        ball.GetComponent<BallController>().LaunchBall();
    }
}
