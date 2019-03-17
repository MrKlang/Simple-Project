using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public MovementDirection DefinedMovementDirection;
    public float MovementDistance;
    public GameObject Player;

    private Vector3 LocalOriginalPosition;
    private Sequence PlatformMovementSequence;

    public void Start()
    {
        LocalOriginalPosition = transform.localPosition;

        CreateAndStartSequence();
    }

    private void CreateAndStartSequence()
    {
        PlatformMovementSequence = DOTween.Sequence();

        switch (DefinedMovementDirection)
        {
            case MovementDirection.Up:
                PlatformMovementSequence.Append(transform.DOLocalMoveY(transform.localPosition.y + MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
            case MovementDirection.Down:
                PlatformMovementSequence.Append(transform.DOLocalMoveY(transform.localPosition.y - MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
            case MovementDirection.Left:
                PlatformMovementSequence.Append(transform.DOLocalMoveZ(transform.localPosition.z + MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
            case MovementDirection.Right:
                PlatformMovementSequence.Append(transform.DOLocalMoveZ(transform.localPosition.z - MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
            case MovementDirection.Front:
                PlatformMovementSequence.Append(transform.DOLocalMoveX(transform.localPosition.x + MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
            case MovementDirection.Back:
                PlatformMovementSequence.Append(transform.DOLocalMoveX(transform.localPosition.x - MovementDistance, 3.0f).SetEase(Ease.InOutSine));
                break;
        }

        PlatformMovementSequence.AppendInterval(1.0f);
        PlatformMovementSequence.Append(transform.DOLocalMove(LocalOriginalPosition, 3.0f).SetEase(Ease.InOutSine));
        PlatformMovementSequence.AppendInterval(1.0f);
        PlatformMovementSequence.SetLoops(-1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && Player.transform.parent == null)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
}

public enum MovementDirection
{
    Up=0,
    Down=1,
    Left=2,
    Right=3,
    Front=4,
    Back=5
}
