using System;
using UnityEngine;
using UnityEngine.Android;

public class BasicEnemyBehaviour : MonoBehaviour
{
    
    private enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Search,
        Attack
    }
    
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask visionLayer;
    
    [Header("Attributes")]
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float detectionAngle = 90f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float searchDuration = 3f;
    [SerializeField] private float movementSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _lastKnownPosition;
    private float _searchTimer;
    private EnemyState _currentState;
    private Vector2 facingDirection = Vector2.right;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentState = EnemyState.Idle;
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;

            case EnemyState.Patrol:
                UpdatePatrol();
                break;

            case EnemyState.Chase:
                UpdateChase();
                break;

            case EnemyState.Search:
                UpdateSearch();
                break;

            case EnemyState.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdateIdle()
    {
        if (CanSeePlayer())
        {
            _lastKnownPosition = player.position;
            _currentState = EnemyState.Chase;
        }
    }

    private void UpdatePatrol()
    {
        if (CanSeePlayer())
        {
            _lastKnownPosition = player.position;
            _currentState = EnemyState.Chase;
        }

        //TODO: Patrol State
    }
    
    private void UpdateChase()
    {
        if (CanSeePlayer()) //Player in sight
        {
            _lastKnownPosition = player.position;

            float distance = Vector2.Distance
            (
                transform.position,
                player.position
            );

            if (distance <= attackRange)
            {
                _currentState = EnemyState.Attack;
                return;
            }

            MoveTowards(player.position);
        }
        else // Player out of sight
        {
            _searchTimer = searchDuration;
            _currentState = EnemyState.Search;
        }
    }
    
    private void UpdateSearch()
    {
        if (CanSeePlayer()) // Player in sight
        {
            _lastKnownPosition = player.position;
            _currentState = EnemyState.Chase;
            return;
        }

        
        float distance = Vector2.Distance //Move to last Known position
        (
            transform.position,
            _lastKnownPosition
        );

        if (distance > 0.2f)
        {
            MoveTowards(_lastKnownPosition);
        }
        else
        {
            LookAround();
        }

        _searchTimer -= Time.deltaTime;

        if (_searchTimer <= 0f)
        {
            _currentState = EnemyState.Patrol;
        }
    }
    
    private bool CanSeePlayer()
    {
        Vector2 toPlayer = player.position - (Vector3)transform.position;
        
        if (toPlayer.sqrMagnitude > detectionRange * detectionRange) // Distance check
        {
            return false;
        }
        
        float angle = Vector2.Angle(facingDirection, toPlayer); // Cone check

        if (angle > detectionAngle / 2f)
        {
            return false;
        }

        
        RaycastHit2D hit = Physics2D.Raycast
        (
            transform.position,
            toPlayer.normalized,
            toPlayer.magnitude,
            visionLayer
        );

        
        if (hit.collider == null) // Nothing was hit
        {
            return false;
        }
        
        return hit.collider.CompareTag("Player"); // The first hit is Player
    }
    
    private void MoveTowards(Vector2 target)
    {
        Vector2 direction =
            (target - _rb.position).normalized;

        _rb.MovePosition
        (
            _rb.position +
            direction * movementSpeed * Time.fixedDeltaTime
        );

        if (direction.sqrMagnitude > 0.01f)
        {
            facingDirection = direction;
        }
    }
    
    private void LookAround()
    {
      //TODO: Logic about searching around
    }
    
    private void UpdateAttack()
    {
        if (!CanSeePlayer())
        {
            _currentState = EnemyState.Search;
            _searchTimer = searchDuration;
            return;
        }

        float distance = Vector2.Distance
        (
            transform.position,
            player.position
        );

        if (distance > attackRange)
        {
            _currentState = EnemyState.Chase;
            return;
        }

        //TODO: AttackLogic
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (_currentState == EnemyState.Search)
        {
            Gizmos.color = Color.yellow;
        }
        
        if (Application.isPlaying && CanSeePlayer())
        {
            Gizmos.color = Color.red;
        }
        
        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
        
        Vector3 leftBoundary =
            Quaternion.Euler(0f, 0f, detectionAngle / 2f) *
            facingDirection;

        Vector3 rightBoundary =
            Quaternion.Euler(0f, 0f, -detectionAngle / 2f) *
            facingDirection;

        Gizmos.DrawLine(
            transform.position,
            transform.position + leftBoundary * detectionRange
        );

        Gizmos.DrawLine(
            transform.position,
            transform.position + rightBoundary * detectionRange
        );
        
        if (Application.isPlaying && player != null)
        {
            Gizmos.DrawLine(
                transform.position,
                player.position
            );
        }
    }
}
