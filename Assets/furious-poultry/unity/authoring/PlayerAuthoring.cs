using System;
using System.Collections.Generic;
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
    public class PlayerAuthoring : MonoBehaviour, ITransformProvider
    {

        public float forceValue = 1000;
        
        public List <PoultryAuthoring> ballPrefabs = new List<PoultryAuthoring>();
        public Transform projectileOrigin;

        public Transform zelpinPoultrySpawnPoint;
        Transform currentFocus;
        bool _poultryHeld;

        Player _player;
        PoultryAuthoring _currentPoultryAuthoring;

        public void OnValidate()
        {
            if (!ballPrefabs.Any())
            {
                throw new Exception("no balls?");
            }
        }

    
        void Start()
        {
            _player = new Player(ballPrefabs.Count);
            if (currentFocus == null)
                ResetFocus();
        }

        void Update()
        {
            PoultryPositionUpdate();
            PlayerPositionUpdate();
            ResetFocus();
        }

        void PoultryPositionUpdate()
        {
            if (_poultryHeld)
            {
                _currentPoultryAuthoring.ResetForce();
                _currentPoultryAuthoring.transform.position = zelpinPoultrySpawnPoint.transform.position;
            }
        }

        void PlayerPositionUpdate()
        {
            if (currentFocus)
            {
                transform.position = currentFocus.position;
                currentFocus.transform.rotation = transform.rotation;
            }
        }


        void ResetFocus()
        {
            if (_currentPoultryAuthoring && _currentPoultryAuthoring.IsDead() || !currentFocus)       
            {
                _currentPoultryAuthoring = Instantiate(ballPrefabs[_player.CurrentIndex], projectileOrigin.position, Quaternion.identity);
                _currentPoultryAuthoring.Initialize();
                currentFocus = _currentPoultryAuthoring.transform;
                _poultryHeld = true;
            }  
        }
        
        void DestroyAllAbilityLeftovers()
        {
            //GameObject[] leftoversToDelete = GameObject.FindGameObjectsWithTag("AbilityLeftovers");
            if (_currentPoultryAuthoring is null) return;
            foreach (var leftOver in _currentPoultryAuthoring.abilityLeftOvers)
            {
                Destroy(leftOver);
            }
        }
        

        public void ExecutePrimaryAction()
        {
            if (_poultryHeld)
            {
                DestroyAllAbilityLeftovers();
                Vector3 yeetPower = transform.forward * forceValue;
                _currentPoultryAuthoring.AddForce(yeetPower);  
                currentFocus = _currentPoultryAuthoring.transform;
                _poultryHeld = false;
                return;
            }
            _currentPoultryAuthoring.DoPrimaryAbility(transform.forward);
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
