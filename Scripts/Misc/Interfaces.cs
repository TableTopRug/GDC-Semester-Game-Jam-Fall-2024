using System;
using System.Collections.Generic;

public interface IStats<T> where T : struct, Enum
{
    public abstract List<Modifier<T>> Modifiers {get; set;}

    // public abstract List<Modifier<Enum>> GetModifiers();
    // public abstract void AddModifiers(List<Modifier<T>> mods);
}