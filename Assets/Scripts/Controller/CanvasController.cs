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
    private RectTransform countDownTextTransform;
    private GameController gameController;
    private int gameStartWaitTime = 3;

    public override void Init()
    {
        base.Init();
        canvasModel = GetComponent<CanvasModel>();
        countDownTextTransform = countDownText.gameObject.GetComponent<RectTransform>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
       
    }

    public IEnumerator CountDown(Action<bool> callback)
    {
        bool result = false;
        StartButton.gameObject.SetActive(false);
        var corutine = canvasModel.CountDownTimer(countDownText, this.gameStartWaitTime, countDownTextTransform, (r => result = r));
        yield return StartCoroutine(corutine);
        callback(result);
    }
}
