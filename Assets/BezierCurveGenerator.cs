using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveGenerator : MonoBehaviour {

    public int Resolution = 10;
    public List<Vector3> Points = new List<Vector3>();
    public Vector3 StartPoint;
    public Vector3 StartPointControl;
    public Vector3 EndPoint;
    public Vector3 EndPointControl;

    public LineRenderer rendoroonie;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float t = 0;
        for (int i = 0; i < Resolution; i++)
        {
            t = 1.0f / Resolution;
            Points[i] = Mathf.Pow(1.0f - t, 3.0f) * StartPoint + 3 * Mathf.Pow(1.0f - t, 2.0f) * t * StartPointControl + 3 * (1.0f - t) * Mathf.Pow(t, 2.0f) * EndPoint + Mathf.Pow(t, 3.0f) * EndPointControl;
        }

        Vector3[] pTransfer = Points.ToArray();

        rendoroonie.SetPositions(pTransfer);
    }
}
