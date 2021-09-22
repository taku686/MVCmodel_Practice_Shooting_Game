using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasModel))]
public class CanvasController : BaseController
{
    [SerializeField] Text countDownText;
    [SerializeField]public Button StartButton;
    private CanvasModel canvasModel;
    private CanvasView canvasView;
    private RectTransform countDownTextTransform;
    private int gameStartWaitTime = 3;

    public override void Init()
    {
        base.Init();
        canvasModel = GetComponent<CanvasModel>();
        canvasView = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasView>();
        countDownTextTransform = countDownText.gameObject.GetComponent<RectTransform>();
    }

    public IEnumerator CountDown(Action<bool> callback)
    {
        bool result = false;
        StartButton.gameObject.SetActive(false);
        var corutine = canvasModel.CountDownTimer(countDownText, this.gameStartWaitTime, countDownTextTransform, (r => result = r));
        yield return StartCoroutine(corutine);
        callback(result);
    }

    public void DeadEnemy(int deadEnemyCount)
    {
        canvasView.DeadEnemy(deadEnemyCount);
    }
}
