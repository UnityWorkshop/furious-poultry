using System.Collections.Generic;
using com.github.UnityWorkshop.furious_poultry.domain.interfaces;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.domain.entities
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