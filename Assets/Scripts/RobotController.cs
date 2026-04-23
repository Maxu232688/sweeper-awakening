using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sweeper Robot Movement Controller
/// Sprint 0: Basic movement with WASD
/// </summary>
public class RobotController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 200f;

    [Header("References")]
    [SerializeField] private Rigidbody rb;

    private Vector3 inputDirection;

    void Update()
    {
        var kb = UnityEngine.InputSystem.Keyboard.current;
        if (kb == null) return;

        float horizontal = 0f;
        float vertical = 0f;
        if (kb.aKey.isPressed) horizontal -= 1f;
        if (kb.dKey.isPressed) horizontal += 1f;
        if (kb.sKey.isPressed) vertical -= 1f;
        if (kb.wKey.isPressed) vertical += 1f;

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (inputDirection.magnitude > 0.1f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
