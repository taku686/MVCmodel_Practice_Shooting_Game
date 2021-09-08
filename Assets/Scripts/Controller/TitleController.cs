using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : BaseController
{
    /// <summary>
    /// ゲームを開始する
    /// </summary>
    public void StartGame()
    {
        this.LoadScene("Game");
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Unityエディタならプレイ停止
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        // アプリ終了
        UnityEngine.Application.Quit();
#endif
    }
}
