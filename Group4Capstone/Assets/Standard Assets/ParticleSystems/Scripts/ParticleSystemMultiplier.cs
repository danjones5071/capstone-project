using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // a simple script to scale the size, speed and lifetime of a particle system

        public float multiplier = 1;


        private void Start()
        {
            var systems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in systems)
            {
				ParticleSystem.MainModule main = system.main;
				ParticleSystem.MinMaxCurve minMax1 = main.startSize;
				ParticleSystem.MinMaxCurve minMax2 = main.startSpeed;
				ParticleSystem.MinMaxCurve minMax3 = main.startLifetime;

				minMax1.constant *= multiplier;
                minMax2.constant *= multiplier;
				minMax3.constant *= Mathf.Lerp(multiplier, 1, 0.5f);

				main.startSize = minMax1;
				main.startSpeed = minMax2;
				main.startLifetime = minMax3;

                system.Clear();
                system.Play();
            }
        }
    }
}
