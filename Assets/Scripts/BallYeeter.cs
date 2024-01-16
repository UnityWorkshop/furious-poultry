using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BallYeeter : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] float forceValue;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 yeetPower = Vector3.forward * forceValue;
            _rigidbody.AddForce(yeetPower);
        }
    }
}
