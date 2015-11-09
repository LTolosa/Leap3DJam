using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public class PalmRotator : MonoBehaviour {

    public static string currentRotating = "";
    public static bool scrambling = false;
    public static int moveCount = 0;
    public static Text textRef;


    public GrabDetector grabSide;
    public SwipeCube swipe;
    public Text moveText;
    public bool rotateX;


    private RigidHand hand;
    private float startTime;
	private Vector3 startAngle;
	private Vector3 sideStartAngle;
    private Vector3 previousAngle;
    private Vector3 nextAngle;
    private int handId;
    private float interTime = 0.0f;

	private string HAND_NAME = "RigidRoundHand(Clone)";
	public static bool canRotate = true;
    private bool interp = false;
    private const float TIME_DIF = 1f;
	// Use this for initialization
	void Start () {
        if(moveText != null)
        {
            textRef = moveText;
        }
	}

	// Update is called once per frame
	void Update () {
        if(currentRotating == this.name && !scrambling){
            if (canRotate && startTime + TIME_DIF < Time.time ) {
                float startDegree = rotateX ? startAngle.x : startAngle.y;
                float curDegree = rotateX ? hand.GetPalmRotation().eulerAngles.x : hand.GetPalmRotation().eulerAngles.y;
                float dir = Mathf.Sign(curDegree - startDegree);
                if(Mathf.Abs(curDegree - startDegree) > 30) {
                    swipe.enabled = false;
                    grabSide.AddCubes();
                    previousAngle = rotateX ? new Vector3(sideStartAngle.x, 0, 0) : new Vector3(0, sideStartAngle.y, 0);
                    nextAngle = rotateX ? new Vector3(sideStartAngle.x + 90 * dir, 0, 0) : new Vector3(0, sideStartAngle.y + 90 * dir, 0);
                    interp = true;
                    canRotate = false;
                    interTime = 0.0f;
                    textRef.text = "Moves: " + (++moveCount);
                }
            }

            Frame frame = SwipeCube.hc.Frame();
            bool found = false;
            for (int h = 0; h < frame.Hands.Count; h++)
            {
                if (frame.Hands[h].Id == handId)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (!interp)
                {
                    grabSide.RemoveCubes();
                    //swipe.enabled = true;
                }
                canRotate = false;
                hand = null;
                currentRotating = "";
            }
        }

        if (interp) {
            if (interTime <= 1.0f)
            {
                grabSide.transform.rotation = Quaternion.Euler(Vector3.Lerp(previousAngle, nextAngle, interTime));
                interTime += (interTime > 0.5f ? 1 : 2.5f)*Time.deltaTime;
            }
            else
            {
                interp = false;
                swipe.enabled = true;
                Vector3 snap = new Vector3(Mathf.Round(nextAngle.x / 90f) * 90, Mathf.Round(nextAngle.y / 90f) * 90, 0);
                grabSide.transform.rotation = Quaternion.Euler(snap);
                grabSide.RemoveCubes();
                /*****
                CLEAN   
                */
                if (hand == null) {
                    grabSide.RemoveCubes();
                    //swipe.enabled = true;
                } else { 
                    startAngle = hand.GetPalmRotation().eulerAngles;
                    sideStartAngle = grabSide.transform.eulerAngles;
                    canRotate = true;
                    startTime = Time.time;
                }
                
            }
        }

	}

	void OnTriggerEnter(Collider other){
		if(other.transform.root.name == HAND_NAME && hand == null && currentRotating == "" && other.CompareTag("Palm") && !scrambling){
			hand = other.transform.root.GetComponent<RigidHand>();
            handId = hand.GetLeapHand().Id;
			startAngle = hand.GetPalmRotation().eulerAngles;
			sideStartAngle = grabSide.transform.eulerAngles;
			canRotate = true;
            //swipe.enabled = false;
            PalmRotator.currentRotating = name;
            startTime = Time.time;
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (hand != null && currentRotating == this.name && !interp && other.CompareTag("Palm")) {
            canRotate = true;
            //grabSide.AddCubes();
        }
    }

	void OnTriggerExit(Collider other){
		if(hand != null && other.transform.root.GetComponent<RigidHand>() == hand && other.CompareTag("Palm")){
            if (!interp){
                grabSide.RemoveCubes();
                //swipe.enabled = true;
            }
            canRotate = false;
            hand = null;
            currentRotating = "";
		}
	}



 
}
