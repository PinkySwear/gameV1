using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 curPosPrev;
	private float xDeg;
	private float yDeg;
	private Rigidbody rb;
	private bool dragged;

	public Board board;
	public bool snapToGrid;
	public int sides;
	public int color;
	public float size;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		size = transform.lossyScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.x == 0f && transform.rotation.z == 0f) {
			rb.freezeRotation = true;
		}

		if (dragged) {
			transform.position = new Vector3(transform.position.x, board.hoverHeight, transform.position.z);
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPos = Camera.main.ScreenToWorldPoint (curScreenPoint);
			curPos.y = board.hoverHeight;
			transform.position = curPos;

			//Debug.Log("(" + curScreenPoint.x + ", " + curScreenPoint.y + ", " + curScreenPoint.z + ")");
		}
	}

	void OnMouseDown() {
		dragged = true;
		Vector2 cellC = board.getCellCoords (transform.position);
		if (cellC.x < 6 && cellC.y < 6) {
			board.grid [(int)cellC.x, (int)cellC.y] = null;
		}
	}

	void OnMouseUp() {
		if (dragged && snapToGrid) {
			transform.position = board.closestPoint (transform.position);
		}
		dragged = false;
		//Debug.Log(board.checkSideOrder());
//		Debug.Log(board.checkColorOrder() && board.checkSideOrder());
		Debug.Log(board.checkSideOrder());
		Vector2 cellC = board.getCellCoords (transform.position);
		if (cellC.x < 6 && cellC.y < 6) {
			board.grid [(int)cellC.x, (int)cellC.y] = this;
		}
	}
		

//	void OnMouseDrag() {
////		if (Input.GetMouseButton (1)) {
////			transform.position = curPosPrev;
////			xDeg -= Input.GetAxis("Mouse X") * 10;
////			//yDeg += Input.GetAxis("Mouse Y") * 5;
////			Quaternion fromRotation = transform.rotation;
////			Quaternion toRotation = Quaternion.Euler(0,xDeg,0);
////			transform.rotation = Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime * 5);
////		} else {
//			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
//			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//			Vector3 curPos = Camera.main.ScreenToWorldPoint (curScreenPoint);
//			curPos.y = 1f;
//			transform.position = curPos;
//			curPosPrev = curPos;
//			dragged = true;
////		}
//	}

//	void OnDrawGizmos() {
//		Gizmos.color = Color.yellow;
//
//		Gizmos.DrawSphere (board.closestPoint(transform.position), 2f);
//	}
}
