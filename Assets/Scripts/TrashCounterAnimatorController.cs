using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterAnimatorController : MonoBehaviour
{
    private const string DISCARD = "Discard";
    [SerializeField] private TrashCounter trashCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        trashCounter.OnDiscard += TrashCounter_OnPlayerDiscardObject;
    }

    private void TrashCounter_OnPlayerDiscardObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(DISCARD);
    }
}
