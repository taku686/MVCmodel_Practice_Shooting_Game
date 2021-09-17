using UnityEngine;
using UnityEngine.UI;

public class CanvasView : MonoBehaviour
{
    GameController gameController;



    [SerializeField]
    Text deadEnemyText;

    public void Init(GameController controller)
    {
        this.gameController = controller;
    }

    public void DeadEnemy(int deadEnemyCount)
    {
        deadEnemyText.text = deadEnemyCount.ToString();
    }
}
