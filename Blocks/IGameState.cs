namespace Blocks;

public interface IGameState
{
    Space[,,] Board { get; }
    List<Block> RemainingBlocks { get; }

    Block TakeBlock(Block block);
    void UpdateBoard(IEnumerable<ColorPosition> cubes);
}