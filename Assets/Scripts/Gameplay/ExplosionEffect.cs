using UnityEngine;

public static class ExplosionEffect
{
    public static void InfectNearby(Vector3 position, float radius)
    {
        Collider[] hit = Physics.OverlapSphere(position, radius);
        foreach (var col in hit)
        {
            if (col.TryGetComponent(out Obstacle obstacle))
            {
                col.GetComponent<Obstacle>().Infect();
            }
        }
    }
}