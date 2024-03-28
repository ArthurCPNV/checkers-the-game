using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Piece _occupyingPiece;

    private int _color;

    private RectTransform _rectTransform;
    private MoveValidatorManager _moveValidatorManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _moveValidatorManager = MoveValidatorManager.Instance;
        _gameManager = GameManager.Instance;
    }

    public Piece OccupyingPiece
    {
        get => _occupyingPiece;
    }

    /// <summary>
    /// Places a piece on the tile.
    /// </summary>
    /// <param name="pieceToPlace">The piece to place on the tile.</param>
    public void PlacePiece(Piece pieceToPlace)
    {
        _occupyingPiece = pieceToPlace;
    }

    /// <summary>
    /// Removes the piece from the tile.
    /// </summary>
    public void RemovePiece()
    {
        if (_occupyingPiece != null) 
        { 
            _occupyingPiece = null;
        }
    }

    /// <summary>
    /// Changes the color of the tile.
    /// </summary>
    /// <param name="color">The color code of the new tile color.</param>
    public void ChangeTileColor(int color) 
    {
        throw new NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject piece = eventData.pointerDrag;
            RectTransform pieceRectTransform = piece.GetComponent<RectTransform>();

            if (piece.GetComponent<Piece>().Team != GameManager.Instance.TeamCurrentTurn)
            {
                return;
            }

            bool canPlacePieceOnTile = false;

            GameObject[] possibleTiles = _moveValidatorManager.GetPossibleTiles;

            foreach (var tile in possibleTiles)
            {
                if (tile != null)
                {
                    if (tile.Equals(gameObject))
                    {
                        canPlacePieceOnTile = true;
                    }
                }
            }

            if (_occupyingPiece == null && canPlacePieceOnTile)
            {
                pieceRectTransform.anchoredPosition = _rectTransform.anchoredPosition;
                pieceRectTransform.transform.SetParent(_rectTransform.transform);
                pieceRectTransform.localPosition = Vector3.zero;

                PlacePiece(piece.GetComponent<Piece>());

                int tileIndexToRemovePiece = 0;

                Piece pieceToRemove = null;

                if (_moveValidatorManager.GetPieceToBeRemove != null)
                {
                    foreach (Tuple<int, Piece> pieceToRemoveTuple in _moveValidatorManager.GetPieceToBeRemove)
                    {
                        if (pieceToRemoveTuple != null)
                        {
                            tileIndexToRemovePiece = pieceToRemoveTuple.Item1;
                            pieceToRemove = pieceToRemoveTuple.Item2;
                        }

                        if (pieceToRemove != null && possibleTiles[tileIndexToRemovePiece] == gameObject)
                        {
                            pieceToRemove.transform.parent.GetComponent<Tile>().RemovePiece();

                            Destroy(pieceToRemove.gameObject);

                            if (pieceToRemove.Team == Team.White)
                            {
                                _gameManager.AddScoreToTeam(Team.White);
                                return;
                            }
                            else
                            {
                                _gameManager.AddScoreToTeam(Team.Black);
                                return;
                            }
                        }
                    }

                    _moveValidatorManager.ResetPieceToBeRemoved();
                }
            }
        }
    }
}

/// <summary>
/// Exception thrown when an invalid placement operation is attempted.
/// </summary>
public class InvalidPlaceOperationException : Exception { }

/// <summary>
/// Exception thrown when an invalid removal operation is attempted.
/// </summary>
public class InvalidRemoveOperationException : Exception { }