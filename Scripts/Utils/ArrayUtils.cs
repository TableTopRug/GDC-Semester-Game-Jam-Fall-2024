using System.Collections.ObjectModel;
using System.Linq;

public static class ArrayUtils {
    public static bool ElementsContain<T>(this T[] vals, T thing) {
        foreach (T val in vals) {
            if (vals.Contains(thing)) {
                return true;
            }
        }

        return false;
    }
}