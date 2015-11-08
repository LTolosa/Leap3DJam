using UnityEngine;
using System.Collections;
using LMWidgets;

public class ButtonDemoActionExample : MonoBehaviour {

  public ButtonDemo SimpleButton; 

	// Use this for initialization
	void Start () {
    SimpleButton.StartHandler += OnSimpleButtonAction;

	}
  
  private void OnSimpleButtonAction (object sender, LMWidgets.EventArg<bool> arg) {
    Debug.Log ("Firing the ButtonDemo Example Button");
    
  }
	

}
