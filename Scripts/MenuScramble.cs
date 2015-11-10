using UnityEngine;
using System.Collections;

public class MenuScramble : MonoBehaviour {
	public Transform[] faces = new Transform[6];
	private System.Random rnd = new System.Random();

	// Use this for initialization
	void Start () {
        StartCoroutine(ScrambleCube());
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	IEnumerator ScrambleCube() {
        yield return new WaitForSeconds(Time.deltaTime);
        for (;;)
			{
					int index = rnd.Next(0, 6);
					Transform face = faces[index];

					for(int j = 0; j < 5; j++)
					{
							face.GetComponent<GrabDetector>().RemoveCubes();
							face.GetComponent<GrabDetector>().AddCubes();
							if(index < 2)
									face.RotateAround(face.position, face.right, 18);
							else if(index < 4)
									face.RotateAround(face.position, face.up, 18);
							else if(index < 6)
									face.RotateAround(face.position, face.forward, 18);
							yield return new WaitForSeconds(Time.deltaTime*2);
					}
					face.GetComponent<GrabDetector>().RemoveCubes();


			}
	}
}
