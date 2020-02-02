using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, IPlayerLocomotion
    {
        // Use this for initialization
        public float MoveSpeed = 5;
        public bool IsMoving { get => motion.magnitude > 0; }

        //private field
        private CharacterController _controller;
        private Vector3 motion;
        private Vector3 LookAtPosition;
        private Vector3 Impact;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void AddImpact(Vector3 dir, float ammount)
            => Impact = dir * ammount;

        public void Move(Vector3 motion)
        {
            this.motion = motion;
            FaceMotionDirection();

            motion.y = 0;

            if (Impact.magnitude > 0) ApplyImpact();
            else _controller.SimpleMove(motion * MoveSpeed);
        }

        private void FaceMotionDirection()
        {
            Debug.Log(IsMoving);
            LookAtPosition = IsMoving ? motion: LookAtPosition;

            if(LookAtPosition.magnitude > 0)
                transform.rotation = Quaternion.LookRotation(LookAtPosition);
        }

        private void ApplyImpact()
        {
            _controller.SimpleMove(Impact);
            Impact = Vector3.zero;
        }
    }
}
