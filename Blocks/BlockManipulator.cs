namespace Blocks;

public class BlockManipulator
{
    public BlockOrientation BlockOrientation { get; private set; }
    private Orientation _orientation => BlockOrientation.Orientation;

    private readonly Orientation[] _orientationOrder = new[]
    {
        Orientation.X,
        Orientation.Z,
        Orientation.Y
    };

    public BlockManipulator(Block block)
    {
        BlockOrientation = new BlockOrientation
        (
            Block: block,
            Orientation: Orientation.X
        );
    }

    public void CycleForward()
    {
        var newIdx = Array.IndexOf(_orientationOrder, _orientation) + 1;
        if (newIdx >= _orientationOrder.Length)
        {
            newIdx = 0;
            Flip();
        }
        BlockOrientation = BlockOrientation with
        {
            Orientation = _orientationOrder[newIdx]
        };
    }

    public void CycleBackward()
    {
        var newIdx = Array.IndexOf(_orientationOrder, _orientation) - 1;
        if (newIdx < 0)
        {
            newIdx = _orientationOrder.Length - 1;
            Flip();
        }
        BlockOrientation = BlockOrientation with
        {
            Orientation = _orientationOrder[newIdx]
        };
    }

    public void Flip()
    {
        BlockOrientation = BlockOrientation with
        {
            Block = new Block
            (
                First: BlockOrientation.Block.Second,
                Second: BlockOrientation.Block.First
            )
        };
    }
}