using Blocks.Util;

namespace Blocks;

public class Game
{
    private readonly List<(Block, int)> _startingBlocks = new()
    {
        (new Block(First: Color.WHITE, Second: Color.WHITE), 3),
        (new Block(First: Color.BLACK, Second: Color.BLACK), 3),
        (new Block(First: Color.WHITE, Second: Color.BLACK), 12),
        (new Block(First: Color.RED, Second: Color.BLACK), 7),
        (new Block(First: Color.RED, Second: Color.WHITE), 7),
    };
    private readonly GameState _state;
    private readonly PlacementRules _rules;

    public Game()
    {
        AssertValidStartingBlocks();

        var board = new Space[4, 4, 4];
        _state = new GameState()
        {
            Board = board,
            RemainingBlocks = _startingBlocks
                .SelectMany(blockInfo =>
                {
                    var (block, num) = blockInfo;
                    return Enumerable.Range(0, num).Select(_ => block);
                }).ToList()
        };
        _rules = new PlacementRules(board);
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

    public BlockManipulator TakeBlock(Block block)
    {
        _state.RemainingBlocks.Remove(block);
        return new BlockManipulator(block);
    }

    public bool PlaceBlock(BlockOrientation block)
    {
        
    }

    public bool CanPlaceBlock(BlockOrientation block, Vec3<int> pos)
    {
        if (!_rules.IsInBounds(pos)) throw new ArgumentException("Position is out of bounds");
        if (!_rules.IsInEmptySpace(pos)) throw new ArgumentException("Position is not empty");

        var (firstCube, secondCube) = block.GetColorPositions(pos);

        return _rules.IsOnValidColor(firstCube.Color, firstCube.Position)
            && _rules.IsInBounds(secondCube.Position)
            && _rules.IsInEmptySpace(secondCube.Position)
            && block.Orientation switch
            {
                Orientation.Y => true,
                _ => _rules.IsOnValidColor(secondCube.Color, secondCube.Position)
            };
    }

    // public Score GetScore()
    // {

    // }
}

public record Score
(
    int Black = 0,
    int White = 0
);