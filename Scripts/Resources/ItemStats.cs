using Godot;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class ItemStats : Resource {
        [Export]
        public int Durability;
        [Export]
        public int Defense;
    }
}