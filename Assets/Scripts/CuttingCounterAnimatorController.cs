using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimatorController : MonoBehaviour
{
    
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        cuttingCounter.IsCutting += CuttingCounter_OnCutObject;
    }

    private void CuttingCounter_OnCutObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
   
}

