using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckSolved : MonoBehaviour {
    public static bool firstScrambled = false;
    public AudioClip tada;
    private Cube[] cubes;
    private bool finished = false;

    // Use this for initialization
    void Start () {
        cubes = GetComponentsInChildren<Cube>();
	}

	// Update is called once per frame
	void Update () {
        if(!PalmRotator.scrambling)
        {
            Dictionary<int, int> faceCheck = new Dictionary<int, int>();
            faceCheck.Add(0, -1);
            faceCheck.Add(1, -1);
            faceCheck.Add(2, -1);
            faceCheck.Add(3, -1);
            faceCheck.Add(4, -1);
            faceCheck.Add(5, -1);
            bool solved = true;
            foreach(Cube cube in cubes)
            {
                Dictionary<Vector3, int> cubeFaces = cube.GetFaceColors();
                foreach(KeyValuePair<Vector3, int> face in cubeFaces)
                {
                    int key;
                    if (Vector3.back == face.Key)
                        key = 0;
                    else if (Vector3.down == face.Key)
                        key = 1;
                    else if (Vector3.forward == face.Key)
                        key = 2;
                    else if (Vector3.left == face.Key)
                        key = 3;
                    else if (Vector3.right == face.Key)
                        key = 4;
                    else
                        key = 5;

                    if (faceCheck[key] == -1)
                        faceCheck[key] = face.Value;
                    else if (faceCheck[key] != face.Value)
                    {
                        solved = false;
                        break;
                    }

                }
                if (!solved)
                    break;
            }

            if(solved && !finished && firstScrambled)
            {
                StartCoroutine(End());
            }

        }

    }

    IEnumerator End(){
      finished = true;
      GetComponent<AudioSource>().clip = tada;
      GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.063f);
        PlayerPrefs.SetInt("CurScore", PalmRotator.moveCount);
      Application.LoadLevel(2);
    }
}
