using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{ 
    [SerializeField] private GameObject[] bars;
    [SerializeField] private Image progressBar;
    [SerializeField] private CuttingCounter cuttingCounter;
    void Start()
    {
        cuttingCounter.PlayerGrabbedKitchenObject += StopCuttigProgress;
        cuttingCounter.OnCut += CuttingProgress_OnCut;
        progressBar.fillAmount = 0;
        Hide();
    }

    private void StopCuttigProgress(object sender, EventArgs e)
    {
        progressBar.fillAmount = 0;
        Hide();
    }

    private void CuttingProgress_OnCut(object sender, CuttingCounter.OnProgressChangedArgs e)
    {
        progressBar.fillAmount = e.progressNormalized;

        if (progressBar.fillAmount >= 1 || progressBar.fillAmount == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].SetActive(true);
        }
    }

    private void Hide()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].SetActive(false);
        }
    }
}
