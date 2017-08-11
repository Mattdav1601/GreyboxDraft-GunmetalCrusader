using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Shouted out when the round begins
public class StartRound : UnityEvent<OnRoundStartPacket>
{
}

//Shouted out when the round ends
public class EndRound : UnityEvent{
}

//Called every time an enemy dies
public class EnemyDeath : UnityEvent{
}


public class ControllerConnect : UnityEvent<GameObject, GameObject>
{

}

//Called when the player disconnects from a gun
public class OnDisconnect : UnityEvent<GameObject>{
}

public class RoundStartWarning : UnityEvent<int>{
}

public class RoundEndWarning : UnityEvent<bool>
{

}

//Called when the player presses the trigger
public class WeaponFire : UnityEvent<OnWeaponFirePacket>
{

}

//Called when the player presses the trigger
public class WeaponReload : UnityEvent<int>
{

}

//Called when the player is out of ammo
public class OutOfAmmo : UnityEvent<int>
{

}

//Called when the player is out of ammo
public class OutOfClips : UnityEvent<int>
{

}

//called when the player attempts to reload
public class ReloadAttempt : UnityEvent<bool>
{
}

public class EventManager : MonoBehaviour
{
    private static EventManager inst;
    public static EventManager instance {
        get
        {
            if(inst == null)
            {
                var newEventManager = new GameObject("EventManager");
                inst = newEventManager.AddComponent<EventManager>();
            }

            return inst;
        }
    }

    //Events
    public StartRound OnRoundStart = new StartRound();
    public EndRound OnRoundEnd = new EndRound();
    public EnemyDeath OnEnemyDeath = new EnemyDeath();
    public ControllerConnect OnControllerConnect = new ControllerConnect();
    public OnDisconnect OnControllerDisconnect = new OnDisconnect();
	public RoundStartWarning OnRoundStartWarning = new RoundStartWarning();
    public RoundEndWarning OnRoundEndWarning = new RoundEndWarning();
	public WeaponFire OnWeaponFire = new WeaponFire();
	public WeaponFire OnWeaponReload = new WeaponReload();
	public OutOfAmmo OnOutOfAmmo = new OutOfAmmo();
	public OutOfClips OnOutOfClips = new OutOfClips();
    public ReloadAttempt OnReloadAttempt = new ReloadAttempt();

    private void Awake()
    {
        if (inst != null)
        {
            DestroyImmediate(this);
            return;
        }

        inst = this;
    }
}