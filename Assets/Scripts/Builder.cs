using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {
	public BuildCursor cursor;
	public BuildingBlock buildingBlockPreview;
	public int buildingBlockIdx;
	public BuildingBlock[] allBlockPrefabs;

	void Update () {
		for (var i = 0; i < allBlockPrefabs.Length; ++i) {
			if (Input.GetKeyDown ((KeyCode)((int)KeyCode.Alpha1 + i))) {
				TogglePreview (i);
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			TogglePreview (buildingBlockIdx);
		}
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
			var pos = cursor.GetCellWorldPos();
			var rot = cursor.GetBestRotation();
			buildingBlockPreview = Instantiate (allBlockPrefabs[i], pos, rot);
		}
	}



	public void BuildBlock(BuildingBlock prefab) {
		var go = Instantiate (prefab);
		// TODO!
	}
}
