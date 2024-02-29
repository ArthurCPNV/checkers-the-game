using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TileUnitTests
{
    private Tile tile;

    [SetUp]
    public void SetUp()
    {
        tile = new Tile();
    }

    #region Initialization Tests

    [Test]
    public void TileIsInitiallyEmpty()
    {
        // Assert
        Assert.IsNull(tile.OccupyingPiece);
    }

    #endregion

    #region Place Piece Tests

    [Test]
    public void PlaceWhitePieceSetsPieceOccupiedToWhitePiece()
    {
        // Act
        tile.PlacePiece(new Piece(PieceTeam.White, PieceColor.White));

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(PieceTeam.White, tile.OccupyingPiece.Team);
        Assert.AreEqual(PieceColor.White, tile.OccupyingPiece.Color);
        Assert.IsFalse(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlaceBlackQueenSetsPieceOccupiedToBlackQueen()
    {
        // Act
        tile.PlacePiece(new Piece(PieceTeam.Black, PieceColor.Black, IsQueen: true));

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(PieceTeam.Black, tile.OccupyingPiece.Team);
        Assert.AreEqual(PieceColor.Black, tile.OccupyingPiece.Color);
        Assert.IsTrue(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlaceWhiteQueenWithGreenColorSetsPieceOccupiedToWhiteQueenWithGreenColor()
    {
        // Act
        tile.PlacePiece(new Piece(PieceTeam.White, PieceColor.Green, IsQueen: true));

        // Assert
        Assert.IsNotNull(tile.OccupyingPiece);
        Assert.AreEqual(PieceTeam.White, tile.OccupyingPiece.Team);
        Assert.AreEqual(PieceColor.Green, tile.OccupyingPiece.Color);
        Assert.IsTrue(tile.OccupyingPiece.IsQueen);
    }

    [Test]
    public void PlacePieceOnOccupiedTileThrowsError()
    {
        // Arrange
        tile.PlacePiece(new Piece(PieceTeam.White, PieceColor.White));

        // Act & Assert
        Assert.Throws<InvalidPlaceOperationException>(() => tile.PlacePiece(new Piece(PieceTeam.Black, PieceColor.Black)));
    }

    #endregion

    #region Remove Piece Tests

    [Test]
    public void RemovePieceSetsPieceOccupiedToNull()
    {
        // Arrange
        tile.PlacePiece(new Piece(PieceTeam.White, PieceColor.White));

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

    #endregion
}