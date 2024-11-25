using Godot;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class WeaponStats : ItemStats {
        [Export]
        public float Damage;
        [Export]
        public DamageType DamageType;
    }
}