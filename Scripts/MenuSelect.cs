using UnityEngine;
using System.Collections;
using LMWidgets;

public class MenuSelect : MonoBehaviour {

    public ButtonDemo mode;
    public bool hard = false;

    // Use this for initialization
    void Start () {
        mode.StartHandler += OnSelect;
    }

	// Update is called once per frame
	void Update () {

	}

	void OnSelect(object sender, LMWidgets.EventArg<bool> arg) {
      if (arg.CurrentValue)
      {
          if (hard)
          {
              PlayerPrefs.SetInt("Difficulty", 1);
              Application.LoadLevel(1);
          }
          else
          {
              PlayerPrefs.SetInt("Difficulty", 0);
							Application.LoadLevel(1);
          }
      }
  }
}
