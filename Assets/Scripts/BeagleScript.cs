using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeagleScript : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        Enemies collidedEnemyScript = collision.gameObject.GetComponent<Enemies>();
        if (collidedEnemyScript is not null)
            collidedEnemyScript.Damage(damage);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
