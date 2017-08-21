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
public class OnDisconnect : UnityEvent<GameObject>
{
}

public class RoundStartWarning : UnityEvent<bool>{
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

//Called when the player presses the trigger
public class ReloadComplete : UnityEvent
{
}

//Called when the player is out of ammo
public class OutOfAmmunition : UnityEvent<int, bool>
{
}

//called when the player attempts to reload
public class AttemptFireWhileDepleted : UnityEvent
{
}

//called when the player attempts to reload
public class ReloadAttempt : UnityEvent<bool>
{
}

//called every few frames to update the weapon's aim (Or to tell weapons they've been deactivated)
public class UpdateAim : UnityEvent<OnUpdateAimPacket>
{
}

//Called when the weapon's ammo changes
public class UpdateAmmoDisplay : UnityEvent<OnUpdateAmmoDisplayPacket>
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
	public WeaponReload OnWeaponReload = new WeaponReload();
    public ReloadComplete OnReloadComplete = new ReloadComplete();
    public OutOfAmmunition OnOutOfAmmunition = new OutOfAmmunition();
	public AttemptFireWhileDepleted OnAttemptFireWhileDepleted = new AttemptFireWhileDepleted();
    public ReloadAttempt OnReloadAttempt = new ReloadAttempt();
    public UpdateAim OnUpdateAim = new UpdateAim();
    public UpdateAmmoDisplay OnUpdateAmmoDisplay = new UpdateAmmoDisplay();

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