using Blocks.Util;

namespace Blocks;

public record BlockOrientation
(
    Block Block,
    Orientation Orientation
);

public record ColorPosition
(
    Color Color,
    Vec3<int> Position
);

public static class BlockOrientationExtensions
{
    public static (ColorPosition, ColorPosition) GetColorPositions(
        this BlockOrientation blockOrientation, Vec3<int> pos)
    {
        var block = blockOrientation.Block;
        var orientation = blockOrientation.Orientation;
        return (
            new ColorPosition
            (
                Color: block.First,
                Position: pos
            ),
            new ColorPosition
            (
                Color: block.Second,
                Position: orientation switch
                {
                    Orientation.X => pos with { X = pos.X + 1 },
                    Orientation.Y => pos with { Y = pos.Y + 1 },
                    Orientation.Z => pos with { Z = pos.Z + 1 },
                    _ => throw new ArgumentException("Invalid orientation")
                }
            )
        );
    }
}