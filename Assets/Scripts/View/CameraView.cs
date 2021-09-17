using UnityEngine;




public class CameraView : MonoBehaviour
{

    const float timeShakeRange = 0.25f;     // カメラシェイク実行時間
    const float shakeRange = 0.4f;          // カメラシェイクのシェイク値

    const float hitslowTimeWait = 0.1f;                 // スロー演出が開始されるまでの待機時間
    const float hitslowTime = hitslowTimeWait + 0.3f;   // スロー演出が持続する時間
    const float hitslowScale = 0.45f;                   // スロー演出のスケール値

    GameController controller;  // ゲームコントローラークラスの参照変数
    Vector3 basePos;            // カメラの初期位置を保持する変数
    float timeShakeUpdate;      // カメラシェイクの計測時間

    public void Init(GameController controller)
    {
        this.controller = controller;
        timeShakeUpdate = 0f;
    }

    // 勝敗決定処理
    public void GameSet()
    {
        // カメラの初期位置を保持
        basePos = transform.localPosition;
    }

    // カメラ演出（カメラシェイク、ヒットスロー）を開始する
    public void StartCameraEffect()
    {
        // 指定した時間までカメラシェイクを毎フレーム実行する
        var taskCameraShake = new TaskManager.Task(timeShakeRange, ShakeUpdate, TaskManager.Task.Type.LoopTime);
        this.controller.TaskManager.Add(taskCameraShake);

        // 指定した時間にスロー設定
        var taskSlow = new TaskManager.Task(hitslowTimeWait, SetSlow, TaskManager.Task.Type.Time);
        this.controller.TaskManager.Add(taskSlow);

        // 指定した時間にスロー設定をリセット
        var taskSlowReset = new TaskManager.Task(hitslowTime, SetSlowReset, TaskManager.Task.Type.Time);
        this.controller.TaskManager.Add(taskSlow);
    }

    // カメラシェイクの毎フレーム更新処理
    public void ShakeUpdate()
    {
        // Time.timeScaleに影響されないunscaledDeltaTimeをtimeに加算
        timeShakeUpdate += Time.unscaledDeltaTime;

        // 加算されるtimeを指定されたtimeRangeで割って割合を出す
        var range = shakeRange * (1 - timeShakeUpdate / timeShakeRange);

        // シェイクするカメラ座標をランダムに決める、rangeの割合で振れ幅を調整
        var x = basePos.x + Random.Range(-1f, 1f) * range;
        var y = basePos.y + Random.Range(-1f, 1f) * range;
        transform.localPosition = new Vector3(x, y, basePos.z);

        // 指定されたtimeRangeを経過していた場合は元のカメラ座標に戻す
        if (timeShakeUpdate >= timeShakeRange)
        {
            transform.localPosition = basePos;
        }
    }

    // 画面をスローに設定
    public void SetSlow()
    {
        Time.timeScale = hitslowScale;
    }

    // 画面をもとに戻す
    public void SetSlowReset()
    {
        Time.timeScale = 1.0f;
    }
}
