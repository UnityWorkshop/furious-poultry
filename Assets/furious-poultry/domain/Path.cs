using System.Collections.Generic;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.domain
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