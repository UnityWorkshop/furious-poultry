using System;
using System.Collections.Generic;
using com.github.UnityWorkshop.furious_poultry.unity.authoring;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.definition
{
    [Serializable]
    public class BallYeeterDefinition
    {
        public float forceValue = 1000;
        public float sensX = 900;
        public float sensY = 900;
        
        public List <PoultryAuthoring> ballPrefabs;
        public Transform yeetPos;

        public Transform zeplinYeetPos;
        public Transform currentFocus;
    }
}