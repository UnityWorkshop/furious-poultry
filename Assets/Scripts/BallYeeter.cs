using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Serialization;

public class BallYeeter : MonoBehaviour
{
    [SerializeField] private float forceValue;
    [FormerlySerializedAs("ballPrefab")] [SerializeField] private List <Poultry> ballPrefabs;
    [SerializeField] private Transform yeetPos;

    [SerializeField] private Transform zeplinYeetPos;
    [SerializeField] private Transform currentFocus;
    
    
    public int sensX;
    public int sensY;

    private float _xRotation;
    private float _yRotation;
    private ClampableIndex _currentPrefabIndex;
    private Poultry _currentPoultry;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!ballPrefabs.Any())
        {
            throw new Exception("no balls?");
        }

        _currentPrefabIndex = new ClampableIndex(0 , 0, ballPrefabs.Count -1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        { 
            _currentPrefabIndex.DecrementIndex();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentPrefabIndex.IncrementIndex();
        }
        
        if (_currentPoultry.IsDead())       
        {
            currentFocus = zeplinYeetPos;
        }    
        transform.position = currentFocus.position;
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentFocus == zeplinYeetPos)   
        {
            Vector3 yeetPower = transform.forward * forceValue;
            Poultry instantiated = Instantiate(ballPrefabs[_currentPrefabIndex.Index], yeetPos.position, Quaternion.identity);
            instantiated.AddForce(yeetPower);
            currentFocus = instantiated.transform;
            

        }
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    
    
}
