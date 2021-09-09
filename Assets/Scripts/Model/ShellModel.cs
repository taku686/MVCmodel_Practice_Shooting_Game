using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShellModel : MonoBehaviour
{

    ShellController shellController;
//    ShellView shellView;
    [SerializeField]
    private GameObject shellPrefab;
    [SerializeField]
    private Transform shellPrabParent;

    public void Init(ShellController shellController)
    {
        this.shellController = shellController;
    }

    private void Update()
    {
        
    }

    public ShellView ShellCreate()
    {
        ShellView shell = Instantiate(shellPrefab,shellPrabParent).GetComponent<ShellView>();
        shell.Init(shellController);

        return shell;
    }

    public ShellView GetShell(List<ShellView> viewList)
    {
        if(viewList.Count == 0)
        {
            return shellController.CreateShell();
        }

        foreach (var shellView in viewList.Where((sv) => sv.IsActive == false))
        {
            if (shellView == null)
            {
                //Debug.Log("Shell生成");
                return shellController.CreateShell();         
            }
            else
            {
                //Debug.Log("Shell返す");
                return shellView;
            } 
        }

        return null;//これでいいのか？？
    }

}
