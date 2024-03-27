using System;
using System.Collections.Generic;
using com.github.UnityWorkshop.furious_poultry.domain;
using com.github.UnityWorkshop.furious_poultry.domain.interfaces;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.definition
{
    [Serializable]
    public class PathDefinition:IPath
    {
        [SerializeField] List<Transform> targets;

        public List<Transform> Targets => targets;
        
    }
}