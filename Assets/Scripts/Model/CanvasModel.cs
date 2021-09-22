using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CanvasModel : MonoBehaviour
{
    private const int WaitForOneSecond = 1;
    public IEnumerator CountDownTimer(Text countDownText,int count,RectTransform textTransform,Action<bool> callback)
    {
        if (countDownText == null || count < 0)
        {         
           callback(false);
          yield  break;
        }

        while (count > 0)
        {
            DOTween.Sequence()
                .OnStart(() =>
                {
                    countDownText.text = count.ToString();
                    countDownText.gameObject.SetActive(true);
                })
                .AppendInterval(0.4f)
                .Append(textTransform.DOLocalMove(new Vector3(0, -200, 0), 0.6f))
                .Join(countDownText.DOFade(0, 0.6f))
                .OnComplete(() =>
                {
                    Debug.Log("完了" + count);
                   
                    count--;
                    textTransform.localPosition = Vector3.zero;
                    countDownText.color = Color.black;
                   

                });
            yield return new WaitForSeconds(1);
        }
        countDownText.text = "GO!!";

        DOTween.Sequence()
           .OnStart(() => {
               countDownText.gameObject.SetActive(true);
           })
           .Append(textTransform.DOScale(Vector3.one * 0.8f, 1f))
           .Join(countDownText.DOFade(0, 1f))
           .OnComplete(() => {
               textTransform.gameObject.SetActive(false);
               
               Debug.Log("Go!!!!!!!");
              
           });
        callback(true);
        yield return new WaitForSeconds(1);
    }
}
