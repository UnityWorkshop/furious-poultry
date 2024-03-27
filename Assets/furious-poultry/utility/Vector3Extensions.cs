using System.Numerics;
using UnityVector3 = UnityEngine.Vector3;

namespace com.github.UnityWorkshop.furious_poultry.unity
{
    public static class Vector3Extensions
    {
        public static Vector3 ToSystem(this UnityVector3 unityVector)
        {
            return new Vector3(unityVector.x, unityVector.y, unityVector.z);
        }
        
        public static UnityVector3 ToUnity(this Vector3 systemVector)
        {
            return new UnityVector3(systemVector.X, systemVector.Y, systemVector.Z);
        }
    }
}