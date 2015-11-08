using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabDetector : MonoBehaviour {

	public GameObject RubiksCube;

	public GameObject rightGrab;

	private List<GameObject> CubeLists;

	// Use this for initialization
	void Start () {
    CubeLists = new List<GameObject>();
  }

	// Update is called once per frame
	void Update () {

	}

	public void AddCubes(){
		foreach(GameObject cube in CubeLists){
            if(cube.CompareTag("Cube"))
    	        cube.transform.parent = this.gameObject.transform;
        }
	}

	public void RemoveCubes(){
		foreach(GameObject cube in CubeLists){
    	    cube.transform.parent = RubiksCube.transform;
        }
    }

	///<summary>
	/// Used to keep track of the cubes inthis trigger
	///</summary>
	void OnTriggerEnter(Collider other){
        // Check if object is in the List
		if(!CubeLists.Contains(other.gameObject) && other.tag == "Cube"){
            //Debug.Log("Added " + other.gameObject.name);
            CubeLists.Add(other.gameObject);
		}
  }

	///<summary>
	/// Used to keep track of the cubes inthis trigger
	///</summary>
	void OnTriggerStay(Collider other){
        // Check if object is in the List
        if (!CubeLists.Contains(other.gameObject) && other.tag == "Cube"){
            CubeLists.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if(CubeLists.Contains(other.gameObject)){
            CubeLists.Remove(other.gameObject);
        }
	}
}
