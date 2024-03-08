using System;
using System.Collections.Generic;
using furious_poultry.domain;
using UnityEngine;

namespace DefaultNamespace
{
    public class Path:IPath
    {
        public CyclicList<Transform> targets ;

        public List<Transform> Targets => targets.ToList();

        public Path(List<Transform> targets)
        {
            this.targets = new CyclicList<Transform>(targets);
        }
    }
}