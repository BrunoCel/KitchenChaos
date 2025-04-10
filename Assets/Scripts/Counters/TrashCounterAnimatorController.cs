using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterAnimatorController : MonoBehaviour
{
    private const string DISCARD = "Discard";
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        TrashCounter.OnDiscard += TrashCounter_OnPlayerDiscardObject;
    }

    private void TrashCounter_OnPlayerDiscardObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(DISCARD);
    }
}
