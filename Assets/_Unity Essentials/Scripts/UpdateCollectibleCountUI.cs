using UnityEngine;
using TMPro;
using System;

public class UpdateCollectibleCountUI : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;
    private AudioSource audioSource;

    [Header("Efectos finales")]
    public GameObject completionEffectPrefab; // Prefab del efecto de partículas
    public AudioClip completionSound;         // Sonido al completar
    public Transform player;                  // Referencia al jugador

    private bool allCollected = false;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("Se requiere un componente TextMeshProUGUI en el mismo GameObject.");
            return;
        }

        // Configuración de audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (completionSound != null)
            audioSource.clip = completionSound;

        // Verificar referencia del jugador
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("No se encontró el jugador. Asigna la referencia en el Inspector.");
        }

        UpdateCollectibleDisplay();
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        // Buscar objetos de tipo Collectible
        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectibleType, FindObjectsSortMode.None).Length;

        // Buscar objetos de tipo Collectible2D si existen
        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;

        if (totalCollectibles != 0)
        {
            collectibleText.text = $"Collectibles remaining: {totalCollectibles}";
        }

        // Si ya no quedan coleccionables
        if (totalCollectibles == 0 && !allCollected)
        {
            allCollected = true;
            TriggerCompletionEffect();
        }
    }

    private void TriggerCompletionEffect()
    {
        Debug.Log("¡Todos los coleccionables fueron recolectados!");

        // 1️⃣ Cambiar texto final
        collectibleText.text = "¡Nivel completado!";

        // 2️⃣ Instanciar efecto de partículas en la posición del jugador
        if (completionEffectPrefab != null)
        {
            Vector3 effectPosition = player != null ? player.position : Vector3.zero;
            GameObject effectInstance = Instantiate(completionEffectPrefab, effectPosition, Quaternion.identity);

            // Si tiene partículas, reproducirlas y destruir al terminar
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(effectInstance, ps.main.duration + ps.main.startLifetime.constantMax);
            else
                Destroy(effectInstance, 3f); // Por seguridad, destrucción automática
        }

        // 3️⃣ Reproducir sonido de victoria
        if (completionSound != null && audioSource != null)
            audioSource.PlayOneShot(completionSound);
    }
}
