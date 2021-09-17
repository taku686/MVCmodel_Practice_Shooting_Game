using UnityEngine;

public class PlayerModel : CharacterModel
{
   

    private Vector3 adjustedValue = Vector3.one; 
    private Vector3 bottomLeft; 
    private Vector3 topRight;
    private float playerCameraDistance;
  


    public void Init()
    {
        this.shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
        this.playerCameraDistance = transform.position.y - Camera.main.transform.position.y;
        this.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(1,1,playerCameraDistance))+adjustedValue;
        this.topRight = Camera.main.ViewportToWorldPoint(new Vector3(0, 0,playerCameraDistance))-adjustedValue;      
    }

    public void Move(int moveSpeed,Transform transform)
    {
        float xVelocity = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zVelocity = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //Debug.Log("xVelocity" + xVelocity);

        transform.position += new Vector3(xVelocity, 0, zVelocity);

        if(transform.position.x > topRight.x|| transform.position.x < bottomLeft.x|| transform.position.z > topRight.z|| transform.position.z < bottomLeft.z)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x), 0, Mathf.Clamp(transform.position.z, bottomLeft.z, topRight.z));
        }
    }

    public void Shot(int shellSpeed,Material shellMaterial,Transform shotPos)
    {
        ShellView shell = shellController.GetShell(shellMaterial);
        //Debug.Log("Shell");
        //Debug.Log(shell == null);
        shell.shellRigidbody.velocity = new Vector3(0, 0, shellSpeed);
        shell.transform.position = shotPos.position;
    }

    public GameObject CereatePlayer(GameObject playerObj,Transform createPos)
    {
        GameObject player = Instantiate(playerObj, createPos.position, Quaternion.identity);
        return player;
    }

    // 敗北処理
    public void Lose()
    {
        Debug.Log("Player死亡");
        Destroy(this.gameObject);
    }

    public bool OnClickButton()
    {
        if(Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
