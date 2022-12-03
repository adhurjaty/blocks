using Blocks.Util;

namespace Blocks;

public class PlacementRules
{
    private Space[,,] _board;

    public PlacementRules(Space[,,] board)
    {
        _board = board;
    }

    public bool IsInBounds(Vec3<int> pos)
    {
        return new[] { pos.X, pos.Y, pos.Z }
            .Select((dim, i) => dim >= 0 && dim < _board.GetLength(0))
            .All(x => x);
    }

    public bool IsOnValidColor(Color color, Vec3<int> pos)
    {
        if (IsOnBase(pos)) return true;

        var spaceUnder = _board[pos.X, pos.Y - 1, pos.Z];
        return color switch
        {
            Color.BLACK => new[] { Space.WHITE, Space.RED }.Contains(spaceUnder),
            Color.WHITE => new[] { Space.BLACK, Space.RED }.Contains(spaceUnder),
            Color.RED => spaceUnder != Space.EMPTY,
            _ => throw new ArgumentException("Invalid color")
        };
    }

    private bool IsOnBase(Vec3<int> pos)
    {
        return pos.Y == 0;
    }

    public bool IsInEmptySpace(Vec3<int> pos)
    {
        return _board[pos.X, pos.Y, pos.Z] == Space.EMPTY;
    }
}