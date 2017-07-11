using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaArmIKController : MonoBehaviour
{

    public MechaWeaponInterfacePoint.E_ActiveSide ActiveSide = MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left;

    Transform WristTrans;
    Transform WristIdentity;
    Vector3 WeaponPoint;

    public Vector3 WristOffset = new Vector3(0.0f, 0.0f, 0.0f);

    public MechaWeaponInterfacePoint WeaponInterface;
    public GameObject MechaRoot;

    public GameObject Wrist;

    public Vector3 AssumedRestingPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public float WeaponOffsetScale = 2.0f;

    public float ArmMovementSpeed = 5.0f;

    void Start()
    {
        WristTrans = Wrist.transform;
        WristTrans.localPosition = WristOffset;
        WristIdentity = Wrist.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Target = new Vector3();
        Quaternion TargetR = new Quaternion();

        if (WeaponInterface)
        {
            //WristTrans.position = WeaponInterface.UsingController.transform.parent.position + WristOffset;
            Vector3 GunOff = WeaponInterface.gameObject.transform.position - MechaRoot.transform.position;
            Target = WristOffset + (GunOff) * WeaponOffsetScale;
            // Find a default position for the "standard" controller position and calculate difference from that standard hand position as the line above.
            TargetR = WeaponInterface.transform.rotation;
        }
        else
        {
            // Set goals to resting position
            Target = WristIdentity.position;
            TargetR = WristIdentity.rotation;

            string offsetTag;
            GameObject tmpWp;
            if (ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left)
                offsetTag = "WeaponInterfaceL";
            else
                offsetTag = "WeaponInterfaceR";

            tmpWp = GameObject.FindGameObjectWithTag(offsetTag);

            if (tmpWp)
                WeaponInterface = tmpWp.GetComponent<MechaWeaponInterfacePoint>();
        }
        WristTrans.localPosition = Vector3.Lerp(WristTrans.localPosition, Target, ArmMovementSpeed * Time.deltaTime); /* + local hand position offset + whatever scaling factor. */
        WristTrans.rotation = Quaternion.Lerp(WristTrans.rotation, TargetR, ArmMovementSpeed * Time.deltaTime);
    }
}