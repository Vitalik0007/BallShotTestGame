using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float maxSize = 1f;
    [SerializeField] private float minSize = 0.2f;
    [SerializeField] private float scaleSpeed = 0.5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform target;
    [SerializeField] private BallMovement ballMovement;

    private GameObject currentProjectile;
    private bool isCharging;

    private void Start()
    {
        SetInitialSizeAndPosition();
    }

    private void Update()
    {
        if (!ballMovement.IsMoving)
            HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCharging();
        }
        else if (Input.GetMouseButton(0) && isCharging)
        {
            ChargeProjectile();
        }
        else if (Input.GetMouseButtonUp(0) && isCharging)
        {
            FireProjectile();
        }
    }

    private void StartCharging()
    {
        if (IsTooSmall())
        {
            GameEvents.PlayerLose();
            return;
        }

        isCharging = true;
        currentProjectile = Instantiate(projectilePrefab, spawnPos.position, Quaternion.identity);
    }

    private void ChargeProjectile()
    {
        float scaleAmount = scaleSpeed * Time.deltaTime;

        ScaleDownPlayer(scaleAmount);
        ScaleUpProjectile(scaleAmount);

        if (IsTooSmall())
        {
            //FireProjectile();
            GameEvents.PlayerLose();
        }
    }

    private void FireProjectile()
    {
        isCharging = false;

        if (currentProjectile == null) return;

        currentProjectile.AddComponent<SphereCollider>();

        Vector3 direction = (target.position - transform.position).normalized;
        currentProjectile.GetComponent<Projectile>().Launch(direction);

        currentProjectile = null;
    }

    private void SetInitialSizeAndPosition()
    {
        float yOffset = maxSize / 2.0f;
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        transform.localScale = Vector3.one * maxSize;
    }

    private void ScaleDownPlayer(float amount)
    {
        transform.localScale = Vector3.Max(transform.localScale - Vector3.one * amount, Vector3.one * minSize);
    }

    private void ScaleUpProjectile(float amount)
    {
        if (currentProjectile != null)
        {
            currentProjectile.transform.localScale += Vector3.one * amount;
        }
    }

    private bool IsTooSmall() => transform.localScale.x <= minSize;
}
