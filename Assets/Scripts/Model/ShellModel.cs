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

    public ShellView CreateShell(List<ShellView> viewList)
    {
        ShellView shellView = Instantiate(shellPrefab,shellPrabParent).GetComponent<ShellView>();
        shellView.Init(shellController);
        viewList.Add(shellView);

        return shellView;
    }

    public ShellView GetShell(List<ShellView> viewList)
    {
        if(viewList.Count == 0)
        {
            return CreateShell(viewList);
        }

        foreach (var shellView in viewList.Where((sv) => sv.IsActive == false))
        {
            if (shellView == null)
            {
                //Debug.Log("Shell生成");
                return CreateShell(viewList);         
            }
            else
            {
                //Debug.Log("Shell返す");
                viewList.Remove(shellView);
                return shellView;
            } 
        }

        return null;//これでいいのか？？
    }

}
