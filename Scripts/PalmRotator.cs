using UnityEngine;
using System.Collections;

public class PalmRotator : MonoBehaviour {

	public GrabDetector grabSide;
	public bool rotateX;

  private RigidHand hand;
	private Vector3 startAngle;
	private Vector3 sideStartAngle;


	private string HAND_NAME = "RigidRoundHand(Clone)";
	private bool canRotate = true;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(hand);
		if(hand != null && canRotate){
    	Vector3 euler = sideStartAngle + (hand.GetPalmRotation().eulerAngles - startAngle);
			euler = rotateX ? new Vector3(euler.x, 0, 0) : new Vector3(0, euler.y, 0);
      grabSide.transform.rotation = Quaternion.Euler(euler);
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log(other.transform.root.name);
		if(other.transform.root.name == HAND_NAME && hand == null){
			hand = other.transform.root.GetComponent<RigidHand>();
			startAngle = hand.GetPalmRotation().eulerAngles;
			sideStartAngle = grabSide.transform.eulerAngles;
			grabSide.AddCubes();
			canRotate = false;
		}
	}

	void OnTriggerExit(Collider other){
		if(hand != null && other.transform.root.GetComponent<RigidHand>() == hand){
			grabSide.RemoveCubes();
			hand = null;
			canRotate = true;
		}
	}
}
