using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    private Transform exitTarget;

    [Header("Arrow Settings")]
    [SerializeField] private float distanceFromPlayer = 1.5f;
    [SerializeField] private float spriteAngleOffset = -90f;

    private void Awake()
    {
        GameObject exitObject = GameObject.Find("Exit");

        if (exitObject != null)
        {
            exitTarget = exitObject.transform;
        }
        else
        {
            Debug.LogError("Could not find Exit!");
        }
    }

    private void LateUpdate()
    {
        if (player == null || exitTarget == null)
            return;

        // Calculate direction toward the exit.
        Vector2 direction = (Vector2)(exitTarget.position - player.position);

        if (direction.sqrMagnitude < 0.0001f)
            return;

        direction.Normalize();

        // Arrow spins around the player.
        Vector2 arrowPosition = (Vector2)player.position + direction * distanceFromPlayer;

  
        transform.position = new Vector3
        (
            arrowPosition.x,
            arrowPosition.y,
            transform.position.z
        );

        // Rotate arrow to point toward exit.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler
        (
            0f,
            0f,
            angle + spriteAngleOffset
        );
    }

    public void ActivateArrow()
    {
        gameObject.SetActive(true);
    }
}