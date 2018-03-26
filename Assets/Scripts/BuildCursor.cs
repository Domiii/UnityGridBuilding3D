using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour {
	public Transform feet;
	Vector3 targetPos;
	Vector3 cameraCenter = new Vector3(.5f, .5f, 0);

	void Start () {
		targetPos = transform.localPosition;
	}

	void Update () {
		CorrectPosition ();
	}

	void CorrectPosition () {
		var ray = Camera.main.ViewportPointToRay (cameraCenter);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && hit.distance < targetPos.z) {
			// physical obstacles hinder our view
			transform.position = hit.point;
		} else {
			transform.localPosition = targetPos;
		}
		//transform.localPosition = ;
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
