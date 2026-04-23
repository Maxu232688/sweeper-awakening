using UnityEngine;

/// <summary>
/// Camera follows the robot from above at a fixed angle
/// Sprint 0: Simple follow camera
/// </summary>
public class FollowCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0, 8, -5);

    [Header("Follow Settings")]
    [SerializeField] private float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Always look at target
        transform.LookAt(target);
    }
}
