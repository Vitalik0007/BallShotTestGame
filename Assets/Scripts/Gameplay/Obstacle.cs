using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration = 0.6f;
    [SerializeField] private float flashInterval = 0.1f;

    private MeshRenderer meshRenderer;
    private bool isInfected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Infect()
    {
        if (isInfected) return;

        isInfected = true;
        StartCoroutine(FlashAndDestroy());
    }

    private IEnumerator FlashAndDestroy()
    {
        float elapsed = 0f;
        bool isFlashed = false;

        while (elapsed < flashDuration)
        {
            meshRenderer.material = isFlashed ? originalMaterial : flashMaterial;
            isFlashed = !isFlashed;

            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        Destroy(transform.parent.gameObject);
    }
}
