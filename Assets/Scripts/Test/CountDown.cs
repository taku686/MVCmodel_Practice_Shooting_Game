using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text countDownText;
    RectTransform textTransform;
    // Start is called before the first frame update
    void Start()
    {
        textTransform = countDownText.gameObject.GetComponent<RectTransform>();
        StartCoroutine(Hoge());
    }

   

    IEnumerator Hoge()
    {
        var coroutine = Wait();
        while (coroutine.MoveNext())
            yield return coroutine.Current;
        print("hoge");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }


}
