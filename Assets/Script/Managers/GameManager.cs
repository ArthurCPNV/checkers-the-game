using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Board _board;

    private Team _teamCurrentTurn = Team.White;

    private int _scoreWhite = 20;
    private int _scoreBlack = 20;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("GameManager");
                    instance = gameObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public void StartPlayGame()
    {
        SceneManager.LoadSceneAsync("BoardGame");
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BoardGame")
        {
            _board.InitializeBoard(10);
            _board = Board.Instance;
        }
    }

    public void switchTurns()
    {
        if (_teamCurrentTurn == Team.White)
        {
            _teamCurrentTurn = Team.Black;
        }
        else
        {
            _teamCurrentTurn = Team.White;
        }

        StartCoroutine(TurnBoard());
    }

    public void AddScoreToTeam(Team team)
    {
        GameObject score = GameObject.Find("Score" + team.ToString());
        Text scoreText = score.transform.Find("ScoreText").gameObject.GetComponent<Text>();

        if (team == Team.White)
        {
            _scoreWhite -= 1;
            scoreText.text = _scoreWhite.ToString();
        }
        else
        {
            _scoreBlack -= 1;
            scoreText.text = _scoreBlack.ToString();
        }
    }

    private IEnumerator TurnBoard()
    {
        _board = Board.Instance;

        GameObject boardGameObject = _board.gameObject;

        Quaternion startRotation = boardGameObject.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, 180);

        float elapsedTime = 0.0f;
        float rotationDuration = 1.5f; // You can adjust the duration here

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            t = EaseInOutQuad(t); // Apply easing function

            // Interpolate between start and target rotation
            boardGameObject.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // Increment time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure final rotation is exactly the target rotation
        boardGameObject.transform.rotation = targetRotation;
    }

    // Easing function: EaseInOutQuad
    private float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    public Team TeamCurrentTurn
    {
        get { return _teamCurrentTurn; }
    }
}
