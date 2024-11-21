using System;
using System.Collections.Generic;


public static class DisctionaryUtils {
    /// <summary>
    ///     Gets a list of [KeyValuePairs] from a dictionary that are similar to eachother based on the provided comperator function
    /// </summary>
    /// <typeparam name="KT">The type of the Key for the [Dictionary]</typeparam>
    /// <typeparam name="VT">The type of the Value for the [Dictionary]</typeparam>
    /// <param name="dict">The [Dictionary] being searched</param>
    /// <param name="extendedKey">The Key you want to comapre to all of the Keys in the [Dictionary]</param>
    /// <param name="Like">The comperator function</param>
    /// <returns>A [List] containing all matching [KeyValuePair]s in th dictionary</returns>
    public static List<KeyValuePair<KT, VT>> GetKeyValuePaisKeyLike<KT, VT>(this Dictionary<KT, VT> dict, KT extendedKey, Func<KT, KT, bool> Like) {
        List<KeyValuePair<KT, VT>> ret = new List<KeyValuePair<KT, VT>>();

        foreach (KeyValuePair<KT, VT> pair in dict) {
            if (Like(extendedKey, pair.Key)) {
                ret.Add(pair);
            }
        }

        return ret;
    }


    /// <summary>
    /// Gets the Value from a dictionary that has a similar key to the given one based on the provided comperator function
    /// </summary>
    /// <typeparam name="KT">The type of the Key for the [Dictionary]</typeparam>
    /// <typeparam name="VT">The type of the Value for the [Dictionary]</typeparam>
    /// <param name="dict">The [Dictionary] being searched</param>
    /// <param name="extendedKey">The Key you want to comapre to all of the Keys in the [Dictionary]</param>
    /// <param name="Like">The comperator function</param>
    /// <returns>The first value whose key matches the given one or [default]</returns>
    public static VT GetFirstValueKeyLikeOrDefault<KT, VT>(this Dictionary<KT, VT> dict, KT extendedKey, Func<KT, KT, bool> Like) {
        foreach (KT key in dict.Keys) {
            if (Like(extendedKey, key)) {
                return dict.GetValueOrDefault(key);
            }
        }

        return default(VT);
    }

    /// <summary>
    ///     Gets a list of [KeyValuePairs] from a dictionary that are similar to eachother based on the provided comperator function
    /// </summary>
    /// <typeparam name="KT">The type of the Key for the [Dictionary]</typeparam>
    /// <typeparam name="VT">The type of the Value for the [Dictionary]</typeparam>
    /// <param name="dict">The [Dictionary] being searched</param>
    /// <param name="extendedKey">The Key you want to comapre to all of the Keys in the [Dictionary]</param>
    /// <param name="Like">The comperator function</param>
    /// <returns>A matching [KeyValuePair] in the [Dictionary] or [default]</returns>
    public static KeyValuePair<KT, VT> GetFirstKeyValueKeyLikeOrDefault<KT, VT>(this Dictionary<KT, VT> dict, KT extendedKey, Func<KT, KT, bool> Like) {
        foreach (KeyValuePair<KT, VT> pair in dict) {
            if (Like(extendedKey, pair.Key)) {
                return pair;
            }
        }

        return default(KeyValuePair<KT, VT>);
    }

    /// <summary>
    ///     Gets a list of Values from a dictionary that are similar to eachother based on the provided comperator function
    /// </summary>
    /// <typeparam name="KT">The type of the Key for the [Dictionary]</typeparam>
    /// <typeparam name="VT">The type of the Value for the [Dictionary]</typeparam>
    /// <param name="dict">The [Dictionary] being searched</param>
    /// <param name="extendedKey">The Key you want to comapre to all of the Keys in the [Dictionary]</param>
    /// <param name="Like">The comperator function</param>
    /// <returns>A [List] containing all matching Values in th dictionary</returns>
    public static List<VT> GetValuesKeyLike<KT, VT>(this Dictionary<KT, VT> dict, KT extendedKey, Func<KT, KT, bool> Like) {
        List<VT> ret = new List<VT>();

        foreach (KeyValuePair<KT, VT> pair in dict) {
            if (Like(extendedKey, pair.Key)) {
                ret.Add(pair.Value);
            }
        }

        return ret;
    }
}