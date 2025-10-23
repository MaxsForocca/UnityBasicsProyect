using UnityEngine;


public class DoorOpener : MonoBehaviour
{
    private Animator doorAnimator;
    public AudioClip AudioClip;
    private AudioSource audioSource;

    void Start()
    {
        // Get the Animator component attached to the same GameObject as this script
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // 3D sound
        audioSource.volume = 0.5f;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (or another specified object)
        if (other.CompareTag("Player")) // Make sure the player GameObject has the tag "Player"
        {
            if (doorAnimator != null)
            {
                // Trigger the Door_Open animation
                doorAnimator.SetTrigger("Door_Open");
                if (AudioClip != null) {
                    audioSource.PlayOneShot(AudioClip);
                }else
                {
                    Debug.LogWarning("AUDIO CLIP NO SELECCIONADO");
                }
            }
            // Desactiva el collider para evitar futuras colisiones
            GetComponent<Collider>().enabled = false;
        }
    }
}