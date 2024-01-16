using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallYeeter : MonoBehaviour
{
    [SerializeField] float forceValue;
    [SerializeField] private Rigidbody ballPrefab;
    public int sensX;
    public int sensY;

    private float xRotation;
    private float yRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 yeetPower = transform.forward * forceValue;
            Rigidbody instantiated = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            instantiated.AddForce(yeetPower);
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
