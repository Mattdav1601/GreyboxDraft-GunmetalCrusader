using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ReloadingHandle : VRTK_InteractableObject
{
    private Vector3 originalPosition;
    public float maxOffset;
    public float StopPoint;
    public GameObject reloadingobjectright;
    public GameObject reloadingobjectleft;
    public GameObject CurrentController;


    private MechaWeaponInterfacePoint[] WeaponInterfaces;

    // Use this for initialization
    void Start () {
        originalPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        Movelever();

    }
    public override void StopUsing(GameObject previousUsingObject)
    {
        CurrentController = null;
        transform.localPosition = originalPosition;
    }
    public override void StartUsing(GameObject usingObject)
    {
        CurrentController = usingObject;
    }
    void Movelever()
    {
        if (CurrentController != null && CheckControllerEmpty())
        {
            //updates position of the physical gameobject in relation ot the controller.
            float newZ = CurrentController.transform.parent.localPosition.z;

            if (newZ > originalPosition.z + maxOffset)
            {
                newZ = transform.localPosition.z + maxOffset;
            }
               
            Vector3 newPos = this.transform.localPosition;

            newPos.z = newZ;

            transform.localPosition = newPos;
            if (transform.localPosition.z <= StopPoint)
            {
                StopUsing(CurrentController);
                Reload();
            }

        }
    }
    bool CheckControllerEmpty()
    {
        WeaponInterfaces = FindObjectsOfType<MechaWeaponInterfacePoint>();
        foreach (MechaWeaponInterfacePoint meme in WeaponInterfaces)
        {
            if (meme.UsingController == CurrentController)
            {
                return false;
            }
        }
        return true;
    }

    void Reload()
    {
        Debug.Log("reloading handle activated");
        if (this.gameObject.name == "RightReloadingHandle")
        {
            reloadingobjectright.GetComponent<MechaWeaponBehaviour>().ToldToReload = true;
        }

        else
        {
            reloadingobjectleft.GetComponent<MechaWeaponBehaviour>().ToldToReload = true;
        }
    }
}
