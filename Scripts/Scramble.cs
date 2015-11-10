using UnityEngine;
using System.Collections;
using LMWidgets;

public class Scramble : MonoBehaviour {

    public ButtonDemo scrambler;
    public int moves = 20;
    public Transform[] faces = new Transform[6];

    private System.Random rnd = new System.Random();
    private bool busy = false;
	// Use this for initialization
	void Start () {
        scrambler.StartHandler += OnScramble;
	}

	// Update is called once per frame
	void Update () {

	}

    void OnScramble(object sender, LMWidgets.EventArg<bool> arg) {
        if (arg.CurrentValue && (SwipeCube.checkSwipe || !PalmRotator.canRotate) && !busy)
        {
            busy = true;
            StartCoroutine(ScrambleCube());
        }
    }

    IEnumerator ScrambleCube() {
        SwipeCube.checkSwipe = false;
        PalmRotator.scrambling = true;
        PalmRotator.canRotate = false;
        for (int i = 0; i < moves; i++)
        {
            int index = rnd.Next(0, 6);
            Transform face = faces[index];

            for(int j = 0; j < 9; j++)
            {
                face.GetComponent<GrabDetector>().RemoveCubes();
                face.GetComponent<GrabDetector>().AddCubes();
                if(index < 2)
                    face.RotateAround(face.position, Vector3.right, 10);
                else if(index < 4)
                    face.RotateAround(face.position, Vector3.up, 10);
                else if(index < 6)
                    face.RotateAround(face.position, Vector3.forward, 10);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            face.GetComponent<GrabDetector>().RemoveCubes();


        }
        SwipeCube.checkSwipe = true;
        PalmRotator.scrambling = false;
        PalmRotator.canRotate = true;
        busy = false;
    }
}
