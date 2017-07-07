using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaArmIKController : MonoBehaviour {

    public MechaWeaponInterfacePoint.E_ActiveSide ActiveSide = MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left;

    Vector3 ShoulderLoc;
	Transform WristTrans;
	Vector3 ElbowLoc = new Vector3 ( 0.0f, 0.0f, 0.0f);
	Vector3 WeaponPoint;

	public Vector3 WristOffset = new Vector3 ( 0.0f, 0.0f, 0.0f);
	public Vector3 WeaponPointOffset = new Vector3 ( 0.0f, 0.0f, 0.0f); 

	public float ElbowConstraintDistance = 0.5f;

	public GameObject WeaponInterface;
	public float multiAmount = 10.0f;

	public GameObject Joint01;
	public GameObject Joint02;
	public GameObject Joint03;

	void Start(){
		ShoulderLoc = this.transform.position;
		WristTrans = Joint03.transform;
		WristTrans.position += WristOffset;
		WeaponPoint = WristTrans.position + WeaponPointOffset;

		ElbowLoc = ShoulderLoc + (WristTrans.position - ShoulderLoc) / 2;
	}


	// Update is called once per frame
	void Update () {
        if (WeaponInterface != null)
        {
            ShoulderLoc = this.transform.position;
            WristTrans.position = WeaponInterface.transform.position + WristOffset;
            WristTrans.rotation = WeaponInterface.transform.rotation;
            WeaponPoint = WristTrans.position + WeaponPointOffset;

            ElbowLoc = FixedDistanceConstraint(ElbowLoc, WristTrans.position, ElbowConstraintDistance, 0.4f);
            ElbowLoc = FixedDistanceConstraint(ElbowLoc, ShoulderLoc, ElbowConstraintDistance, 0.4f);

            DEBUGSetPositions();
        }
        else
        {
            string offsetTag;
            if (ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left)
                offsetTag = "WeaponInterfaceL";
            else
                offsetTag = "WeaponInterfaceR";

            WeaponInterface = GameObject.FindGameObjectWithTag(offsetTag);
        }
	}

	Vector3 FixedDistanceConstraint(Vector3 v1, Vector3 v2, float distance, float compensate)
	{
		Vector3 Output = v1;
		Vector3 delta = v1 - v2;
		float deltaLength = (float)Mathf.Sqrt (delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);

		Debug.Log (deltaLength);

		if (deltaLength != 0) {
			float diff = (deltaLength - distance) / deltaLength;
			if (compensate != 0)
				Output -= delta * compensate * diff;
		}
		return Output;
	}

	void DEBUGSetPositions()
	{
		Joint01.transform.position = ShoulderLoc;
		Joint02.transform.position = ElbowLoc;
		Joint03.transform.position = WristTrans.position;
		Joint03.transform.rotation = WristTrans.rotation;
	}
}