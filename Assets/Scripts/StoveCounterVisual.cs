using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject[] stoveCounters;

    private void Start()
    {
        stoveCounter.PutIn += TurnOn;
        stoveCounter.PutOff += TurnOff;
    }

    public void TurnOn(object sender, System.EventArgs e)
    {
        for (int i = 0; i < stoveCounters.Length; i++)
        {
            stoveCounters[i].SetActive(true);
        }   
    }

    public void TurnOff(object sender, System.EventArgs e)
    {
        for (int i = 0; i < stoveCounters.Length; i++)
        {
            stoveCounters[i].SetActive(false);
        }
    }
}
