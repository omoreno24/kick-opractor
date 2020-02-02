using UnityEngine;
using Player.Movement;
using System;
using UnityEditor.Animations;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
    public class PlayerController : MonoHelper
    {
        public Animator animator;
        public PlayerProperties properties;
        IPlayerLocomotion movementController;
        bool isPunching = false;
        public int counter = 0;
        public Text CounterText;
        public GameObject hitbox;
        public bool CanPunch;

        void Start()
        {
            movementController = gameObject.GetComponent<IPlayerLocomotion>();
            CanPunch = true;
        }

        void Update()
        {
            CounterText.text = "Backpain fix Count: " + counter;

            if (isPunching == true && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))) { hitbox.SetActive(false); isPunching = false; animator.ResetTrigger("punch"); animator.SetBool("Punch", false); }


            Vector3 motion = new Vector3(Input.GetAxis(properties.HorizontalInput), 0, Input.GetAxis(properties.VerticalInput));
            var running = motion != Vector3.zero;

            if (Input.GetButton(properties.LightHitButton) || Input.GetButton(properties.HeavyHitButton))
            {
                motion = Vector3.zero;
                running = false;

                if(CanPunch) StartCoroutine(Punch());
            }

            animator.SetBool("Running", running);

            if (running && !properties.FX.isPlaying) properties.FX.PlayOneShot(properties.WalkClip, 0.2f);

            if(!isPunching)
                movementController.Move(motion);
            else
                movementController.Move(motion * 0.2f);

            CheckPlayerDeath();
        }

        public IEnumerator Punch()
        {
            CanPunch = false;
            ResetPunch();
            animator.SetTrigger("punch");
            //animator.SetBool("Punch", true);
            animator.SetBool("Running", false);
            PlayPunchSound(UnityEngine.Random.Range(0,properties.PunchClips.Length-1));
            hitbox.SetActive(true);
            isPunching = true;

            yield return new WaitForSeconds(0.2f);

            CanPunch = true;
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

        public void ResetPunch()
        {
            animator.SetBool("Punch", false);
        }

        public void PlayPunchSound(int i)
        {
            properties.FX.PlayOneShot(properties.PunchClips[i]);
        }
    }
}
