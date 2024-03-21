using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    private GameObject[] _tiles;
    private int _size;

    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private GameObject piecePrefab;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BoardGame")
        {
            InitializeBoard(10);
        }
    }

    /// <summary>
    /// Generates the tiles on the board.
    /// </summary>
    private void GenerateTiles()
    {
        GameObject tileZoneObject = GameObject.Find("TileZone");

        _tiles = new GameObject[_size * _size];

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                int current_iteration = i * _size + j;

                Vector3 tilePosition = new Vector3(-315 + (j * 70), 315 + (i * -70), 0);
                GameObject newTile = Instantiate(tilePrefab, tileZoneObject.transform);

                RectTransform rectTransform = newTile.GetComponent<RectTransform>();

                rectTransform.anchoredPosition = tilePosition;

                Color tileColor;

                if (i % 2 == 0)
                {

                    if (current_iteration % 2 == 0)
                    {
                        tileColor = new Color(255, 255, 255, 255);
                    }
                    else
                    {
                        tileColor = new Color(0, 0, 0, 255);
                    }
                }
                else
                {
                    if (current_iteration % 2 != 0)
                    {
                        tileColor = new Color(255, 255, 255, 255);
                    }
                    else
                    {
                        tileColor = new Color(0, 0, 0, 255);
                    }
                }

                newTile.GetComponent<Image>().color = tileColor;

                _tiles[current_iteration] = newTile;
            }
        }
    }

    /// <summary>
    /// Creates a new piece on the specified tile with the given team and color.
    /// </summary>
    /// <param name="currentTile">The tile to place the piece on.</param>
    /// <param name="pieceTeam">The team of the piece.</param>
    /// <param name="pieceColor">The color of the piece.</param>
    private void CreateNewPiece(GameObject currentTile, Team pieceTeam, Color pieceColor)
    {
        GameObject newPiece = Instantiate(piecePrefab, currentTile.transform);

        RectTransform pieceRectTransform = newPiece.GetComponent<RectTransform>();
        Vector2 tilePosition = currentTile.GetComponent<RectTransform>().anchoredPosition;
        pieceRectTransform.anchoredPosition = tilePosition;
        pieceRectTransform.localPosition = Vector3.zero;

        Tile tileScript = currentTile.GetComponent<Tile>();
        tileScript.PlacePiece(newPiece.GetComponent<Piece>());

        Piece pieceScript = newPiece.GetComponent<Piece>();
        pieceScript.Initialize(pieceTeam, pieceColor);
    }

    /// <summary>
    /// Initializes the pieces on the board.
    /// </summary>
    private void InitializePieces()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                int current_iteration = i * _size + j;
                GameObject currentTile = _tiles[current_iteration];


                if (i % 2 == 0)
                {

                    if (current_iteration % 2 != 0)
                    {
                        if (i >= 0 && i < 4) 
                        {
                            CreateNewPiece(currentTile, Team.Black, Color.black);
                        }
                        else if(i > 5)
                        {
                            CreateNewPiece(currentTile, Team.White, Color.white);
                        }
                    }
                }
                else
                {
                    if (current_iteration % 2 == 0)
                    {
                        if (i >= 0 && i < 4)
                        {
                            CreateNewPiece(currentTile, Team.Black, Color.black);
                        }
                        else if (i > 5)
                        {
                            CreateNewPiece(currentTile, Team.White, Color.white);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Initializes the game board with the specified size.
    /// </summary>
    /// <param name="size">The size of the board.</param>
    public void InitializeBoard(int size)
    {
        _size = size;

        GenerateTiles();
        InitializePieces();
    }

    /// <summary>
    /// Gets the tile at the specified index.
    /// </summary>
    /// <param name="index">The index of the tile.</param>
    /// <returns>The tile at the specified index.</returns>
    public Tile GetTile(int index)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Places a piece on the specified tile.
    /// </summary>
    /// <param name="index">The index of the tile.</param>
    /// <param name="piece">The piece to be placed.</param>
    public void PlacePiece(int index, Piece piece)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes a piece from the specified tile.
    /// </summary>
    /// <param name="index">The index of the tile.</param>
    public void RemovePiece(int index)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the array of tiles on the board.
    /// </summary>
    public GameObject[] GetTiles
    {
        get => _tiles;
    }
}
