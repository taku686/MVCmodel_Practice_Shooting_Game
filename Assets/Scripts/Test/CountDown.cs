using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text countDownText;
    RectTransform textTransform;
    int count = 3;
    int WaitForOneSecond;
    float timeCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        textTransform = countDownText.gameObject.GetComponent<RectTransform>();
        StartCoroutine(Hoge());
    }

    private void Update()
    {
        timeCount += Time.unscaledDeltaTime;
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
