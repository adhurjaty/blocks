using System;
using System.Linq;
using Blocks.Util;
using FluentAssertions;
using Xunit;

namespace Blocks.Test;

public class GameStateTests
{
    [Fact]
    public void FourByFourByFourGameStateInitialization()
    {
        var state = new FourByFourByFourGameState();

        var expected = new[]
        {
            new Block(First: Color.WHITE, Second: Color.WHITE),
            new Block(First: Color.WHITE, Second: Color.WHITE),
            new Block(First: Color.WHITE, Second: Color.WHITE),
            new Block(First: Color.BLACK, Second: Color.BLACK),
            new Block(First: Color.BLACK, Second: Color.BLACK),
            new Block(First: Color.BLACK, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.WHITE, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.BLACK),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
            new Block(First: Color.RED, Second: Color.WHITE),
        };

        for (int i = 0; i < expected.Length; i++)
        {
            state.RemainingBlocks[i].Should().BeEquivalentTo(expected[i]);
        }
        state.RemainingBlocks.Count.Should().Be(expected.Length);
    }

    [Fact]
    public void TakeBlock_Success()
    {
        var state = new FourByFourByFourGameState();

        var block = state.TakeBlock(new Block(Color.RED, Color.WHITE));

        block.Should().BeEquivalentTo(new Block(Color.RED, Color.WHITE));
        state.RemainingBlocks.Where(x => x == block).Count().Should().Be(6);
    }

    [Fact]
    public void TakeBlock_Failure()
    {
        var state = new FourByFourByFourGameState();

        for (int i = 0; i < 3; i++)
        {
            state.TakeBlock(new Block(Color.WHITE, Color.WHITE));
        }

        Action failAction = () => state.TakeBlock(new Block(Color.WHITE, Color.WHITE));
        failAction.Should().Throw<Exception>().WithMessage("Block (WHITE, WHITE) does not exist in remaining blocks");
    }

    [Fact]
    public void UpdateBoard_Success()
    {
        var state = new FourByFourByFourGameState();

        state.UpdateBoard(new[]
        {
            new ColorPosition(Color.BLACK, new Vec3<int>(1, 0, 1)),
            new ColorPosition(Color.WHITE, new Vec3<int>(1, 0, 2))
        });

        state.Board[1, 0, 1].Should().Be(Space.BLACK);
        state.Board[1, 0, 2].Should().Be(Space.WHITE);
    }
}
