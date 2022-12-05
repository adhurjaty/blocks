using Blocks;
using FluentAssertions;
using Xunit;

namespace Test;

public class BlockManipulatorTests
{
    [Fact]
    public void CycleForward_Success()
    {
        var block = new Block
        (
            First: Color.BLACK,
            Second: Color.WHITE
        );

        var manipulator = new BlockManipulator(block);

        manipulator.CycleForward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Z);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.BLACK);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.WHITE);

        manipulator.CycleForward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Y);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.BLACK);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.WHITE);
        
        manipulator.CycleForward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.X);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);
        
        manipulator.CycleForward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Z);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);
        
        manipulator.CycleForward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Y);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);
    }

    [Fact]
    public void CycleBackward_Success()
    {
        var block = new Block
        (
            First: Color.BLACK,
            Second: Color.WHITE
        );

        var manipulator = new BlockManipulator(block);

        manipulator.CycleBackward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Y);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);

        manipulator.CycleBackward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Z);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);

        manipulator.CycleBackward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.X);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);
        
        manipulator.CycleBackward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Y);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.BLACK);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.WHITE);
        
        manipulator.CycleBackward();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.Z);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.BLACK);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.WHITE);
    }

    [Fact]
    public void Flip_Success()
    {
        var block = new Block
        (
            First: Color.BLACK,
            Second: Color.WHITE
        );

        var manipulator = new BlockManipulator(block);

        manipulator.Flip();
        manipulator.BlockOrientation.Orientation.Should().Be(Orientation.X);
        manipulator.BlockOrientation.Block.First.Should().Be(Color.WHITE);
        manipulator.BlockOrientation.Block.Second.Should().Be(Color.BLACK);
    }
}