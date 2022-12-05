using Blocks.Util;
using FluentAssertions;
using Xunit;

namespace Blocks.Test;

public class RulesTest
{
    [Fact]
    public void IsInBounds_True()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");

        var rules = new PlacementRules(board);

        rules.IsInBounds(new Vec3<int>(1, 1, 1)).Should().BeTrue();
    }

    [Fact]
    public void IsInBounds_False()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");

        var rules = new PlacementRules(board);

        rules.IsInBounds(new Vec3<int>(1, 1, 4)).Should().BeFalse();
    }
    
    [Fact]
    public void IsInEmptySpace_True()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsInEmptySpace(new Vec3<int>(1, 3, 1)).Should().BeTrue();
    }

    [Fact]
    public void IsInEmptySpace_False()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsInEmptySpace(new Vec3<int>(0, 3, 0)).Should().BeFalse();
    }

    [Fact]
    public void IsOnValidColor_BaseTrue()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsOnValidColor(Color.BLACK, new Vec3<int>(0, 0, 3)).Should().BeTrue();
    }

    [Fact]
    public void IsOnValidColor_BlackOnBlackFalse()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsOnValidColor(Color.BLACK, new Vec3<int>(2, 1, 3)).Should().BeFalse();
    }

    [Fact]
    public void IsOnValidColor_BlackOnWhiteTrue()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsOnValidColor(Color.BLACK, new Vec3<int>(2, 1, 2)).Should().BeTrue();
    }

    [Fact]
    public void IsOnValidColor_RedOnAnyTrue()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsOnValidColor(Color.RED, new Vec3<int>(2, 1, 2)).Should().BeTrue();
        rules.IsOnValidColor(Color.RED, new Vec3<int>(2, 1, 3)).Should().BeTrue();
    }

    [Fact]
    public void IsOnValidColor_AnyOnRedTrue()
    {
        var board = TestUtil.BoardFromFile(@"./Objects/StandardCube.txt");
        var rules = new PlacementRules(board);

        rules.IsOnValidColor(Color.BLACK, new Vec3<int>(2, 3, 0)).Should().BeTrue();
        rules.IsOnValidColor(Color.WHITE, new Vec3<int>(2, 3, 0)).Should().BeTrue();
    }
}

