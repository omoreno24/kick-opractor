using UnityEngine;
using Player.Movement;
using System;
using UnityEditor.Animations;

namespace Player
{
    public class PlayerController : MonoHelper
    {
        public Animator animator;
        public PlayerProperties properties;
        IPlayerLocomotion movementController;

        void Start()
        {
            movementController = gameObject.GetComponent<IPlayerLocomotion>();
        }

        void Update()
        {
            Vector3 motion = new Vector3(Input.GetAxis(properties.HorizontalInput), 0, Input.GetAxis(properties.VerticalInput));

            if (Input.GetButton(properties.LightHitButton) || Input.GetButton(properties.HeavyHitButton))
            {
                motion = Vector3.zero;
            }

            var running = motion != Vector3.zero;
            animator.SetBool("Running", running);

            if (running && !properties.FX.isPlaying) properties.FX.PlayOneShot(properties.WalkClip, 0.2f);
           
            movementController.Move(motion);

            CheckPlayerDeath();
        }

        private void CheckPlayerDeath()
        {
            if (properties.HealthPoints <= 0) Death();
        }

        void Death()
        {
            Debug.Log("Player Death");
            shaker.Shake(0.7f, 4);
        }
    }
}
