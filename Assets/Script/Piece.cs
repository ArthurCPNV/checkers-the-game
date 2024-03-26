using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Team
{
    White,
    Black
}

/// <summary>
/// Class <c>Piece</c> models a piece of the checkers game.
/// </summary>
public class Piece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private Color _color;

    [SerializeField]
    private Team _team;

    private bool _isQueen = false;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private GameObject _oldTile;
    private GameManager _gameManager;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Initializes the piece with team and color.
    /// </summary>
    /// <param name="team">The team of the piece.</param>
    /// <param name="color">The color of the piece.</param>
    public void Initialize(Team team, Color color)
    {
        _team = team;
        _color = color;
    }

    /// <summary>
    /// Converts the piece into a queen.
    /// </summary>
    public void QueenPiece()
    {
        throw new NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _oldTile = _rectTransform.transform.parent.gameObject;

        if (_gameManager.TeamCurrentTurn != _team) 
        {
            return;
        }

        _rectTransform.transform.parent.GetComponent<Tile>().RemovePiece();
        _rectTransform.SetParent(_canvas.transform);

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_gameManager.TeamCurrentTurn != _team)
        {
            return;
        }
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        // If piece is still a parent of canvas, that means an invalid move was made. Resets the pieces position to its original state. 
        if (_rectTransform.transform.parent == _canvas.transform || _gameManager.TeamCurrentTurn != _team) 
        {
            _rectTransform.transform.SetParent(_oldTile.transform);
            _rectTransform.anchoredPosition = _oldTile.GetComponent<RectTransform>().anchoredPosition;
            _rectTransform.localPosition = Vector3.zero;

            _oldTile.GetComponent<Tile>().PlacePiece(_rectTransform.GetComponentInParent<Piece>());
        }
        else if (_rectTransform.transform.parent != _oldTile.transform)
        {
            _gameManager.switchTurns();
        }
    }

    /// <summary>
    /// Gets the team of the piece.
    /// </summary>
    public Team Team 
    { 
        get { return _team; } 
    }

    /// <summary>
    /// Gets the color of the piece.
    /// </summary>
    public Color Color { get; }

    /// <summary>
    /// Gets or sets whether the piece is a queen.
    /// </summary>
    public bool IsQueen { get; set; }
}
