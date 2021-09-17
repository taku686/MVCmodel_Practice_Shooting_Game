using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenTest : MonoBehaviour
{
    private void Start()
    {
        this.transform.DOMove(new Vector3(5, 0, 0), 3f)
            .SetEase(Ease.InOutBounce)
            .SetLoops(4,LoopType.Yoyo);
            
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.DOKill();
            this.transform.DOJump(new Vector3(5, 0, 0), 3, 3, 2)
            .OnComplete(
                () =>
                {
                    this.transform.DORotate(Vector3.up * 360, 1);
                });
            this.transform.GetComponent<MeshRenderer>().material.DOColor(Color.blue, 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.DOKill();
            var sequence = DOTween.Sequence();

            sequence.Append(this.transform.DOMove(new Vector3(4, 0, 4), 3));
            sequence.Append(this.transform.GetComponent<MeshRenderer>().material.DOFade(0, 3));
            Debug.Log("sequence開始");
            sequence.Play();
        }
    }
}
