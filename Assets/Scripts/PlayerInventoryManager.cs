using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour {

    public static PlayerInventoryManager inventory;

    // Holds reference to the player's weapons so that it can be accessed later
    private GameObject[] weaponArray = new GameObject[2];

    // The game objects on the mech that the weapons are parented to.
    [Tooltip("The game objects on the mech that the weapons are parented to.")]
    [SerializeField]
    private GameObject[] weaponPoints = new GameObject[2];

    // The initial weapon loadout that the player starts with.
    [Tooltip("The initial weapon loadout that the player starts with.")]
    [SerializeField]
    private GameObject[] initialLoadout = new GameObject[2];

    // Use this for initialization
    void Start () {
        inventory = this;
        EquipWeapon(0, initialLoadout[0]);
        EquipWeapon(1, initialLoadout[1]);
    }

    // Replaces the current weapon on the indicated hand with a new one.
    public void EquipWeapon(int i, GameObject weapon)
    {
        if (weapon.GetComponent<Weapon>() != null && i <= 2 && i > -1)
        {
            if (weaponArray[i] != null)
                Destroy(weaponArray[i]);

            weaponArray[i] = Instantiate(weapon, weaponPoints[i].transform);
            weaponArray[i].GetComponent<Weapon>().sideIndex = i;
        }
        else
        {
            Debug.Log("Error! Equipped Game Object is not a weapon or Invalid slot chosen.");
        }
    }
}
