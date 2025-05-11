using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float obstacleCheckDistance = 1f;

    [Header("References")]
    [SerializeField] private Transform goal;

    private Rigidbody ballRb;
    private Collider ballCollider;

    private bool isGrounded = true;
    private bool isMoving = false;

    public bool IsMoving
    {
        get { return isMoving; }
        private set { }
    }

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (goal == null) return;

        if (isGrounded && IsObstacleAhead())
        {
            StopMovement();
            return;
        }

        MoveTowardsGoal();

        if (isGrounded)
        {
            Jump();
        }
    }

    private void StopMovement()
    {
        isMoving = false;
        ballRb.velocity = Vector3.zero;
    }

    private void MoveTowardsGoal()
    {
        isMoving = true;

        Vector3 direction = GetHorizontalDirectionToGoal();
        Vector3 velocity = new Vector3(direction.x * moveSpeed, ballRb.velocity.y, direction.z * moveSpeed);
        ballRb.velocity = velocity;
    }

    private void Jump()
    {
        if (Mathf.Abs(ballRb.velocity.y) < 0.01f)
        {
            ballRb.velocity = new Vector3(ballRb.velocity.x, jumpForce, ballRb.velocity.z);
            isGrounded = false;
        }
    }

    private bool IsObstacleAhead()
    {
        Vector3 direction = GetHorizontalDirectionToGoal();
        Vector3 origin = ballRb.position;
        Vector3 size = ballCollider.bounds.size * 0.5f;

        if (Physics.BoxCast(origin, size, direction, out RaycastHit hit, Quaternion.identity, obstacleCheckDistance))
        {
            return hit.collider.GetComponent<Obstacle>() != null;
        }

        return false;
    }

    private Vector3 GetHorizontalDirectionToGoal()
    {
        Vector3 direction = goal.position - transform.position;
        direction.y = 0f;
        return direction.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (goal == null || ballCollider == null)
            return;

        Vector3 direction = (goal.position - transform.position).normalized;
        direction.y = 0f;
        Vector3 origin = transform.position;
        Vector3 size = ballCollider.bounds.size;
        Vector3 center = origin + direction * obstacleCheckDistance * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
