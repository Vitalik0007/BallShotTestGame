using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform door;

    [Header("Door Settings")]
    [SerializeField] private float openDistance = 5f;
    [SerializeField] private Vector3 targetRotationEuler = new Vector3(0, 90, 0);
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private Ease openEase = Ease.OutQuad;

    private bool isOpen = false;

    private void Update()
    {
        if (!isOpen && Vector3.Distance(transform.position, player.position) <= openDistance)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        door.DORotate(targetRotationEuler, rotationDuration)
                .SetEase(openEase);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBallController playerBall))
        {
            GameEvents.PlayerWin();
        }
    }
}
