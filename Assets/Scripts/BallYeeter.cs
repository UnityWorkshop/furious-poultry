using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallYeeter : MonoBehaviour
{
    [SerializeField] float forceValue;
    [SerializeField] private Rigidbody ballPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 yeetPower = Vector3.forward * forceValue;
            Rigidbody instantiated = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            instantiated.AddForce(yeetPower);
        }
    }
}
