using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Board board;

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
}
