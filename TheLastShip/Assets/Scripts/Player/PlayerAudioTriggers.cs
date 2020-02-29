using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioTriggers : MonoBehaviour
{
    // Cole: Put methods for the player ship's sounds here

    // audio ckrueger vvv
    public void PlaySoundDamageShield()
    {
        AkSoundEngine.PostEvent("plr_shield_damage", gameObject);
    }
    // audio ckrueger ^^^
}
