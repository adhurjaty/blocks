namespace Blocks;

public enum Color
{
    RED,
    BLACK,
    WHITE
}

public static class ColorExtensions
{
    public static Space ToSpace(this Color c)
    {
        return c switch
        {
            Color.RED => Space.RED,
            Color.WHITE => Space.WHITE,
            Color.BLACK => Space.BLACK,
            _ => throw new ArgumentException()
        };
    }
}
