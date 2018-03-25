using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid3D : MonoBehaviour {
	public static BuildingGrid3D instance;

	public BuildingGridCell cellPrefab;
	public float cellSize = 3;
	public Dictionary<Int3, BuildingBlock> cells = new Dictionary<Int3, BuildingBlock>();

	public BuildingGrid3D() {
		instance = this;
	}

	public BuildingBlock GetCell(Int3 gridPos) {
		BuildingBlock cell;
		//if (!cells.TryGetValue (gridPos, out cell)) {
		//}
		cells.TryGetValue (gridPos, out cell);
		return cell;
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
