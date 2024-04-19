using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.domain.aggregates;
using com.github.UnityWorkshop.furious_poultry.unity.definition;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.authoring
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
                MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
                if(menuHandler != null)
                    menuHandler.UpdateWarthogAmount();
                Destroy(gameObject);
                //Debug.Log("Got'em");
            }

            //CheckCollision();
        }

        

        public void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Ground"))
            {
                //Debug.Log("hit ground");
                Warthog.Landed();
            }
        }

        public void Damage(float dmg)
        {
            Warthog.Damage(dmg);
        }
        
    }
}