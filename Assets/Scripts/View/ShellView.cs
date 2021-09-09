using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellView : MonoBehaviour
{
    private const float InactiveWaitTime = 0f;

    private bool isActive = false;
    public ShellController shellController;
    public Rigidbody shellRigidbody;

    public bool IsActive { get => isActive; }

    public void Init(ShellController controller)
    {
        this.shellController = controller;
        shellRigidbody = GetComponent<Rigidbody>();
        Inactive();
    }
    private void OnBecameInvisible()
    {
        var taskInactive = new TaskManager.Task(InactiveWaitTime, Inactive, TaskManager.Task.Type.Time);
        shellController.viewList.Add(this);
        shellController.TaskManager.Add(taskInactive);
    }

    private void Inactive()
    {
        shellRigidbody.Sleep();
        isActive = !shellRigidbody.IsSleeping();
    }
}
