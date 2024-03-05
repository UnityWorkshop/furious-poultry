using System;
using System.Collections.Generic;
using furious_poultry.domain;
using UnityEngine;

namespace furious_poultry.unity
{
    [Serializable]
    public class PathDefinition:IPath
    {
        [SerializeField] List<Transform> targets;

        public List<UnityEngine.Transform> Targets => targets;
    }
}