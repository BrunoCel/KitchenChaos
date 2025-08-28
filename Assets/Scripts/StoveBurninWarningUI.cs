using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurninWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    
    void Start()
    {
        StoveCounter.OnProgressChanged += FoodIsBurning;
        ShowAndHide(false);
    }

    private void FoodIsBurning(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float showBurnProgressAmount = .5f;
        bool show = StoveCounter.IsFried() && e.progressNormalized >= showBurnProgressAmount;

        ShowAndHide(show);

    }

    void ShowAndHide(bool showAndHide)
    {
        gameObject.SetActive(showAndHide);
    }
}
