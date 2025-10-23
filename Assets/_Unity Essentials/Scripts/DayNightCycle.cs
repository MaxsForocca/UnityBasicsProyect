using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    [Tooltip("Duration of a full day (in seconds).")]
    [SerializeField, Min(1f)]
    private float dayDuration = 120f;

    [Tooltip("Axis of rotation for the sun (usually X or Z).")]
    [SerializeField]
    private Vector3 rotationAxis = Vector3.right;

    [Header("Time Control")]
    [Tooltip("Start the cycle automatically on play.")]
    [SerializeField]
    private bool autoRotate = true;

    private void Update()
    {
        if (!autoRotate) return;

        // Calculate the rotation speed (degrees per second)
        float degreesPerSecond = 360f / dayDuration;

        // Rotate smoothly around the chosen axis
        transform.Rotate(rotationAxis, degreesPerSecond * Time.deltaTime, Space.World);
    }

    // Allow runtime editing in the Inspector
    private void OnValidate()
    {
        if (dayDuration <= 0f)
            dayDuration = 1f;
    }
}
