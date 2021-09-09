using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController :BaseController
{
    private const float AddViewsWaitTime = 0;

    private ShellModel shellModel;
    [SerializeField]
    private int maxShellCount;

    public List<ShellView> viewList = new List<ShellView>();   
    private int shellCount;
    


    public override void Init()
    {
        base.Init();
        shellModel = GetComponent<ShellModel>();
        shellModel.Init(this);
        for(int i = 0; i < maxShellCount; i++)
        {
            var taskAddViews =new TaskManager.Task(AddViewsWaitTime, CreateShell, TaskManager.Task.Type.Time);
            //Debug.Log("AddViews");
            this.TaskManager.Add(taskAddViews);
        }   

    }

    public override void GameUpdate()
    {
        base.GameUpdate();
    }

    public ShellView GetShell()
    {
        ShellView shellView = shellModel.GetShell(viewList);
        viewList.Remove(shellView);
        return shellView;
    }

    public ShellView CreateShell()
    {
        ShellView shellView = shellModel.ShellCreate();
        viewList.Add(shellView);
        return shellView;
    }


}
