using System.Collections.Generic;
using Godot;


public partial class Effect<T, V>: Resource where T: IStats where V: struct, System.Enum {
    public List<Modifier<V>> Modifiers;


    public void Apply(T target) {
        target.Modifiers.AddRange(this.Modifiers);
    }
}