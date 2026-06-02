using UnityEngine;

namespace Utilities
{
    public struct HitInfo
    {
        public GameObject OtherObject; //Can be removed if we want
        public Vector3 HitPoint;
        public int Damage;
    }
}