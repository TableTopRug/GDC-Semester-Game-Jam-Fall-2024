public static class Extensions
{
    public static string ToString<T>(this T[] vals)
    {
        string res = "[";

        for (int i = 0; i < vals.Length; i++)
        {
            if (i != 0) 
            {
                res += ", ";
            }
            res += $"{vals[i]}";
        }

        return res + "]";
    }
}