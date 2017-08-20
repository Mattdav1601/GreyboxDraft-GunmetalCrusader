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
        if (WeaponInterface != null)
        {
            WristTrans.LookAt(WeaponInterface.TargetPos);
            WristTrans.localPosition = Vector3.Lerp(WristTrans.localPosition, WristOffset + (WeaponInterface.gameObject.transform.localPosition - MechaRoot.transform.localPosition) * WeaponOffsetScale, ArmMovementSpeed * Time.deltaTime);

        }
        else
        {
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
    }
}