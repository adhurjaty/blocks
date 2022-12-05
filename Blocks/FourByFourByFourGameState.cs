namespace Blocks;

public class FourByFourByFourGameState : IGameState
{
    public Space[,,] Board { get; private set; } = new Space[4, 4, 4];
    public List<Block> RemainingBlocks { get; private set; }

    public FourByFourByFourGameState()
    {
        var startingBlocks = new List<(Block, int)>()
        {
            (new Block(First: Color.WHITE, Second: Color.WHITE), 3),
            (new Block(First: Color.BLACK, Second: Color.BLACK), 3),
            (new Block(First: Color.WHITE, Second: Color.BLACK), 12),
            (new Block(First: Color.RED, Second: Color.BLACK), 7),
            (new Block(First: Color.RED, Second: Color.WHITE), 7),
        };

        RemainingBlocks = startingBlocks
            .SelectMany(blockInfo =>
            {
                var (block, num) = blockInfo;
                return Enumerable.Range(0, num).Select(_ => block);
            }).ToList();
    }

    private void AssertValidStartingBlocks(List<(Block, int)> startingBlocks)
    {
        var numberOfBlocks = startingBlocks.Sum(x => x.Item2);
        if(numberOfBlocks != 32)
            throw new Exception($"Incorrect number of blocks: {numberOfBlocks}");

        var colorDict = startingBlocks.Aggregate(new Dictionary<Color, int>()
        {
            { Color.BLACK, 0 },
            { Color.WHITE, 0 },
            { Color.RED, 0 }
        },
        (dict, blockInfo) =>
        {
            var (block, num) = blockInfo;
            dict[block.First] += num;
            dict[block.Second] += num;
            return dict;
        });
        if(colorDict[Color.BLACK] != colorDict[Color.WHITE])
            throw new Exception("Unbalanced black vs. white");
    }

    public Block TakeBlock(Block block)
    {
        if (!RemainingBlocks.Remove(block))
            throw new Exception($"Block ({block.First}, {block.Second}) does not exist in remaining blocks");
        return block;
    }

    public void UpdateBoard(IEnumerable<ColorPosition> cubes)
    {
        foreach(var cube in cubes)
        {
            var cubePos = cube.Position;
            Board[cubePos.X, cubePos.Y, cubePos.Z] = cube.Color.ToSpace();
        }
    }
}
