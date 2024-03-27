using System;
using System.Collections.Generic;
using System.Linq;
using com.github.UnityWorkshop.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using furious_poultry.unity;
using NSubstitute.Extensions;
using UnityEngine;
using SystemVector3 = System.Numerics.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public class BallYeeterAuthoring : MonoBehaviour, ITransformProvider, IInputProvider
    { 
        private float _xRotation;
        private float _yRotation;
        
        private PoultryAuthoring _currentPoultryAuthoring;

        [SerializeField] BallYeeterDefinition config;
        private Player _player;

        public void OnValidate()
        {
            if (!config.ballPrefabs.Any())
            {
                throw new Exception("no balls?");
            }
        }

        protected BallYeeterService BallYeeterService;
        BallYeeter _ballYeeter;
    
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _player = new Player(config.ballPrefabs.Count);
            _currentPrefabIndex = new ClampableIndex(0 , 0, ballPrefabs.Count -1);
            _ballYeeter = new BallYeeter(10);
            BallYeeterService = new BallYeeterService(ballPrefabs.Count - 1, _ballYeeter, this);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _player.PreviousPoultry();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _player.NextPoultry();
            }
        
            if (Input.GetKeyDown(KeyCode.Mouse0)) ExecutePrimaryAction();

            TryPlayerPositionUpdate();
        
            MouseLook();
        
            TryResetFocus();

            if (Input.GetKeyDown(KeyCode.R))
            {
                _currentPoultryAuthoring.Destruct();
            }
        }

        //--//
    
        private void TryPlayerPositionUpdate()
        {
            if (config.currentFocus)
            {
                transform.position = config.currentFocus.position;
                config.currentFocus.transform.rotation = transform.rotation;
            }
        }
    
        private void MouseLook() // actual name for looking around with a mouse in a game source: wikipedia
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * config.sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * config.sensY;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
       
        }
    
        private void TryResetFocus()
        {
            if (_currentPoultryAuthoring && _currentPoultryAuthoring.IsDead() || !config.currentFocus)       
            {
                config.currentFocus = config.zeplinYeetPos;
            }  
        }

        private void ExecutePrimaryAction()
        {
            if (config.currentFocus == config.zeplinYeetPos)
            {
                DestroyAllAbilityLeftovers();
                Vector3 yeetPower = transform.forward * config.forceValue;
                _currentPoultryAuthoring = Instantiate(config.ballPrefabs[_player.CurrentIndex], config.yeetPos.position, Quaternion.identity);
                _currentPoultryAuthoring.Initialize();
                _currentPoultryAuthoring.AddForce(yeetPower);  
                config.currentFocus = _currentPoultryAuthoring.transform;
                return;
            }
            _currentPoultryAuthoring.DoPrimaryAbility(transform.forward);
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
        public bool PressedKeySwitchPoultryPrevious => Input.GetKeyDown(KeyCode.A);
        public bool PressedKeySwitchPoultryNext => Input.GetKeyDown(KeyCode.D);
        public bool PressedKeyResetPoultry => Input.GetKeyDown(KeyCode.R);
        public bool PressedKeyShoot => Input.GetKeyDown(KeyCode.Mouse0);
    }
}
