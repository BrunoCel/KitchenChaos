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
    [SerializeField] private GameObject hasProgressBar;
    [SerializeField] private IHasProgress counter;
    void Start()
    {
        counter = hasProgressBar.GetComponent<IHasProgress>();
        if (cuttingCounter != null)
        {
            cuttingCounter.PlayerGrabbedKitchenObject += StopCuttigProgress;
        }

        counter.OnProgressChanged += CuttingProgressOnProgressChanged;
        progressBar.fillAmount = 0;
        Hide();
    }

    private void StopCuttigProgress(object sender, EventArgs e)
    {
        progressBar.fillAmount = 0;
        Hide();
    }

    private void CuttingProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
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
