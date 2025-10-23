using System.Collections;
using UnityEngine;

namespace Assets._Unity_Essentials.Scripts
{
    public class Block : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip fallSound;
        public float minImpactForce = 5f;
        public float multiplerVolume = 1.0f;
        // Use this for initialization
        void Start()
        {
            if (fallSound != null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.clip = fallSound;
            }
            else
            {
                Debug.LogWarning("No se asigno sonido de caida " + name);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            float impactForce = collision.relativeVelocity.magnitude;
            if (impactForce > 2f)
            {
                if (fallSound != null)
                {
                    float volume = Mathf.Clamp01(impactForce / 20f) * multiplerVolume;
                    audioSource.PlayOneShot(fallSound, volume);
                }
            }
        }
    }
}