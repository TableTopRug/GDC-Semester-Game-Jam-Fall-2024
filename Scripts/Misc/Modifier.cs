using Godot;


public partial class Modifier<T>: Resource {
        public enum ModType {
            ADD,
            SUB,
            MULT,
            DIV,
            num_types_modtypes
        }

        public T Class;
        public ModType Type;
        public float Value;
    }