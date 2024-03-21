using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PieceUnitTests
{
    private Piece piece;
    private GameObject gameObject = new GameObject();

    [SetUp]
    public void SetUp()
    {
        piece = new GameObject().AddComponent<Piece>();
    }

    [Test]
    public void BlackPieceColorGreenInitializedSetsPieceToTeamBlackColorGreen()
    {
        // Act
        piece.Initialize(Team.Black, Color.green);

        // Assert
        Assert.AreEqual(Team.Black, piece.Team);
        Assert.AreEqual(Color.green, piece.Color);
        Assert.IsFalse(piece.IsQueen);
    }

    [Test]
    public void QueeningPieceSetsPieceToQueen()
    {
        // Act
        piece.Initialize(Team.White, Color.white);
        piece.QueenPiece();

        // Assert
        Assert.AreEqual(Team.Black, piece.Team);
        Assert.AreEqual(Color.green, piece.Color);
        Assert.IsTrue(piece.IsQueen);
    }
}
