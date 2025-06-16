using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager instance;
    [SerializeField] AudioRefsSO audioRefsSO;

    private float volume = 1f;
    void Awake()
    {
        instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
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
        AudioSource.PlayClipAtPoint(audioClip, position,volume * this.volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume * this.volume);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioRefsSO.footSteps , position , volumeMultiplier * volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
