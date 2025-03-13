using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject[] stoveCounters;

    private void Start()
    {
        stoveCounter.OnStateChanged += OnStateChanged_OnStoveCounter;
        
    }

    public void OnStateChanged_OnStoveCounter(object sender, StoveCounter.OnStateChangedArgs args)
    {
        bool showVisual = args.state == StoveCounter.State.Frying || args.state == StoveCounter.State.Fried;
        for (int i = 0; i < stoveCounters.Length; i++)
        {
            stoveCounters[i].SetActive(showVisual);
        }   
    }

    
}
