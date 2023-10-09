using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STM_MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;


    [SerializeField] private float duration = 5;
    [SerializeField] private Ease ease = Ease.Linear;

    Sequence mySequence = DOTween.Sequence();
    // Start is called before the first frame update
    void Start()
    {
        // start and end are children
        start.SetParent(null);
        end.SetParent(null);

        transform.position = start.position;

        mySequence.Append(transform.DOMove(end.position, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(ease));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<STM_PlayerController>() != null)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<STM_PlayerController>() != null)
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDestroy()
    {
        mySequence.Kill();
    }
}
