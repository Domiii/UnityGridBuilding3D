using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid3D : MonoBehaviour {
	public static BuildingGrid3D instance;

	public BuildingGridCell cellPrefab;
	public float cellSize = 3;
	public BuildingBlock[] allBlockPrefabs;
	Dictionary<Int3, BuildingBlock>[] cells;

	public BuildingGrid3D() {
		instance = this;
	}

	void Awake() {
		cells = new Dictionary<Int3, BuildingBlock>[allBlockPrefabs.Length];
		for (var i = 0; i < cells.Length; ++i) {
			cells [i] = new Dictionary<Int3, BuildingBlock> ();
		}
	}

	public Int3 GetBestMatchingCell(Ray cursorRay, float maxDist, int blockType) {
		// 1. check for each feasible cell; there are at most 2 (the target cell, and the cell between own and target cell, ignoring own cell), starting with the closest
		// 2. 

		// TODO: each cell has 8 edge connectors.
		// TODO: Determine the best matching cell, based on: (1) cursor position, (2) connectivity constraints, (3) selected block type
		// TODO: also have to output matching rotation?
		return new Int3();
	}

	public BuildingBlock GetCell(Int3 gridPos, int blockType) {
		BuildingBlock cell;	
		//if (!cells.TryGetValue (gridPos, out cell)) {
		//}
		cells[blockType].TryGetValue (gridPos, out cell);
		return cell;
	}

	public void SetCell(Int3 gridPos, int blockType, BuildingBlock block) {
		cells[blockType].Add(gridPos, block);
		block.transform.SetParent (transform, true);
	}

//	public BuildingBlock GetOrCreateCell(Int3 gridPos) {
//		BuildingBlock cell;
//		if (!cells.TryGetValue (gridPos, out cell)) {
//			var pos = GridToWorld (gridPos);
//			var rot = Quaternion.identity;
//
//			cell = Instantiate (cellPrefab, pos, rot, transform);
//			cells.Add (gridPos, cell);
//		}
//		return cell;
//	}

	/// <summary>
	/// Convert world position to grid coordinates
	/// </summary>
	public Int3 WorldToGrid(Vector3 worldPos) {
		return new Int3 (worldPos / cellSize);
	}

	/// <summary>
	/// Convert world position to grid coordinates
	/// </summary>
	public Vector3 GridToWorld(Int3 gridPos) {
		return (Vector3)(gridPos * cellSize);
	}

	/// <summary>
	/// Determines the offset position of the cell containing the given world position
	/// </summary>
	public Vector3 SnapWorld(Vector3 worldPos) {
		return GridToWorld(WorldToGrid(worldPos));
	}
}
