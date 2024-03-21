using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoardUnitTests
{
    private Board board;
    private GameObject gameObject = new GameObject();

    [SetUp]
    public void SetUp()
    {
        board = new GameObject().AddComponent<Board>();
    }

    [Test]
    public void InitializeBoard_SizeOfTen_CreatesListOf100Tiles()
    {
        // Arrange
        int boardSize = 10;

        // Act
        board.InitializeBoard(boardSize);

        // Assert
        Assert.AreEqual(boardSize * boardSize, board.GetTiles.Length);
    }

    [Test]
    public void GetTile_FromIndexInRange_GetTilesNotNull()
    {
        // Arrange
        int boardSize = 10;
        board.InitializeBoard(boardSize);

        // Act
        Tile tile1 = board.GetTile(6);
        Tile tile2 = board.GetTile(99);

        // Assert
        Assert.IsNotNull(tile1);
        Assert.IsNotNull(tile2);
    }

    [Test]
    public void GetTile_FromIndexOutOfRange_TileIsNull()
    {
        // Arrange
        int boardSize = 10;
        board.InitializeBoard(boardSize);

        // Act
        Tile tile = board.GetTile(100);

        // Assert
        Assert.IsNull(tile);
    }

    [Test]
    public void PlacePiece_WhitePiece_TileIsOccupiedByPiece()
    {
        // Arrange
        int boardSize = 10;
        int tileIndex = 1;

        board.InitializeBoard(boardSize);

        Piece piece = new GameObject().AddComponent<Piece>();
        piece.Initialize(Team.White, Color.white);

        Tile tile = board.GetTile(tileIndex);

        // Act
        board.PlacePiece(tileIndex, piece);

        // Assert
        Assert.AreEqual(tile.OccupyingPiece, piece);
    }

    [Test]
    public void RemovePiece_WhitePieceOnTile_TileOccupyingPieceIsNull()
    {
        // Arrange
        int boardSize = 10;
        int tileIndex = 1;

        board.InitializeBoard(boardSize);

        Piece piece = new GameObject().AddComponent<Piece>();
        piece.Initialize(Team.White, Color.white);

        Tile tile = board.GetTile(tileIndex);

        board.PlacePiece(tileIndex, piece);

        // Act
        board.RemovePiece(tileIndex);

        // Assert
        Assert.IsNull(tile.OccupyingPiece);
    }
}
