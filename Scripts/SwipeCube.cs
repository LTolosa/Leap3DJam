using UnityEngine;
using System.Collections;
using Leap;

public class SwipeCube : MonoBehaviour {

    public Controller hc;
    private float time = 0;
    private Vector3 dir = Vector3.zero;
    private SwipeGesture sg = null;
		private float[] coords;
		private float max_dir;
    private Frame last;
    private Frame cur;

    // Use this for initialization
    void Start()
    {
        hc = new Controller();
        hc.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        cur = hc.Frame();
    }

    // Update is called once per frame
    void Update () {
        if (hc.IsConnected && sg == null)
        {
            last = cur;
            cur = hc.Frame();
            long dif = cur.Id - last.Id;
            while (dif > 0)
            {
              Frame frame = hc.Frame((int)dif);
              GestureList gests = frame.Gestures();
              for (int i = 0; i < gests.Count; i++)
              {
                if (gests[i].Type == Gesture.GestureType.TYPE_SWIPE)
                {
                  time = 0;
                  sg = new SwipeGesture(gests[i]);
                  dir = UnityVectorExtension.ToUnity(sg.Direction);
                  coords = new float[] { Mathf.Abs(dir.x),
                                         Mathf.Abs(dir.y),
                                         Mathf.Abs(dir.z) };
                  max_dir = Mathf.Max(coords);
                  break;
                }
              }
              dif--;
            }
        }

				if (sg != null && time < 90) {
					if (max_dir == coords[0]) {
						if (dir.x > 0) {
            	this.transform.eulerAngles = new Vector3(
													this.transform.eulerAngles.x,
													this.transform.eulerAngles.y - 5,
													this.transform.eulerAngles.z);
            } else {
              this.transform.eulerAngles = new Vector3(
													this.transform.eulerAngles.x,
													this.transform.eulerAngles.y + 5,
													this.transform.eulerAngles.z);
						}
					} else if (max_dir == coords[1]) {
            if (dir.y > 0) {
            	this.transform.eulerAngles = new Vector3(
													this.transform.eulerAngles.x + 5,
													this.transform.eulerAngles.y,
													this.transform.eulerAngles.z);
            } else {
              this.transform.eulerAngles = new Vector3(
													this.transform.eulerAngles.x - 5,
													this.transform.eulerAngles.y,
													this.transform.eulerAngles.z);
						}
            time += 5;
        }
				if (time == 90) {
            sg = null;
        }
      }

}
