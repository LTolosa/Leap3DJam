using UnityEngine;
using System.Collections;

public class ScoreButtonHandler : MonoBehaviour
{

    public ButtonDemo mode;

    // Use this for initialization
    void Start()
    {
        mode.StartHandler += OnSelect;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect(object sender, LMWidgets.EventArg<bool> arg)
    {
        if (arg.CurrentValue)
        {
            Application.LoadLevel(0);
        }
    }
}
