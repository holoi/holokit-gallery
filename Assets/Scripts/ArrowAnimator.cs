using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimator : MonoBehaviour
{
    const float MOVE_AMOUNT = 30f;
    const float MOVE_DURATION = 1f;

    private void OnEnable()
    {
        MoveArrowUpAndDown();   
    }

    private void MoveArrowUpAndDown()
    {
        LeanTween.moveLocalY(gameObject, transform.localPosition.y - MOVE_AMOUNT, MOVE_DURATION)
            .setEaseInOutSine()
            .setOnComplete(MoveArrowUp);
    }

    private void MoveArrowUp()
    {
        LeanTween.moveLocalY(gameObject, transform.localPosition.y + MOVE_AMOUNT, MOVE_DURATION)
            .setEaseInOutSine()
            .setOnComplete(MoveArrowUpAndDown);
    }
}
