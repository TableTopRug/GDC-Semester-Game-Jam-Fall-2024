using Godot;
using System.Collections.Generic;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class ItemStats : Resource {
        [Export]
        public int MaxDurability;
        [Export]
        public int CurrentDurability;
        [Export]
        public int Defense;
        
        public List<Modifier<ItemStatMods>> Modifiers;

        public enum ItemStatMods {
            MAX_DURABILITY,
            MURABILITY_CHANGE,
            DEFENSE,
            num_types_modifiers
        }
    }
}