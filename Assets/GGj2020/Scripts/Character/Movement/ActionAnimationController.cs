using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimationController : Singleton<ActionAnimationController>
{
    private Animator animator;
    public Animator Animator { get { return (animator == null) ? animator = GetComponent<Animator>() : animator; } }

    public void TriggerAction(string action)
    {
        if(!string.IsNullOrEmpty(action))
            Animator.SetTrigger(action);
    }

    public void SetAction(string action, bool value)
    {
        if (!string.IsNullOrEmpty(action))
            Animator.SetBool(action, value);
    }
}
