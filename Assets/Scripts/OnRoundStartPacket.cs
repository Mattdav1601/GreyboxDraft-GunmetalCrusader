using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoundStartPacket {
    //The on roundd start packet will hold information relevant to classes
    //at the beginning of the round (These might be set in the WaveManager)
    public float enemyMaxHealth;
    public float enemySpeed;
    public float enemyDamage;
}
