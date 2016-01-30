﻿using UnityEngine;
using System.Collections;


public class Board: MonoBehaviour {

	public float cellsHorizontal;
	public float cellsVertical;
	public Shape[] shapes;
	public float hoverHeight;
	public Shape[,]grid;

	private float cellWidth;
	private float cellHeight;
	private float actualWidth;
	private float actualHeight;
	private Vector3 bottomLeft;


	// Use this for initialization
	void Start () {
		actualWidth = transform.lossyScale.x;
		actualHeight = transform.lossyScale.z;
		cellWidth = actualWidth / cellsHorizontal;
		cellHeight = actualHeight / cellsVertical;
		bottomLeft = transform.position - new Vector3(transform.lossyScale.x / 2f, 0f, transform.lossyScale.z / 2f);
		grid = new Shape[(int) cellsHorizontal, (int) cellsVertical];
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Vector2 getCellCoords(Vector3 pos) {
		float xDist = pos.x - bottomLeft.x;
		float zDist = pos.z - bottomLeft.z;
		float xCoord = (float)(int) (xDist / cellWidth);
		float zCoord = (float)(int) (zDist / cellHeight);
//		Debug.Log (cellWidth + ", " + cellHeight);
//		Debug.Log (xDist + "," + zDist + ", " + xCoord + ", " + zCoord);
		//Debug.Log (xCoord + ", " + zCoord);
		return new Vector2 (xCoord, zCoord);
	}

	public Vector3 getCellCenter(Vector2 cell) {
		return new Vector3 (bottomLeft.x + (cell.x+0.5f) * cellWidth,
							0f,
							bottomLeft.z + (cell.y+0.5f) * cellHeight);
	}

	public Vector3 closestPoint(Vector3 pos) {
		Vector3 temp = getCellCenter(getCellCoords(pos));
		temp.y = pos.y;
		return temp;
	}

	public bool checkSideOrder () {
		for(int i = 0; i < grid.GetLength(0); i++) {
			int minSides = 0;
			if(grid[i,0] != null)
				minSides = grid [i, 0].sides;
			for (int j = 0; j < grid.GetLength (1); j++) {
				Debug.Log (minSides);
				if (grid[i,j] != null && grid[i,j].sides > minSides) {
					minSides = grid[i,j].sides;
				} else if (grid[i,j] != null && grid[i,j].sides < minSides) {
				return false;
				}
			}
		}
		return true;
	}

	public bool checkColorOrder() {
		for(int i = 0; i < grid.GetLength(1); i++) {
			int minColor = 0;
			if(grid[0,i] != null)
				minColor = grid [0, i].color;
			for (int j = 0; j < grid.GetLength (0); j++) {
				if (grid[j,i] != null && grid[j,i].color > minColor) {
					minColor = grid[j,i].color;
				} else if (grid[j,i] != null && grid[j,i].color < minColor) {
					return false;
				}
			}
		}
		return true;
	}

	public bool checkSizeOrder() {
		for(int i = 0; i < grid.GetLength(1); i++) {
			float minSize = 0;
			if(grid[0,i] != null)
				minSize = grid [0, i].size;
			for (int j = 0; j < grid.GetLength (0); j++) {
				if (grid[j,i] != null && grid[j,i].size > minSize) {
					minSize = grid[j,i].size;
				} else if (grid[j,i] != null && grid[j,i].size < minSize) {
					return false;
				}
			}
		}
		return true;
	}

//	public void sortHorizontal () {
//		for (int i = 0; i < shapes.Length; i++) {
//			int minShape = i;
//			float minX = shapes[i].transform.position.x;
//			for (int j = i; j < shapes.Length; j++) {
//				if (shapes [j].transform.position.x < minX) {
//					minShape = j;
//					minX = shapes [j].transform.position.x;
//				}
//			}
//			Shape temp = shapes [i];
//			shapes [i] = shapes [minShape];
//			shapes [minShape] = temp;
//		}
//
//
//	}
//
//	public void sortVertical() {
//		for (int i = 0; i < shapes.Length; i++) {
//			int maxShape = i;
//			float maxZ = shapes[i].transform.position.z;
//			for (int j = i; j < shapes.Length; j++) {
//				if (shapes [j].transform.position.z > maxZ) {
//					maxShape = j;
//					maxZ = shapes [j].transform.position.z;
//				}
//			}
//			Shape temp = shapes [i];
//			shapes [i] = shapes [maxShape];
//			shapes [maxShape] = temp;
//		}
//
//	}

//	public bool checkSideOrder() {
//		//sortHorizontal ();
//		int minSides = shapes[0].sides;
//		for (int i = 0; i < shapes.Length; i++) {
//			if (shapes [i].sides > minSides) {
//				minSides = shapes [i].sides;
//			} else if (shapes [i].sides < minSides) {
//				return false;
//			}
//		}
//		return true;
//	}
//
//	public bool checkColorOrder() {
//		//sortVertical ();
//		int minColor = shapes [0].color;
//		for (int i = 0; i < shapes.Length; i++) {
//			if (shapes [i].color > minColor) {
//				minColor = shapes [i].color;
//			} else if (shapes [i].color < minColor) {
//				return false;
//			}
//		}
//		return true;
//	}


//	void OnDrawGizmos() {
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawSphere (bottomLeft, 0.1f);
//		for (int i = 0; i < cellsHorizontal; i++) {
//			for (int j = 0; j < cellsVertical; j++) {
//				Gizmos.DrawSphere (getCellCenter(new Vector2((float) i, (float) j)), 0.1f);
//			}
//		}
//	}

}
