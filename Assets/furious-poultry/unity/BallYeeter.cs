using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.domain;
using furious_poultry.unity;
using UnityEngine;
using UnityEngine.Serialization;


namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class BallYeeter : MonoBehaviour
    { 
        [SerializeField] private List <PoultryAuthoring> ballPrefabs;
        [SerializeField] private Transform yeetPos;

        [SerializeField] private Transform zeplinYeetPos;
        [SerializeField] private Transform currentFocus;

        private float _xRotation;
        private float _yRotation;
        private ClampableIndex _currentPrefabIndex;
        private PoultryAuthoring _currentPoultryAuthoring;

        [SerializeField] BallYeeterDefinition ballYeeterDefinition;
    
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
            PoultryCycling();
        
            if (Input.GetKeyDown(KeyCode.Mouse0)) ExecutePrimaryAction();

            TryPlayerPositionUpdate();
        
            MouseLook();
        
            TryResetFocus();

            TryManualPoultryReset();
        }

        //--//
    
        private void TryPlayerPositionUpdate()
        {
            if (currentFocus)
            {
                transform.position = currentFocus.position;
                currentFocus.transform.rotation = transform.rotation;
            }
        }
    
        private void MouseLook() // actual name for looking around with a mouse in a game source: wikipedia
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * ballYeeterDefinition.sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ballYeeterDefinition.sensY;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
       
        }
    
        private void TryResetFocus()
        {
            if (_currentPoultryAuthoring && _currentPoultryAuthoring.IsDead() || !currentFocus)       
            {
                currentFocus = zeplinYeetPos;
            }  
        }

        private void PoultryCycling()
        {
            if (Input.GetKeyDown(KeyCode.A))
            { 
                _currentPrefabIndex.DecrementIndex();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _currentPrefabIndex.IncrementIndex();
            }
        }
        private void ExecutePrimaryAction()
        {
            if (currentFocus == zeplinYeetPos)
            {
                DestroyAllAbilityLeftovers();
                Vector3 yeetPower = transform.forward * ballYeeterDefinition.forceValue;
                _currentPoultryAuthoring = Instantiate(ballPrefabs[_currentPrefabIndex.Index], yeetPos.position, Quaternion.identity);
                _currentPoultryAuthoring.Initialize();
                _currentPoultryAuthoring.AddForce(yeetPower);  
                currentFocus = _currentPoultryAuthoring.transform;
                return;
            }
            _currentPoultryAuthoring.DoPrimaryAbility();
        }

        private void DestroyAllAbilityLeftovers()
        {
            GameObject[] leftoversToDelete = GameObject.FindGameObjectsWithTag("AbilityLeftovers");
            foreach (GameObject leftOver in leftoversToDelete)
            {
                Destroy(leftOver);
            }
        }

        private void TryManualPoultryReset()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _currentPoultryAuthoring.Destruct();
            }
        }
    
    }
}
