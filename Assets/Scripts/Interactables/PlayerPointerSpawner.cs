using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerPointerSpawner : VRTK_InteractableObject{

    [Tooltip("Determines which mech-arms the spawned guns will relate to.")]
    [SerializeField]
    private int sideIndex = 0;

    [Tooltip("The ghost gun model that the player will see on their controllers")]
    [SerializeField]
    private GameObject pointerObject;

    // Have we currently produced a pointer?
    private bool AlreadySpawned = false;


    void Start()
    {
        EventManager.instance.OnControllerConnect.AddListener((p, o) => {
            if (p == this.gameObject)
            {
                OnSwipeUp(o);
            }
        });
    }

    private void attemptToReactivate(int side)
    {
        if (side == sideIndex)
            AlreadySpawned = false;
    }

    //Performed when swiping up
    public void OnSwipeUp(GameObject usingObject)
    {
        GameObject pointer = Instantiate(pointerObject, usingObject.transform);
        pointer.GetComponent<PlayerPointer>().initializePointer(usingObject.GetComponent<VRTK_ControllerEvents>(), sideIndex);
        Debug.Log("Spawned Pointer");
    }
}
