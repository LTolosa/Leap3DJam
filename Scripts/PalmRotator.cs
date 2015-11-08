using UnityEngine;
using System.Collections;

public class PalmRotator : MonoBehaviour {

    public static string currentRotating = "";

	public GrabDetector grabSide;
	public bool rotateX;

    private RigidHand hand;
	private Vector3 startAngle;
	private Vector3 sideStartAngle;
    private Vector3 previousAngle;
    private Vector3 nextAngle;
    private float interTime = 0.0f;

	private string HAND_NAME = "RigidRoundHand(Clone)";
	private bool canRotate = true;
    private bool interp = false;
    private bool removeCubes = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (hand == null)
            StartInterp();
        if (hand != null)
            Debug.Log(hand.GetPalmRotation().eulerAngles);
		if(currentRotating == this.name){
            if (canRotate) {
                removeCubes = false;
                Debug.Log("In rotating" + Time.time);
                Vector3 euler = sideStartAngle + 3 * (hand.GetPalmRotation().eulerAngles - startAngle);
                euler = rotateX ? new Vector3(euler.x, 0, 0) : new Vector3(0, euler.y, 0);
                float dif = rotateX ? euler.x - grabSide.transform.eulerAngles.x : euler.y - grabSide.transform.eulerAngles.y;
                previousAngle = grabSide.transform.eulerAngles;
                grabSide.transform.rotation = Quaternion.Euler(euler);
                float curDegreeChange = rotateX ? euler.x - sideStartAngle.x : euler.y - sideStartAngle.y;

                if (Mathf.Abs(curDegreeChange) > 45 && Mathf.Sign(curDegreeChange) == Mathf.Sign(dif)) {
                    canRotate = false;
                    nextAngle = rotateX ? new Vector3(sideStartAngle.x + Mathf.Sign(curDegreeChange) * 90, 0, 0)
                                        : new Vector3(0, sideStartAngle.y + Mathf.Sign(curDegreeChange) * 90, 0);
                    interTime = 0.0f;
                }

            }
		}

        if (interp) {
            SwipeCube.checkSwipe = false;
            if (interTime <= 1.0f)
            {
                Vector3 euler = Vector3.Lerp(previousAngle, nextAngle, interTime);
                grabSide.transform.rotation = Quaternion.Euler(euler);
                interTime += Time.deltaTime;
            }
            else
            {
                interp = false;
                if (removeCubes) {
                    grabSide.RemoveCubes();
                    removeCubes = false;
                    SwipeCube.checkSwipe = true;
                    hand = null;
                    currentRotating = "";

                }
                else {
                    startAngle = hand.GetPalmRotation().eulerAngles;
                    sideStartAngle = grabSide.transform.eulerAngles;
                    canRotate = true;

                }
            }
        }
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log(other.transform.root.name);
		if(other.transform.root.name == HAND_NAME && hand == null && currentRotating == ""){
			hand = other.transform.root.GetComponent<RigidHand>();
			startAngle = hand.GetPalmRotation().eulerAngles;
			sideStartAngle = grabSide.transform.eulerAngles;
			grabSide.AddCubes();
			canRotate = true;
            SwipeCube.checkSwipe = false;
            PalmRotator.currentRotating = name;
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (hand != null && currentRotating == this.name && !interp) {
            canRotate = true;
            grabSide.AddCubes();
        }
    }

	void OnTriggerExit(Collider other){
		if(hand != null && other.transform.root.GetComponent<RigidHand>() == hand){
            removeCubes = true;
            StartInterp();
		}
	}

    void StartInterp()
    {
        removeCubes = true;
        interp = true;
        interTime = 0.0f;
        previousAngle = grabSide.transform.eulerAngles;
        float ang = rotateX ? Mathf.Round(previousAngle.x / 90.0f) * 90 : Mathf.Round(previousAngle.y / 90.0f) * 90;
        nextAngle = rotateX ? new Vector3(ang, 0, 0) : new Vector3(0, ang, 0);
    }
}
