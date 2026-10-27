
using UnityEngine;

public class WeaponWallRetract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform muzzle;
    [SerializeField] private LayerMask wallLayer;

    [Header("Wall Detection")]
    [SerializeField] private float weaponThickness = 0.12f;
    [SerializeField] private float wallPadding = 0.05f;
    [SerializeField] private float minDistanceFromPlayer = 0.3f;

    [Header("Movement")]
    [SerializeField] private float returnSpeed = 8f;

    private Vector3 _originalLocalPosition;
    private Vector3 _originalMuzzleLocalPosition;

    public bool IsBlocked { get; private set; }

    private void Awake()
    {
        if (player == null) player = transform.parent;

        if (player == null || muzzle == null)
        {
            Debug.LogError
            (
                "WeaponWallRetract is missing references!",
                this
            );

            enabled = false;
            return;
        }

        // Remember the original weapon and muzzle positions.
        _originalLocalPosition = transform.localPosition;

        _originalMuzzleLocalPosition = player.InverseTransformPoint(muzzle.position);
    }

    private void LateUpdate()
    {
        Vector2 origin = player.position;

        // Calculate where the muzzle would be if the
        // weapon were fully extended.
        Vector2 muzzlePosition = player.TransformPoint(_originalMuzzleLocalPosition);

        Vector2 difference = muzzlePosition - origin;

        float weaponReach = difference.magnitude;

        if (weaponReach < 0.001f)
            return;

        Vector2 direction = difference.normalized;

        // Detect walls in front of the weapon.
        RaycastHit2D hit = Physics2D.CircleCast
        (
            origin,
            weaponThickness,
            direction,
            weaponReach,
            wallLayer
        );

        IsBlocked = hit.collider != null;

        float retractAmount = 0f;

        if (IsBlocked)
        {
            float availableDistance = Mathf.Max(0f, hit.distance - wallPadding);

            retractAmount = Mathf.Clamp
            (
                weaponReach - availableDistance,
                0f,
                Mathf.Max(0f, weaponReach - minDistanceFromPlayer)
            );
        }

        // Convert world-space retraction into the
        // player's local coordinates.
        Vector3 localRetraction = player.InverseTransformVector(new Vector3(direction.x, direction.y, 0f) * retractAmount);

        Vector3 targetPosition = _originalLocalPosition - localRetraction;

        if (IsBlocked)
        {
            // Retract immediately to avoid clipping.
            transform.localPosition = targetPosition;
        }
        else
        {
            // Return smoothly to the normal position.
            transform.localPosition = Vector3.MoveTowards
            (
                transform.localPosition,
                _originalLocalPosition,
                returnSpeed * Time.deltaTime
            );
        }
    }
}