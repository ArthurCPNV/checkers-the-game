using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TileUnitTests
{
    private Tile tile;
    private GameObject gameObject = new GameObject();


    [SetUp]
    public void SetUp()
    {
        tile = gameObject.AddComponent<Tile>();
    }

    [Test]
    public void TileIsInitiallyEmpty()
    {
        // Assert
        Assert.IsNull(tile.OccupyingPiece);
    }

    [Test]
    public void PlaceWhitePieceSetsPieceOccupiedToWhitePiece()
    {
        // Arrange
        Piece newPiece = gameObject.AddComponent<Piece>();
        newPiece.Initialize(Team.White, Color.white);

        // Act
        tile.PlacePiece(newPiece);

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(Team.White, tile.OccupyingPiece.Team);
        Assert.AreEqual(Color.white, tile.OccupyingPiece.Color);
        Assert.IsFalse(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlaceBlackQueenSetsPieceOccupiedToBlackQueen()
    {
        // Arrange
        Piece newPiece = gameObject.AddComponent<Piece>();
        newPiece.Initialize(Team.Black, Color.black);

        // Act

        tile.PlacePiece(newPiece);
        newPiece.QueenPiece();

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(Team.Black, tile.OccupyingPiece.Team);
        Assert.AreEqual(Color.black, tile.OccupyingPiece.Color);
        Assert.IsTrue(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlaceWhiteQueenWithGreenColorSetsPieceOccupiedToWhiteQueenWithGreenColor()
    {
        // Arrange
        Piece newPiece = gameObject.AddComponent<Piece>();
        newPiece.Initialize(Team.White, Color.green);

        // Act
        tile.PlacePiece(newPiece);
        newPiece.QueenPiece();

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(Team.White, tile.OccupyingPiece.Team);
        Assert.AreEqual(Color.green, tile.OccupyingPiece.Color);
        Assert.IsTrue(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlacePieceOnOccupiedTileThrowsError()
    {
        // Arrange
        Piece newPiece = gameObject.AddComponent<Piece>();
        newPiece.Initialize(Team.White, Color.white);

        Piece blackPiece = gameObject.AddComponent<Piece>();
        blackPiece.Initialize(Team.Black, Color.black);

        tile.PlacePiece(newPiece);

        // Act & Assert
        Assert.Throws<InvalidPlaceOperationException>(() => tile.PlacePiece(blackPiece));
    }

    [Test]
    public void RemovePieceSetsPieceOccupiedToNull()
    {
        // Arrange
        Piece newPiece = gameObject.AddComponent<Piece>();
        newPiece.Initialize(Team.White, Color.white);
        tile.PlacePiece(newPiece);

        // Act
        tile.RemovePiece();

        // Assert
        Assert.IsNull(tile.OccupyingPiece);
    }

    [Test]
    public void RemovePieceFromEmptyTileDoesNotThrowError()
    {
        // Act & Assert
        Assert.Throws<InvalidRemoveOperationException>(() => tile.RemovePiece());
    }
}