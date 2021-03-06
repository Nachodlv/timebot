﻿using System;
using UnityEngine;

namespace Entities
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private bool instantKill;
        [SerializeField] private bool overrideInvincibility;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var damageReceiver = other.collider.GetComponent<DamageReceiver>();
            if (damageReceiver == null) return;
            damageReceiver.ReceiveDamage(damage, transform.position, instantKill, overrideInvincibility);
        }
    }
}