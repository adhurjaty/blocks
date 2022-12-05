using Blocks.Util;

namespace Blocks;

public class Game
{
    private readonly IGameState _state;
    private readonly PlacementRules _rules;

    public Game(IGameState state)
    {
        _state = state;
        _rules = new PlacementRules(state.Board);
    }

    public BlockManipulator TakeBlock(Block block)
    {
        return new BlockManipulator(_state.TakeBlock(block));
    }

    public bool PlaceBlock(BlockOrientation block, Vec3<int> pos)
    {
        if (!CanPlaceBlock(block, pos)) return false;

        var (first, second) = block.GetColorPositions(pos);
        _state.UpdateBoard(new[] { first, second });
        return true;
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

    public Score GetScore()
    {
        var score = GetVolumeSpaces(
            new[] { 0, _state.Board.GetLength(0) - 1 },
            Enumerable.Range(0, _state.Board.GetLength(1)),
            Enumerable.Range(0, _state.Board.GetLength(2)))
            .Concat(GetVolumeSpaces(
                Enumerable.Range(0, _state.Board.GetLength(0)),
                Enumerable.Range(0, _state.Board.GetLength(1)),
                new[] { 0, _state.Board.GetLength(2) - 1 }
            ))
            .Concat(GetVolumeSpaces(
                Enumerable.Range(0, _state.Board.GetLength(0)),
                new[] { _state.Board.GetLength(1) - 1 },
                Enumerable.Range(0, _state.Board.GetLength(2))
            )).GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        return new Score
        (
            Black: score.GetValueOrDefault(Space.BLACK, 0),
            White: score.GetValueOrDefault(Space.WHITE, 0)
        );
    }

    private IEnumerable<Space> GetVolumeSpaces(
        IEnumerable<int> xs,
        IEnumerable<int> ys,
        IEnumerable<int> zs
    )
    {
        return xs.SelectMany(x =>
            ys.SelectMany(y =>
                zs.Select(z => _state.Board[x, y, z])));
    }
}

public record Score
(
    int Black = 0,
    int White = 0
);