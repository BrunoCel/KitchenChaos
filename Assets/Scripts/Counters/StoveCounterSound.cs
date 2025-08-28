using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stove;
    private AudioSource audioSource;
    private float warningSoundTimer = 0f;
    private bool playWarningSound = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stove.OnStateChanged += StoveCOunterStateChanged;
        stove.OnProgressChanged += FoodIsBurnig;
    }

    private void FoodIsBurnig(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float showBurnProgressAmount = .5f;
        playWarningSound = stove.IsFried() && e.progressNormalized >= showBurnProgressAmount;
    }

    private void StoveCOunterStateChanged(object sender, StoveCounter.OnStateChangedArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.instance.PlayWarningSound(stove.transform.position);
            }
        }
    }
}