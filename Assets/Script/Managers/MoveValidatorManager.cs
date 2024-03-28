using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MoveValidatorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _possibleTiles;

    private Board _board;

    private Tuple<int, Piece>[] _pieceToBeRemoved;

    private static int[] _boardTileBoundaries;

    private static int[] _boardIndexRepresentation;

    private static MoveValidatorManager instance;

    private void Awake()
    {
        _board = Board.Instance;

        InitializeBoardTileBoundaries();
    }

    private void InitializeBoardTileBoundaries()
    {
        _boardTileBoundaries = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                           -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                                           -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                           -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        _boardIndexRepresentation = new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34,
                                                37, 38, 39, 40, 41, 42, 43, 44, 45, 46,
                                                49, 50, 51, 52, 53, 54, 55, 56, 57, 58,
                                                61, 62, 63, 64, 65, 66, 67, 68, 69, 70,
                                                73, 74, 75, 76, 77, 78, 79, 80, 81, 82,
                                                85, 86, 87, 88, 89, 90, 91, 92, 93, 94,
                                                97, 98, 99, 100, 101, 102, 103, 104, 105, 106,
                                                109, 110, 111, 112, 113, 114, 115, 116, 117, 118,
                                                121, 122, 123, 124, 125, 126, 127, 128, 129, 130,
                                                133, 134, 135, 136, 137, 138, 139, 140, 141, 142 };

        _pieceToBeRemoved = new Tuple<int, Piece>[2];
    }

    public static MoveValidatorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MoveValidatorManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("GameManager");
                    instance = gameObject.AddComponent<MoveValidatorManager>();
                }
            }
            return instance;
        }
    }

    public void checkPossibleTiles(GameObject startingTile, Team pieceTeam)
    {
        _possibleTiles = new GameObject[2];

        GameObject[] boardTiles = _board.GetTiles;
        int startingTileIndex = 0;

        for (int i = 0; i < boardTiles.Length; i++)
        {
            if (boardTiles[i].Equals(startingTile))
            {
                startingTileIndex = i;
            }
        }

        int offsetDirection = 1;

        if (pieceTeam == Team.Black)
        {
            offsetDirection = -1;
        }

        if (_boardTileBoundaries[_boardIndexRepresentation[startingTileIndex] - (13 * offsetDirection)] != -1)
        {
            GameObject tile = boardTiles[startingTileIndex - (11 * offsetDirection)];

            Tile tileScript = tile.GetComponent<Tile>();

            if (tileScript.OccupyingPiece == null)
            {
                _possibleTiles[0] = tile;
            }
            else if (tileScript.OccupyingPiece.Team != pieceTeam)
            {
                if (_boardTileBoundaries[_boardIndexRepresentation[startingTileIndex] - (26 * offsetDirection)] != -1)
                {

                    _pieceToBeRemoved[0] = Tuple.Create(0, tileScript.OccupyingPiece);

                    tile = boardTiles[startingTileIndex - (22 * offsetDirection)];

                    tileScript = tile.GetComponent<Tile>();

                    if (tileScript.OccupyingPiece == null)
                    {
                        _possibleTiles[0] = tile;
                    }
                    else
                    {
                        _pieceToBeRemoved[0] = null;
                    }
                }
            }
        }

        if (_boardTileBoundaries[_boardIndexRepresentation[startingTileIndex] - (11 * offsetDirection)] != -1)
        {
            GameObject tile = boardTiles[startingTileIndex - (9 * offsetDirection)];

            Tile tileScript = tile.GetComponent<Tile>();

            if (tileScript.OccupyingPiece == null)
            {
                _possibleTiles[1] = tile;
            }
            else if (tileScript.OccupyingPiece.Team != pieceTeam)
            {
                if (_boardTileBoundaries[_boardIndexRepresentation[startingTileIndex] - (22 * offsetDirection)] != -1)
                {
                    _pieceToBeRemoved[1] = Tuple.Create(1, tileScript.OccupyingPiece);

                    tile = boardTiles[startingTileIndex - (18 * offsetDirection)];

                    tileScript = tile.GetComponent<Tile>();

                    if (tileScript.OccupyingPiece == null)
                    {
                        _possibleTiles[1] = tile;
                    }
                    else
                    {
                        _pieceToBeRemoved[1] = null;
                    }
                }
            }
        }

        Debug.Log(_pieceToBeRemoved);

        showPossibleTiles();
    }

    private void showPossibleTiles()
    {
        foreach (var tile in _possibleTiles)
        {
            if (tile != null)
            {
                tile.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            }
        }
    }

    public void hidePossibleTiles()
    {
        foreach (var tile in _possibleTiles)
        {
            if (tile != null)
            {
                tile.GetComponent<UnityEngine.UI.Image>().color = Color.black;
            }
        }
    }

    public void ResetPieceToBeRemoved()
    {
        _pieceToBeRemoved[0] = null;
        _pieceToBeRemoved[1] = null;
    }

    public GameObject[] GetPossibleTiles
    {
        get => _possibleTiles;
    }

    public Tuple<int, Piece>[] GetPieceToBeRemove
    {
        get => _pieceToBeRemoved;
    }
}
