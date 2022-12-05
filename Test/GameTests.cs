using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.Util;
using FluentAssertions;
using Moq;
using Xunit;

namespace Blocks.Test;

public class GameTests
{
    [Fact]
    public void TakeBlock_Success()
    {
        var stateMock = new Mock<IGameState>();
        var block = new Block(Color.BLACK, Color.WHITE);

        stateMock.Setup(x => x.TakeBlock(block)).Returns(block);

        var game = new Game(stateMock.Object);

        var manipulator = game.TakeBlock(block);
        manipulator.BlockOrientation.Block.Should().Be(block);
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.X);
        stateMock.Verify(x => x.TakeBlock(block), Times.Once());
    }

    [Fact]
    public void CanPlaceBlock_Success()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Z
        ), new Vec3<int>(3, 1, 2))
        .Should().BeTrue();
    }

    [Fact]
    public void CanPlaceBlock_SuccessYOrientation()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Y
        ), new Vec3<int>(3, 1, 2))
        .Should().BeTrue();
    }

    [Fact]
    public void CanPlaceBlock_ErrorOutOfBounds()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        Action failAction = () => game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Z
        ), new Vec3<int>(3, 4, 2));

        failAction.Should().Throw<ArgumentException>().WithMessage("Position is out of bounds");
    }

    [Fact]
    public void CanPlaceBlock_ErrorNonEmpty()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        Action failAction = () => game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Z
        ), new Vec3<int>(0, 0, 0));

        failAction.Should().Throw<ArgumentException>().WithMessage("Position is not empty");
    }

    [Fact]
    public void CanPlaceBlock_FailNotEmpty()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.X
        ), new Vec3<int>(1, 0, 1))
        .Should().BeFalse();
    }

    [Fact]
    public void CanPlaceBlock_FailOverEmptySpace()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.X
        ), new Vec3<int>(0, 1, 1))
        .Should().BeFalse();
    }

    [Fact]
    public void CanPlaceBlock_FailPartlyOverEmptySpace()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.WHITE, Color.WHITE),
            Orientation.X
        ), new Vec3<int>(0, 1, 1))
        .Should().BeFalse();
    }

    [Fact]
    public void CanPlaceBlock_FailWrongColors()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.WHITE, Color.BLACK),
            Orientation.Z
        ), new Vec3<int>(3, 1, 2))
        .Should().BeFalse();
    }

    [Fact]
    public void CanPlaceBlock_FailOutOfBounds()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.X
        ), new Vec3<int>(3, 1, 2))
        .Should().BeFalse();
    }

    [Fact]
    public void CanPlaceBlock_FailOutOfBoundsYOrientation()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        game.CanPlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Y
        ), new Vec3<int>(2, 3, 0))
        .Should().BeFalse();
    }

    [Fact]
    public void PlaceBlock_Success()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));
        stateMock.Setup(x => x.UpdateBoard(It.IsAny<IEnumerable<ColorPosition>>()));

        var game = new Game(stateMock.Object);

        game.PlaceBlock(new BlockOrientation
        (
            new Block(Color.BLACK, Color.WHITE),
            Orientation.Z
        ), new Vec3<int>(3, 1, 2))
        .Should().BeTrue();

        var expected = new[]
        {
            new ColorPosition(Color.BLACK, new Vec3<int>(3, 1, 2)),
            new ColorPosition(Color.WHITE, new Vec3<int>(3, 1, 3))
        };
        stateMock.Verify(x => x.UpdateBoard(It.Is<IEnumerable<ColorPosition>>(y =>
            IsColorPositionMatch(expected, y))), Times.Once());
    }

    [Fact]
    public void GetScore_Success()
    {
        var stateMock = new Mock<IGameState>();
        stateMock.Setup(x => x.Board)
            .Returns(TestUtil.BoardFromFile(@"./Objects/StandardCube.txt"));

        var game = new Game(stateMock.Object);

        var score = game.GetScore();

        score.Black.Should().Be(16);
        score.White.Should().Be(14);
    }

    private bool IsColorPositionMatch(IEnumerable<ColorPosition> expected, IEnumerable<ColorPosition> actual)
    {
        return expected.Zip(actual)
            .All(tup =>
            {
                var (e, a) = tup;
                return e == a;
            });
    }
}