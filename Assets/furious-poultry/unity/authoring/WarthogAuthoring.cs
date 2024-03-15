using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity.definition;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class WarthogAuthoring : MonoBehaviour
    {
        public WarthogDefinition WarthogDefinition;
        public Warthog Warthog { get; private set; }

        public void Start()
        {
            Warthog = new Warthog(WarthogDefinition.health);
        }

        public void Update()
        {
            if (Warthog.IsDead)
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
                Warthog.Landed();
            }
        }

        public void Damage(int dmg)
        {
            Warthog.Damage(dmg);
        }
    }
}