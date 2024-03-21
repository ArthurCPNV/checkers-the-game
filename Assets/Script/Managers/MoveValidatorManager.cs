using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveValidatorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _board;

    [SerializeField]
    private GameObject[] _possibleTiles;

    private void checkPossibleTiles(Tile startingTile, Team pieceTeam)
    {
        Board boardScript = _board.GetComponent<Board>();
        GameObject[] boardTiles = boardScript.GetTiles;
        int startingTileIndex = 0;

        for (int i = 0; i < boardTiles.Length; i++)
        {
            if (boardTiles[i].Equals(startingTile))
            {
                startingTileIndex = i;
            }
        }

        int offsetDirection = 1;

        if (pieceTeam == Team.White)
        {
            offsetDirection = -1;
        }

        _possibleTiles[0] = boardTiles[startingTileIndex - (11 * offsetDirection)];
        _possibleTiles[1] = boardTiles[startingTileIndex - (9 * offsetDirection)];
    }
}
