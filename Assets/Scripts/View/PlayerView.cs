
using UnityEngine;


public class PlayerView : CharacterView
{
    PlayerModel playerModel;
    [SerializeField]
    private int shellSpeed;
    [SerializeField]
    private int moveSpeed;
    private Material shellMaterial;
    [SerializeField]
    private Transform shotPos;

    public void Init( PlayerModel model,int shellSpeed,int moveSpeed,Material shellMaterial)
    {
        this.playerModel = model;
        this.shellSpeed = shellSpeed;
        this.moveSpeed = moveSpeed;
        this.shellMaterial = shellMaterial;
        this.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        ShellView shellView = other.GetComponent<ShellView>();
        if (shellView == null)
        {
            return;
        }
        playerModel.Lose();
    }


    public void Shot()
    {
        //Debug.Log("Shot");
        playerModel.Shot(shellSpeed,shellMaterial,shotPos);
    }

    public void Move()
    {
        //Debug.Log("Move");
        playerModel.Move(moveSpeed,transform);
    }


}
