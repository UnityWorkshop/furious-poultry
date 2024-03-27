using System.Collections.Generic;
using UnityEngine;

namespace com.github.UnityWorkshop.furious_poultry.domain.interfaces
{
    public interface IPath
    {
        public List<Transform> Targets { get; }
    }
}