using System.Collections.Generic;
using UnityEngine;

namespace furious_poultry.domain
{
    public interface IPath
    {
        public List<Transform> Targets { get; }
    }
}