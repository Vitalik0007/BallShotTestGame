using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform ballObject;
    [SerializeField] private Transform door;

    private void Start()
    {
        lineRenderer.SetPosition(0, ballObject.position - new Vector3(0f, ballObject.transform.localScale.y / 2.0f - 0.01f, 0f));
        lineRenderer.SetPosition(1, door.position - new Vector3(0f, door.transform.position.y - 0.01f, 0f));

        lineRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        float width = ballObject.localScale.x;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
