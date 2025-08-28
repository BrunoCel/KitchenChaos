using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashBarUI : MonoBehaviour
{
    private const string isFlashingBollean = "isFlashing";
    [SerializeField] StoveCounter StoveCounter;
     private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StoveCounter.OnProgressChanged += FlashProgressBar;
    }

    private void FlashProgressBar(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float showBurnProgressAmount = .5f;
        bool PlayFlashAnimation = StoveCounter.IsFried() && e.progressNormalized >= showBurnProgressAmount;

        animator.SetBool(isFlashingBollean, PlayFlashAnimation);
        
    }
}
