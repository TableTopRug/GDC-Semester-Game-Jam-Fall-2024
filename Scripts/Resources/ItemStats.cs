using Godot;
using System.Collections.Generic;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class ItemStats : Resource, IStats {
        [Export]
        public int MaxDurability;
        [Export]
        public int CurrentDurability;
        [Export]
        public int Defense;
        [Export]
        public int Uses;
        
        public List<Modifier<ItemStatMods>> itemStatMods;
    }
}