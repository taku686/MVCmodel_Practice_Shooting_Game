using UnityEngine;
using System.Linq;

public class EnemyModel : CharacterModel
{
    public const float MoveWaitTime = 0f;
    private EnemyCreateController enemyCreateController;
   

    public void Init(EnemyCreateController enemyCreateController)
    {
        //this.gameController = gameController;
        this.shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
        this.enemyCreateController = enemyCreateController;
    }

    public void Move(Vector3 moveSpeed,Transform enemyTransform)
    {
       
            float xVelocity = moveSpeed.x * Time.deltaTime;
            float zVelocity = moveSpeed.z * Time.deltaTime;
            //Debug.Log("xVelocity" + xVelocity);
            enemyTransform.position += new Vector3(xVelocity, 0, zVelocity);
        
    }

    public void Shot(float shellSpeed,Transform shotPos)
    {
            ShellView shell = shellController.GetShell();
            shell.shellRigidbody.velocity = new Vector3(0, 0, shellSpeed);
            shell.transform.position = shotPos.position;
    }


}
