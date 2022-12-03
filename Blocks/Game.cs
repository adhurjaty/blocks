using Blocks.Util;

namespace Blocks;

public class Game
{
    private readonly List<(Block, int)> _startingBlocks = new()
    {
        (new Block() { First = Color.WHITE, Second = Color.WHITE }, 3),
        (new Block() { First = Color.BLACK, Second = Color.BLACK }, 3),
        (new Block() { First = Color.WHITE, Second = Color.BLACK }, 12),
        (new Block() { First = Color.RED, Second = Color.BLACK }, 7),
        (new Block() { First = Color.RED, Second = Color.WHITE }, 7),
    };
    private readonly GameState _state;

    public Game()
    {
        AssertValidStartingBlocks();
        _state = new GameState()
        {
            Board = new Space[4, 4, 4],
            RemainingBlocks = _startingBlocks
                .SelectMany(blockInfo =>
                {
                    var (block, num) = blockInfo;
                    return Enumerable.Range(0, num).Select(_ => block);
                }).ToList()
        };
    }

    private void AssertValidStartingBlocks()
    {
        var numberOfBlocks = _startingBlocks.Sum(x => x.Item2);
        if(numberOfBlocks != 32)
            throw new Exception($"Incorrect number of blocks: {numberOfBlocks}");

        var colorDict = _startingBlocks.Aggregate(new Dictionary<Color, int>()
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

    // public bool PlaceBlock(BlockOrientation block)
    // {

    // }

    // public bool CanPlaceBlock(BlockOrientation block)
    // {
    //     return block.Orientation switch
    //     {
    //         Orientation.Y => true,
    //         _ => false
    //     };
    // }

    // public Score GetScore()
    // {

    // }
}

public record Score
(
    int Black = 0,
    int White = 0
);