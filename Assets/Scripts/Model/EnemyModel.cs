using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class EnemyModel : CharacterModel
{
    
    [SerializeField]
    private Material shellMaterial;
    
    

    public void Init()
    {
        //this.gameController = gameController;
        this.shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
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
            ShellView shell = shellController.GetShell(shellMaterial);
            shell.shellRigidbody.velocity = new Vector3(0, 0, shellSpeed);
            shell.transform.position = shotPos.position;
    }

    public void EnemyCreate(Enum enemyType,int shellSpeed,Vector3 moveSpeed,EnemyCreateController enemyCreateController,List<EnemyView> enemyViewList,GameObject enemyObj,Transform enemyCreatePos)
    {
        EnemyView enemyView = Instantiate(enemyObj, EnemyCreatePos(enemyCreatePos), Quaternion.identity)
                              .GetComponent<EnemyView>();

        switch (enemyType)
        {
            case EnemyCreateController.EnemyType.Weak:
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, 2);
                break;
            case EnemyCreateController.EnemyType.Normal:
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, 1);
                break;
            case EnemyCreateController.EnemyType.Strong:
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, 1);
                break;
            case EnemyCreateController.EnemyType.Boss:
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, 2);
                break;
            default:
                enemyView = null;
                break;
        }
        enemyViewList.Add(enemyView);
    }

    private Vector3 EnemyCreatePos(Transform enemyCreatePos)
    {
        Vector3 pos = new Vector3(Random.Range((int)-enemyCreatePos.position.x - 10, (int)enemyCreatePos.position.x + 10),
                                 enemyCreatePos.position.y,
                                 enemyCreatePos.position.z
                                 );
        return pos;

    }

    public void Die(GameObject gameObject)
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

}
