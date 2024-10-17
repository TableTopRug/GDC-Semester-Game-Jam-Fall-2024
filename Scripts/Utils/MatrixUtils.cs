using System;
using Godot;

public static class MatrixUtils {
    public static string GetMatrixAsFormattedString<T>(T[,] array, char[] labels, Func<T, char[], char> determineLabel) {
        string ret = "";

GD.Print($"Labels: [{labels[0]}, {labels[1]}]");
        for (int i = 0; i < array.Rank; i++) {
            for (int j = 0; j < array.Length / array.Rank; j++) {
                GD.Print($"Chosen Label {determineLabel(array[i, j], labels)}");
                ret += $"{determineLabel(array[i, j], labels)}";
            }
            ret += "\n";
        }

        return ret;
    }
}