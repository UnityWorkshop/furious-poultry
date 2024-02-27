using System;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public abstract class WarthogAuthoring : MonoBehaviour
    {
        public WarthogDefinition WarthogDefinition;
        private Warthog _warthog;

        public void Start()
        {
            _warthog = new Warthog(WarthogDefinition.health);
        }

        public void Update()
        {
            if (_warthog.IsDead)
            {
                Destroy(gameObject);
                //Debug.Log("Got'em");
            }
        }

        public void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Ground"))
            {
                //Debug.Log("hit ground");
                _warthog.Kill();
            }
        }

        public void Damage(int dmg)
        {
            _warthog.Damage(dmg);
        }
    }
}
