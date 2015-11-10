using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour {
    [SerializeField]
    public static string[] Colors = { "White", "Red", "Green", "Orange", "Blue", "Yellow" };

    public List<int> colorIndex;
    public List<Vector3> faceDir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public Dictionary<Vector3, int> GetFaceColors()
    {
        Dictionary<Vector3, int> result = new Dictionary<Vector3, int>();

        for (int i = 0; i < faceDir.Count; i++)
        {
            result.Add(transform.TransformDirection(faceDir[i]), colorIndex[i]);

        }

        return result;
    }
}
