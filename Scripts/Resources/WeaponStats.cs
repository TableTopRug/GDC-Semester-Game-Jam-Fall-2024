using Godot;
using System;
using System.Collections.Generic;


namespace WaltuhsMagicAdventure
{
	[GlobalClass]
	public partial class WeaponStats : ItemStats, IIStats<DamageType> {
        [Export]
        public float[] Damage = new float[(int)DamageType.num_types_damage];

        public new List<Modifier<DamageType>> Modifiers { get; set; }


        public List<Modifier<T>> GetModifiers<T>(T type) where T : struct, Enum
        {
            if (typeof(T)== typeof(DamageType)) 
            {
                return new List<Modifier<T>>((IEnumerable<Modifier<T>>)Modifiers);
            } 
            else if (typeof(T)== typeof(ItemStatMods)) 
            {
                return new List<Modifier<T>>((IEnumerable<Modifier<T>>)base.Modifiers);
            } 
            else 
            {
                throw new ArgumentException();
            }
        }
    }
}