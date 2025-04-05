using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioRefsSO : ScriptableObject
{
    public AudioClip[] chops;
    public AudioClip[] deliveryFails;
    public AudioClip[] deliverySuccesses;
    public AudioClip[] footSteps;
    public AudioClip[] objectDrops;
    public AudioClip[] objectPickups;
    public AudioClip stoveSizzle;
    public AudioClip[] trashCan;
    public AudioClip[] warnings;
}
