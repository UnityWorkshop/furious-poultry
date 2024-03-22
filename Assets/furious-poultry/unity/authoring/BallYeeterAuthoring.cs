using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;
using SystemVector3 = System.Numerics.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class BallYeeterAuthoring : MonoBehaviour, ITransformProvider
    { 
        [SerializeField] private List <PoultryAuthoring> ballPrefabs;
        [SerializeField] private Transform yeetPos;

        [SerializeField] private Transform zeplinYeetPos;
        [SerializeField] private Transform currentFocus;
    
    
        public int sensX;
        public int sensY;

        private float _xRotation;
        private float _yRotation;
        private ClampableIndex _currentPrefabIndex;
        private PoultryAuthoring _currentPoultryAuthoring;
        protected BallYeeterService BallYeeterService;
    
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
                _currentPoultryAuthoring = Instantiate(ballPrefabs[_currentPrefabIndex.Index], yeetPos.position, Quaternion.identity);
                _currentPoultryAuthoring.Initialize();
                _currentPoultryAuthoring.AddForce(BallYeeterService.CalculatedYeetPower().ToUnity());  
                currentFocus = _currentPoultryAuthoring.transform;
                return;
            }
            _currentPoultryAuthoring.DoPrimaryAbility();
        }

        private void DestroyAllAbilityLeftovers()
        {
            //GameObject[] leftoversToDelete = GameObject.FindGameObjectsWithTag("AbilityLeftovers");
            if (_currentPoultryAuthoring is null) return;
            foreach (var leftOver in _currentPoultryAuthoring.abilityLeftOvers)
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

        public SystemVector3 Forward => transform.forward.ToSystem();
    }
}
