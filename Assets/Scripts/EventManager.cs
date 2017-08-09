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
public class EnemyDeath : UnityEvent
{

}

//Called when the player disconnects from a gun
public class OnDisconnect : UnityEvent<GameObject>
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

    public StartRound OnRoundStart = new StartRound();
    public EndRound OnRoundEnd = new EndRound();
    public EnemyDeath OnEnemyDeath = new EnemyDeath();
    public OnDisconnect OnControllerDisconnect = new OnDisconnect();

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
