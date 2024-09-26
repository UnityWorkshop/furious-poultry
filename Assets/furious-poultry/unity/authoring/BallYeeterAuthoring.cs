using System;
using System.Linq;
using com.github.UnityWorkshop.Assets.furious_poultry.application.interfaces;
using com.github.UnityWorkshop.Assets.furious_poultry.application.services;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.domain.aggregates;
using com.github.UnityWorkshop.furious_poultry.unity.definition;
using UnityEngine;
using SystemVector3 = System.Numerics.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity.authoring
{
    public class BallYeeterAuthoring : MonoBehaviour, ITransformProvider
    { 
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

    
        void Start()
        {
            _player = new Player(config.ballPrefabs.Count);
        }

        void Update()
        {
            TryPlayerPositionUpdate();
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
    
        
    
        public void TryResetFocus()
        {
            if (_currentPoultryAuthoring && _currentPoultryAuthoring.IsDead() || !config.currentFocus)       
            {
                config.currentFocus = config.zeplinYeetPos;
            }  
        }

        public void ExecutePrimaryAction()
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
        public void DestroyPoultry()
        {
            _currentPoultryAuthoring.Destruct();
        }
        public void NextPoultry()
        {
            _player.NextPoultry();
        }
        public void PreviousPoultry()
        {
            _player.PreviousPoultry();
        }
    }
}
