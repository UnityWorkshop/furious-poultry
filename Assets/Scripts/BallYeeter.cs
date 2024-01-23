using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BallYeeter : MonoBehaviour
{
    [SerializeField] private float forceValue;
    [SerializeField] private Rigidbody ballPrefab;
    [SerializeField] private Transform yeetPos;
    
    public int sensX;
    public int sensY;

    private float _xRotation;
    private float _yRotation;

    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            yeet(startTime);
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    void yeet(float startTime)
    {
        float elapsedTime = Time.time - startTime;
        float yeetMultiplier = 0;
        yeetMultiplier = elapsedTime + 1f;
        if (yeetMultiplier>3)
        { 
            yeetMultiplier = 3;
        }
        Vector3 yeetPower = transform.forward * (forceValue * yeetMultiplier);
        Rigidbody instantiated = Instantiate(ballPrefab, yeetPos.position, Quaternion.identity);
        instantiated.AddForce(yeetPower);
    }
}
