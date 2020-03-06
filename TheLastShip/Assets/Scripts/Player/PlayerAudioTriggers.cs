using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioTriggers : MonoBehaviour
{
    // Cole: Put methods for the player ship's sounds here

    public void PlaySoundDamageShield()
    {
        AkSoundEngine.PostEvent("plr_shield_damage", this.gameObject);
    }

    public void PlaySoundHitmarker()
    {
        AkSoundEngine.PostEvent("hitmarker", this.gameObject);
    }
}
