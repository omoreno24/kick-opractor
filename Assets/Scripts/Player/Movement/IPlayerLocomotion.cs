using UnityEngine;
using System.Collections;

namespace Player.Movement
{
    public interface IPlayerLocomotion
    {
        void Move(Vector3 motion);
        void AddImpact(Vector3 direction, float ammount);
    }
}
