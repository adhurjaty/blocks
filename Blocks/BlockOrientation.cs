namespace Blocks;

public enum Orientation
{
    X,
    Y,
    Z
}

public enum Direction
{
    ORIGIN,
    FLIPPED
}

public class BlockOrientation
{
    private readonly Block _block;

    public Orientation Orientation { get; private set; } = Orientation.X;
    public Direction Direction { get; private set; } = Direction.ORIGIN;

    private readonly Orientation[] _orientationOrder = new[]
    {
        Orientation.X,
        Orientation.Z,
        Orientation.Y
    };

    public BlockOrientation(Block block)
    {
        _block = block;
    }

    public void CycleForward()
    {
        var newIdx = Array.IndexOf(_orientationOrder, Orientation) + 1;
        if (newIdx >= _orientationOrder.Length)
        {
            newIdx = 0;
            Flip();
        }
        Orientation = _orientationOrder[newIdx];
    }

    public void CycleBackward()
    {
        var newIdx = Array.IndexOf(_orientationOrder, Orientation) - 1;
        if (newIdx < 0)
        {
            newIdx = _orientationOrder.Length - 1;
            Flip();
        }
        Orientation = _orientationOrder[newIdx];
    }

    public void Flip()
    {
        Direction = Direction == Direction.ORIGIN ? Direction.FLIPPED : Direction.ORIGIN;
    }
}