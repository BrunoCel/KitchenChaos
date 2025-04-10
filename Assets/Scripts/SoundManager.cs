using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioRefsSO audioRefsSO;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DeliveyManager.instance.OnRecipeSuccess+= DeliveryManager_OnOnRecipeSuccess;
        DeliveyManager.instance.OnRecipeFailed+= DeliveryManager_OnOnRecipeFailed;
        CuttingCounter.OnAnyCut+= CuttingCounterOnOnAnyCut;
        Player.Instance.OnPickedSomething += PlayerPickedSomething;
        BaseCounter.OnPlayerDropSomething += PlayerDropedSomething;
        TrashCounter.OnDiscard += IngridientDiscarted;
    }

    private void IngridientDiscarted(object sender, EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(audioRefsSO.trashCan,counter.transform.position);
    }

    private void PlayerDropedSomething(object sender, EventArgs e)
    {
        
        PlaySound(audioRefsSO.objectDrops,Player.Instance.transform.position);
    }

    private void PlayerPickedSomething(object sender, EventArgs e)
    {
        Player player = sender as Player;
        PlaySound(audioRefsSO.objectPickups,player.transform.position);
    }

    private void CuttingCounterOnOnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter counter = sender as CuttingCounter;
        PlaySound(audioRefsSO.chops,counter.transform.position);
    }

    private void DeliveryManager_OnOnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter counter = DeliveryCounter.instance;
        PlaySound(audioRefsSO.deliveryFails,counter.transform.position);
    }

    private void DeliveryManager_OnOnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter counter = DeliveryCounter.instance;
        PlaySound(audioRefsSO.deliverySuccesses,counter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position,volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioRefsSO.footSteps,position,volume);
    }
}
