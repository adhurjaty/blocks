using Blocks.Util;

namespace Blocks;

public class GameState
{
    public Space[,,] Board { get; init; }
    public List<Block> RemainingBlocks { get; init; }
}
