using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Board board;

    private Team _teamCurrentTurn = Team.White;

    private static GameManager instance;

    public void StartPlayGame()
    {
        SceneManager.LoadSceneAsync("BoardGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BoardGame")
        {
            board.InitializeBoard(10);
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
    }

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

    public Team TeamCurrentTurn
    {
        get { return _teamCurrentTurn; }
    }
}
