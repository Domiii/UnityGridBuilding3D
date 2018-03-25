using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour {
	public Transform feet;

	void Start () {
		
	}

	void Update () {
		
	}

//	public BuildingGridCell GetCell() {
//		
//	}

	public Int3 GetCellGridPos() {
		return BuildingGrid3D.instance.WorldToGrid (transform.position);
	}

	public Vector3 GetCellWorldPos() {
		return BuildingGrid3D.instance.SnapWorld (transform.position);
	}

	/// <summary>
	/// Determine rotation based on where the cursor is relative to the feet
	/// </summary>
	public Quaternion GetBestRotation() {
		// NOTE: feet + cursor could be in the same cell
		// TODO!
		return Quaternion.identity;
	}
}
