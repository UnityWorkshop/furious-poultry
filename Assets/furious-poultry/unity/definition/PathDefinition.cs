using System;
using System.Collections.Generic;
using com.github.UnityWorkshop.furious_poultry.domain;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.unity.definition
{
    [Serializable]
    public class PathDefinition:IPath
    {
        [SerializeField] List<Transform> targets;

        public List<UnityEngine.Transform> Targets => targets;
        
    }
}