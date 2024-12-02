using Godot;
using System.Collections.Generic;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class ItemStats : Resource, IStats<ItemStatMods> {
        [Export]
        public int MaxDurability;
        [Export]
        public int CurrentDurability;
        [Export]
        public int Defense;
        [Export]
        public int Uses;
        
        public List<Modifier<ItemStatMods>> Modifiers {get; set;}
    }
}