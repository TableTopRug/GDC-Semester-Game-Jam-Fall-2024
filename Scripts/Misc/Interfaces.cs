using System;
using System.Collections.Generic;
using Godot.Collections;

public interface IStats<T> where T : struct, Enum
{
    public abstract List<Modifier<T>> Modifiers {get; set;}

    // public abstract List<Modifier<Enum>> GetModifiers();
    // public abstract void AddModifiers(List<Modifier<T>> mods);
    public virtual float GetModifiers<V>(V type) where V : struct, Enum {
            var mods = Modifiers.FindAll(mod => mod.Type.Equals(type));
            float ret = 1;
            Array<float> mults = new Array<float>();


            foreach (var mod in mods) {
                switch (mod.Type) {
                    case Modifier<T>.ModType.ADD:
                        ret += mod.Value;
                        break;
                    case Modifier<T>.ModType.SUB:
                        ret -= mod.Value;
                        break;
                    case Modifier<T>.ModType.MULT:
                        mults.Add(mod.Value);
                        break;
                }
            }

            mults.Sort();

            foreach (float val in mults) {
                ret *= val;
            }

            return ret;
        }
}

public interface IIStats<V>: IStats<V> where V : struct, Enum
{
    public new abstract List<Modifier<T>> GetModifiers<T>(T type) where T: struct, Enum;
}