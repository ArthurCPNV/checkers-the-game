using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler
{
    private Piece _occupyingPiece { get; set; }
    private int _color;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
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

            if (_occupyingPiece == null)
            {
                pieceRectTransform.anchoredPosition = _rectTransform.anchoredPosition;
                pieceRectTransform.transform.SetParent(_rectTransform.transform);
                pieceRectTransform.localPosition = Vector3.zero;

                PlacePiece(piece.GetComponent<Piece>());
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