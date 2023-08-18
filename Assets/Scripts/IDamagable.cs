using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public interface IDamagable
    {
        public float Health { get; set; }
        public void TakeDamage(float p_amount, Vector2 p_pos);
        public void RestoreHealth(float p_amount);
        public void OnDeath();
    }
}