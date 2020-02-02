using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerProperties
    {
        public float HealthPoints = 100;

        public string HorizontalInput;
        public string VerticalInput;
        public string LightHitButton;
        public string HeavyHitButton;

        public AudioClip WalkClip;
        public AudioClip[] PunchClips;
        public AudioSource FX;
        public AudioSource FX2;
    }
}