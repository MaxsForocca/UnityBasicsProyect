using UnityEngine;
using UnityEngine.Audio;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationSpeed;
    public GameObject onCollectEffect;
    private AudioSource onCollectAudio;
    public AudioClip onCollectAudioClip;
    private float volume = 0.5f;
    void Start()
    {
        onCollectAudio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha tocado el objeto");
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            if (onCollectAudioClip != null)
                onCollectAudio.PlayOneShot(onCollectAudioClip, volume);
            else
                Debug.LogWarning("No hay un AudioClip asignado en " + name);

            Destroy(gameObject, 0.1f);
        }
    }
}
