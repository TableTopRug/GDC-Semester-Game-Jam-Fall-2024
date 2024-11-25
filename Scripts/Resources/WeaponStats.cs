using Godot;
using System.Collections.Generic;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class WeaponStats : ItemStats {
        [Export]
        public float[] Damage = new float[(int)DamageType.num_types_damage];

        public List<Modifier<DamageType>> Modifiers;
    }
}