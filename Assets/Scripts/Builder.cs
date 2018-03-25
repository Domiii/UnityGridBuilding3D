using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {
	public BuildCursor cursor;
	public BuildingBlock buildingBlockPreview;
	public Color goodPreviewColor = new Color (.5f, 1f, .5f, .5f);
	public Color badPreviewColor = new Color (1f, .5f, .5f, .5f);
	public int buildingBlockIdx;
	public BuildingBlock[] allBlockPrefabs;

	public BuildingBlock CurrentPrefab {
		get {
			return allBlockPrefabs [buildingBlockIdx];
		}
	}

	void Update () {
		for (var i = 0; i < allBlockPrefabs.Length; ++i) {
			if (Input.GetKeyDown ((KeyCode)((int)KeyCode.Alpha1 + i))) {
				TogglePreview (i);
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			TogglePreview (buildingBlockIdx);
		}
		//if (Input.GetKey (KeyCode.LeftShift)) {
		if (Input.GetMouseButton(0)) {
			TryBuild ();
		}

		UpdatePreviewPosition ();
	}

	bool IsCellOccupied() {
		var gridPos = cursor.GetCellGridPos();
		return BuildingGrid3D.instance.GetCell(gridPos) != null;
	}

	void TogglePreview(int i) {
		var prefab = allBlockPrefabs [i];
		var shouldBuild = !buildingBlockPreview || i != buildingBlockIdx;
		if (buildingBlockPreview) {
			// 取消原來的 preview
			Destroy (buildingBlockPreview.gameObject);
		}

		if (shouldBuild) {
			// 顯示新的 block
			buildingBlockIdx = i;
			buildingBlockPreview = BuildBlock (CurrentPrefab);
			UpdatePreviewStatusColor ();
		}
	}

	BuildingBlock BuildBlock(BuildingBlock prefab) {
		var go = (BuildingBlock)Instantiate (prefab);
		SetBlockPosition (go);
		return go;
	}

	void SetBlockPosition(BuildingBlock block) {
		var pos = cursor.GetCellWorldPos ();
		var rot = cursor.GetBestRotation ();
		block.transform.position = pos;
		block.transform.rotation = rot;	
	}

	void UpdatePreviewPosition() {
		if (buildingBlockPreview) {
			SetBlockPosition (buildingBlockPreview);

			//print (cursor.transform.position + ", " + pos);
		}
	}

	void UpdatePreviewStatusColor() {
		if (!IsCellOccupied()) {
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
		if (buildingBlockPreview && !IsCellOccupied()) {
			// make it official!
			var newBlock = BuildBlock(CurrentPrefab);
			BuildingGrid3D.instance.SetCell(cursor.GetCellGridPos(), newBlock);
		}
	}
}
