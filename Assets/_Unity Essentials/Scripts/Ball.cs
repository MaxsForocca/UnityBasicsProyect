using System.Collections;
using UnityEngine;

namespace Assets._Unity_Essentials.Scripts
{
    public class Ball : MonoBehaviour
    {
        public AudioClip bounceAudioClip;
        private AudioSource audioSource;
        public float minImpactForce = 0.05f;
        public float multiplerVolume = 1.0f;
        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            float impactForce = collision.relativeVelocity.magnitude;
            if (impactForce > minImpactForce)
            {
                if (bounceAudioClip != null)
                {
                    float volume = Mathf.Clamp01(impactForce/5f )* multiplerVolume;
                    audioSource.PlayOneShot(bounceAudioClip, volume);
                }
                else
                {
                    Debug.LogWarning("No se asigno sonido de rebote " + name);
                }
            }
        }
    }
}