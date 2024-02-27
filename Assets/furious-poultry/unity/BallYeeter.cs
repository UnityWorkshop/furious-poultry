using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;
using UnityEngine.Serialization;


namespace com.github.UnityWorkshop.furious_poultry.unity
{
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
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
       
        }
    
        private void TryResetFocus()
        {
            if (_currentPoultry && _currentPoultry.IsDead() || !currentFocus)       
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
                Vector3 yeetPower = transform.forward * forceValue;
                _currentPoultry = Instantiate(ballPrefabs[_currentPrefabIndex.Index], yeetPos.position, Quaternion.identity);
                _currentPoultry.AddForce(yeetPower);  
                currentFocus = _currentPoultry.transform;
                return;
            }
            _currentPoultry.DoPrimaryAbility();
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
                _currentPoultry.DestroyPoultry();
            }
        }
    
    }
}