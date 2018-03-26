using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour {
	public BuildingBlock buildingBlockPreview;
	public Color goodPreviewColor = new Color (.5f, 1f, .5f, .5f);
	public Color badPreviewColor = new Color (1f, .5f, .5f, .5f);
	public int buildingBlockType;
	public Transform feet;
	public float cursorRadius = .1f;

	Vector3 targetPos;
	Vector3 cameraCenter = new Vector3(.5f, .5f, 0);


	public BuildingBlock CurrentPrefab {
		get {
			return BuildingGrid3D.instance.allBlockPrefabs [buildingBlockType];
		}
	}

	public bool IsShowingPreview {
		get {
			return buildingBlockPreview != null;
		}
	}
		
	public bool IsCellOccupied {
		get {
			var gridPos = GetCellGridPos ();
			return BuildingGrid3D.instance.GetCell (gridPos, buildingBlockType) != null;
		}
	}

	void Start () {
		targetPos = transform.localPosition;
	}

	void Update () {
		for (var i = 0; i < BuildingGrid3D.instance.allBlockPrefabs.Length; ++i) {
			if (Input.GetKeyDown ((KeyCode)((int)KeyCode.Alpha1 + i))) {
				SelectPreview (i);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			SelectPreview (buildingBlockType);
		}

		if (Input.GetMouseButton(0)) {
			TryBuild ();
		}

		CorrectPosition ();
		if (IsShowingPreview) {
			UpdatePreviewPosition ();
			UpdatePreviewStatusColor ();
		}
	}

	void CorrectPosition () {
		// TODO: Be smarter about determining optimal building position. E.g. when reaching through an empty cell, try to figure out if the reached-through cell is not already a good match

		var ray = Camera.main.ViewportPointToRay (cameraCenter);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && hit.distance < targetPos.z+cursorRadius) {
			// physical obstacles hinder our view
			transform.position = hit.point - ray.direction * cursorRadius;
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

	void SelectPreview(int i) {
		var shouldBuild = !IsShowingPreview || i != buildingBlockType;
		if (IsShowingPreview) {
			// 取消原來的 preview
			Destroy (buildingBlockPreview.gameObject);
		}

		if (shouldBuild) {
			// 顯示新的 preview
			buildingBlockType = i;
			buildingBlockPreview = BuildBlock (CurrentPrefab);
			buildingBlockPreview.GetComponentInChildren<Collider> ().enabled = false;		// don't collide with preview, ever
			UpdatePreviewStatusColor ();
		}
	}

	BuildingBlock BuildBlock(BuildingBlock prefab) {
		var go = (BuildingBlock)Instantiate (prefab);
		SetBlockPosition (go);
		return go;
	}

	void SetBlockPosition(BuildingBlock block) {
		var pos = GetCellWorldPos ();
		var rot = GetBestRotation ();
		block.transform.position = pos;
		block.transform.rotation = rot;	
	}

	void UpdatePreviewPosition() {
		SetBlockPosition (buildingBlockPreview);

		//print (cursor.transform.position + ", " + pos);
	}

	void UpdatePreviewStatusColor() {
		if (!IsCellOccupied) {
			SetPreviewColor(goodPreviewColor);
		}
		else {
			SetPreviewColor (badPreviewColor);
		}
	}

	void SetPreviewColor(Color col) {
		var mat = buildingBlockPreview.GetComponentInChildren<Renderer> ().material;
		mat.color = col;
	}



	public void TryBuild() {
		if (IsShowingPreview && !IsCellOccupied) {
			// make it official!
			var newBlock = BuildBlock(CurrentPrefab);
			BuildingGrid3D.instance.SetCell(GetCellGridPos(), buildingBlockType, newBlock);
		}
	}
}
