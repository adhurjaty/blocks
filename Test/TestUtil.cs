using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blocks.Test;

public static class TestUtil
{
    public static Space[,,] BoardFromFile(string filename)
    {
        var board = File.ReadAllText(filename)
            .Split("\n\n")
            .Select(level => level.Split("\n")
                .Select(x => x.ToCharArray()
                    .Select(y => (Space)((int)y - 48))
                    .ToArray())
                .ToArray())
            .ToArray();

        var outBoard = new Space[board[0][0].Length, board.Length, board[0].Length];

        for (int y = 0; y < board.Length; y++)
        {
            for (int z = 0; z < board[0].Length; z++)
            {
                for (int x = 0; x < board[0][0].Length; x++)
                {
                    outBoard[x, y, z] = board[y][z][x];
                }
            }
        }
        return outBoard;
    }
}
