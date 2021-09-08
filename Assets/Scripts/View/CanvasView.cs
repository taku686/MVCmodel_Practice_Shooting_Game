using UnityEngine;

public class CanvasView : MonoBehaviour
{
    GameController controller;

    [SerializeField]
    GameObject WaitImage;   // 「いざ尋常に」
    [SerializeField]
    GameObject LoseImage;   // 「敗北」
    [SerializeField]
    GameObject WinImage;    // 「勝利」
    [SerializeField]
    GameObject OutImage;    // 「反則」
    [SerializeField]
    GameObject GoImage;     // 「勝負」

    public void Init(GameController controller, float executionTime)
    {
        this.controller = controller;
        var task = new TaskManager.Task(executionTime, Go);
        this.controller.TaskManager.Add(task);
        WaitImage.SetActive(true);
    }

    // 反則の場合、「反則」を表示
    public void Out()
    {
        WaitImage.SetActive(false);
        OutImage.SetActive(true);
    }

    // 合図「開始」を表示
    public void Go()
    {
        WaitImage.SetActive(false);
        GoImage.SetActive(true);
    }

    // 勝敗が決まった時
    public void GameSet()
    {
        // 「勝負」を非表示
        GoImage.SetActive(false);

        // 「勝利」か「敗北」を一定時間後に表示するタスク追加
        var taskGameMatch = new TaskManager.Task(GameController.GameMatchTime, GameMatch, TaskManager.Task.Type.Time);
        this.controller.TaskManager.Add(taskGameMatch);
    }

    // 勝敗を表示する時
    public void GameMatch()
    {
        switch (this.controller.IsExecution)
        {
            case GameController.Execution.Player:
                // 「勝利」表示
                WinImage.SetActive(true);
                break;
            case GameController.Execution.Enemy:
                // 「敗北」表示
                LoseImage.SetActive(true);
                break;
        }
    }
}
