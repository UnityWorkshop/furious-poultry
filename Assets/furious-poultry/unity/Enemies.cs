using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class Enemies : MonoBehaviour
    {
        [SerializeField] private int health = 50;

        public void Update()
        {
            if (health <= 0)
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
