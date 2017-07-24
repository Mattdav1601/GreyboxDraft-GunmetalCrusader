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

    public GameObject LaserPointer;
    public LineRenderer lineRend;

    void Start()
    {
        WristTrans = Wrist.transform;
        WristTrans.localPosition = WristOffset;
        WristIdentity = Wrist.transform;
    }

    // Update is called once per frame
    void Update()
    {
        SetLines();

        if (WeaponInterface != null)
        {
           // Debug.Log("Should be moving");
            WristTrans.LookAt(WeaponInterface.TargetPos);
            LaserPointer.transform.LookAt(WeaponInterface.TargetPos);
            WristTrans.localPosition = Vector3.Lerp(WristTrans.localPosition, WristOffset + (WeaponInterface.gameObject.transform.localPosition - MechaRoot.transform.localPosition) * WeaponOffsetScale, ArmMovementSpeed * Time.deltaTime);
            //WristTrans.localPosition = Vector3.Lerp(WristTrans.localPosition, WristOffset + Vector3.Scale(WeaponInterface.gameObject.transform.localPosition, MechaRoot.transform.localRotation.eulerAngles ) * WeaponOffsetScale, ArmMovementSpeed * Time.deltaTime);
            //WristTrans.localPosition = Vector3.Scale(WristTrans.localPosition, MechaRoot.transform.forward);
            // transform.localRotation = Quaternion.Euler(0, 0,WeaponInterface.gameObject.transform.rotation.z);


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

    void SetLines()
    {
        if (WeaponInterface)
        {
            lineRend.enabled = true;

            Vector3[] LineRendPts = new Vector3[2];

            LineRendPts[0] = LaserPointer.transform.position;
            LineRendPts[1] = WeaponInterface.TargetPos;

            lineRend.SetPositions(LineRendPts);
        }
        else
        {
            lineRend.enabled = false;
        }
    }
}