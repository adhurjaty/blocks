namespace Blocks;

public record GameState
{
    public Space[,,] Board { get; init; }
    public List<Block> RemainingBlocks { get; init; }
}