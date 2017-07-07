using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaArmIKController : MonoBehaviour {

    public MechaWeaponInterfacePoint.E_ActiveSide ActiveSide = MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left;

	Transform WristTrans;
	Vector3 WeaponPoint;

	public Vector3 WristOffset = new Vector3 ( 0.0f, 0.0f, 0.0f);

	public MechaWeaponInterfacePoint WeaponInterface;
    public GameObject MechaRoot;
    
	public GameObject Wrist;

    public Vector3 AssumedRestingPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public float WeaponOffsetScale = 2.0f;

    void Start(){
		WristTrans = Wrist.transform;
		WristTrans.localPosition = WristOffset;
	}

	// Update is called once per frame
	void Update () {
        if (WeaponInterface != null)
        {
            //WristTrans.position = WeaponInterface.UsingController.transform.parent.position + WristOffset;
            Vector3 GunOff = WeaponInterface.gameObject.transform.position - MechaRoot.transform.position;
            WristTrans.localPosition = WristOffset + (GunOff) * WeaponOffsetScale; /* + local hand position offset + whatever scaling factor. */
            // Find a default position for the "standard" controller position and calculate difference from that standard hand position as the line above.
            WristTrans.rotation = WeaponInterface.transform.rotation;

            DEBUGSetPositions();
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

	Vector3 FixedDistanceConstraint(Vector3 v1, Vector3 v2, float distance, float compensate)
	{
		Vector3 Output = v1;
		Vector3 delta = v1 - v2;
        float deltaLength = (float)Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);

		if (deltaLength != 0) {
			float diff = (deltaLength - distance) / deltaLength;
			if (compensate != 0)
				Output -= delta * compensate * diff;
		}
		return Output;
	}

	void DEBUGSetPositions()
	{
        //Wrist.transform.localPosition = WristTrans.position;
        //Wrist.transform.rotation = WristTrans.rotation;
	}
}