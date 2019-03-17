using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public GameObject Player;
    private Sequence PowerUpSequence;

    private void Start()
    {
        PowerUpSequence = DOTween.Sequence();
        var originalPosition = transform.localPosition;
        PowerUpSequence.Append(transform.DOLocalMoveY(originalPosition.y + 3.0f, 2.0f).SetEase(Ease.InOutSine));
        PowerUpSequence.Append(transform.DOLocalMoveY(originalPosition.y, 2.0f).SetEase(Ease.InOutSine));
        PowerUpSequence.SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            var playerController = Player.GetComponent<PlayerController>();
            playerController.OriginalSpeed *= 1.5f;
            playerController.RelativeJumpForceValue *= 1.5f;
            Destroy(gameObject);
        }
    }
}
