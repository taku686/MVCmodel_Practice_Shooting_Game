using UnityEngine;

public class EnemyView : CharacterView,EnemyModel.Move
{
    protected EnemyModel enemyModel;
    protected EnemyCreateController enemyCreateController;
    public int shellSpeed;
    public Vector3 moveSpeed;
    public float shotWaitTime;
    public float timeCount;
    [SerializeField]
    protected Transform shotPos;
    protected CanvasController canvasController;
    protected EnemyCreateController.EnemyType enemyType;

    public virtual void Init(EnemyCreateController enemyCreateController,EnemyModel enemyModel,int shellSpeed,Vector3 moveSpeed,float shotWaitTime,EnemyCreateController.EnemyType enemyType)
    {
        this.enemyModel = enemyModel;
        this.enemyCreateController = enemyCreateController;
        this.shellSpeed = shellSpeed;
        this.moveSpeed = moveSpeed;
        this.shotWaitTime = shotWaitTime;
        this.canvasController = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
        this.enemyType = enemyType;
    }

    protected virtual void Update()
    {
        //Debug.Log("活きてますよ");
        var taskMove = new TaskManager.Task(EnemyCreateController.MoveWaitTime, EnemyMove, TaskManager.Task.Type.Time);
        this.enemyCreateController.TaskManager.Add(taskMove);
        timeCount += Time.unscaledDeltaTime;
        if(timeCount > this.shotWaitTime)
        {
            var taskShot = new TaskManager.Task(EnemyCreateController.ShotWaitTime, Shot, TaskManager.Task.Type.Time);
            this.enemyCreateController.TaskManager.Add(taskShot);
            timeCount = 0f;
        }
    }

    private void EnemyMove()
    {
        Move(moveSpeed, this.transform);
    }

    public virtual void Move(Vector3 moveSpeed, Transform enemyTransform)
    {

    }

    private void Shot()
    {
        enemyModel.Shot(shellSpeed, shotPos);
    }

    private void Dead()
    {
        enemyModel.Die(this.gameObject);
        int deadEnemyCount = enemyCreateController.deadEnemyCount++;
        //Debug.Log(deadEnemyCount);
        canvasController.DeadEnemy(deadEnemyCount);
    }

    private void OnTriggerEnter(Collider other)
    {
      ShellView shellView =  other.GetComponent<ShellView>();

        if(shellView == null)
        {
            return;
        }
        Dead();
    }

  
}
