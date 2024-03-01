using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class EnemiesAuthoring : MonoBehaviour
    {
        public float health = 50;
        public Enemies Enemy { get; private set; }

        private void Start()
        {
            Enemy = new Enemies(health);
        }

        public void Update()
        {
            if (Enemy.Health <= 0)
            {
                Destroy(gameObject);
                //Debug.Log("Got'em");
            }
        }

        public void Damage(int damage)
        {
            health -= damage;
        }

        public void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Ground"))
            {
                //Debug.Log("hit ground");
                health = 0;
            }
        }
    }
}
