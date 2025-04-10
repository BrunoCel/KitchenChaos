using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = 0.5f;
    float volume = 1f;

    void Awake()
    {
        player = GetComponent<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;
            if (player.IsWalking())
            {
                SoundManager.instance.PlayFootstepSound(this.transform.position, volume);
            }
        }
    }
}
