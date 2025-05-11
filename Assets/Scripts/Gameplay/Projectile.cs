using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float radiusMultiplier;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.velocity = new Vector3(direction.x, 0f, direction.z) * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            float radius = transform.localScale.x * radiusMultiplier;
            ExplosionEffect.InfectNearby(transform.position, radius);
        }

        Destroy(gameObject);
    }
}
