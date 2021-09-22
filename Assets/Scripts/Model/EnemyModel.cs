using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class EnemyModel : CharacterModel
{

    [SerializeField]
    private Material shellMaterial;
    public Transform playerTransform;
    


    public void Init()
    {
        //this.gameController = gameController;
        this.shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
    }

    public interface Move
    {
        void Move(Vector3 moveSpeed, Transform enemyTransform);
    }


    public void StraightMove(Vector3 moveSpeed, Transform enemyTransform)
    {
        float xVelocity = moveSpeed.x * Time.deltaTime;
        float zVelocity = moveSpeed.z * Time.deltaTime;
        //Debug.Log("xVelocity" + xVelocity);
        enemyTransform.position += new Vector3(xVelocity, 0, zVelocity);
    }

    public void ZigzagMove(Vector3 moveSpeed, Transform enemyTransform)
    {
        float xVelocity = moveSpeed.x * Mathf.Sin(Time.time*2);
        float zVelocity = moveSpeed.z * Time.deltaTime;
        enemyTransform.position += new Vector3(xVelocity, 0, zVelocity);
    }
    [SerializeField]
    private Vector3 endDir = Vector3.zero;

    public void ChaseMove(Vector3 moveSpeed, Transform enemyTransform, Transform targetTransform)
    {
        Vector3 moveDir = enemyTransform.position - targetTransform.position;
        
        if (enemyTransform.position.z < targetTransform.position.z + 5)
        {
            enemyTransform.position += endDir * moveSpeed.z * Time.deltaTime;
        }
        else
        {
            enemyTransform.position += moveDir.normalized * moveSpeed.z * Time.deltaTime;
            endDir = moveDir.normalized;
        }       
    }

    public void Shot(float shellSpeed,Transform shotPos)
    {
            ShellView shell = shellController.GetShell(shellMaterial);
            shell.shellRigidbody.velocity = new Vector3(0, 0, shellSpeed);
            shell.transform.position = shotPos.position;
    }

    public void EnemyCreate(Enum enemyType, int shellSpeed, Vector3 moveSpeed, EnemyCreateController enemyCreateController, List<EnemyView> enemyViewList, GameObject enemyObj, Transform enemyCreatePos)
    {
        EnemyView enemyView = Instantiate(enemyObj, EnemyCreatePos(enemyCreatePos), Quaternion.identity)
                              .GetComponent<EnemyView>();
        enemyView.GetComponent<Collider>().isTrigger = true;
        float shotWaitTime;
        switch (enemyType)
        {
            case EnemyCreateController.EnemyType.Weak:
                shotWaitTime = 2;
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, shotWaitTime, EnemyCreateController.EnemyType.Weak);
                break;
            case EnemyCreateController.EnemyType.Normal:
                shotWaitTime = 1;
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, shotWaitTime, EnemyCreateController.EnemyType.Normal);
                break;
            case EnemyCreateController.EnemyType.Strong:
                shotWaitTime = 1;
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, shotWaitTime, EnemyCreateController.EnemyType.Strong);
                break;
            case EnemyCreateController.EnemyType.Boss:
                shotWaitTime = 1;
                enemyView.Init(enemyCreateController, this, shellSpeed, moveSpeed, shotWaitTime, EnemyCreateController.EnemyType.Boss);
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
     //   gameObject.SetActive(false);
    }

}
