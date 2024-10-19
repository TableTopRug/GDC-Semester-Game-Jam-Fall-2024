using System;
using Godot;

public static class MatrixUtils 
{
    public static string GetMatrixAsFormattedString<T>(T[,] array, char[] labels, Func<T, char[], char> determineLabel) {
        string ret = "";

        GD.Print($"Labels: [{labels[0]}, {labels[1]}]");
        GD.Print($"Width: {array.GetLength(0)}, RowLength: {array.Length / array.GetLength(0)}");
        for (int i = 0; i < array.GetLength(0); i++) {
            for (int j = 0; j < array.Length / array.GetLength(0); j++) {
                // GD.Print($"({i}, {j}) - Chosen Label: {determineLabel(array[i, j], labels)}");
                ret += $"{determineLabel(array[i, j], labels)} ";
            }
            ret += "\n";
        }

        return ret;
    }
}