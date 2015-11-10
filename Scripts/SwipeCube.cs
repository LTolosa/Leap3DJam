using UnityEngine;
using System.Collections;
using Leap;

public class SwipeCube : MonoBehaviour {

    public static Controller hc;
    public static bool checkSwipe = true;
    private float time = 0;
    private float wait = 0;
    private Vector3 dir = Vector3.zero;
    private SwipeGesture lr = null;
    private SwipeGesture ud = null;
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
        hc.Config.SetFloat("Gesture.Swipe.MinLength", 200.0f);
        hc.Config.SetFloat("Gesture.Swipe.MinVelocity", 300f);
        hc.Config.Save();
        cur = hc.Frame();

    }

    // Update is called once per frame
    void Update ()
    {
      if (hc.IsConnected && lr == null && ud == null && wait == 0 && checkSwipe)
      {
        //Debug.Log("Checking for swipes.");
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
              if (max_dir == coords[0])
              {
                lr = sg;
              }
              else if (max_dir == coords[1])
              {
                ud = sg;
              }
              break;
            }
          }
          if (lr != null || ud != null) break;
          dif--;
        }
          wait = 15;

          //PalmRotator.canRotate = false;
        }

			if (time < 90 && wait > 0 && checkSwipe)
      {
        //Debug.Log("Choosing what direction.");
        if (lr != null && ud == null)
        {
					if (dir.x > 0)
          {
            this.transform.RotateAround(
            this.transform.position, Vector3.up, -10);
          }
          else
          {
            this.transform.RotateAround(
            this.transform.position, Vector3.up, 10);
					}
			  }
        else if (lr == null && ud != null)
        {
          if (dir.y > 0)
          {
            this.transform.RotateAround(
            this.transform.position, Vector3.right, 10);
          }
          else
          {
            this.transform.RotateAround(
            this.transform.position, Vector3.right, -10);
          }
        }
        if(time == 20)
                GetComponent<AudioSource>().Play();
            time += 10;
      }
			else if (time == 90)
      {
        //Debug.Log("Finished swipe.");
        if (lr != null && ud == null)
        {
          lr = null;
          PalmRotator.canRotate = true;
          if(PlayerPrefs.GetInt("Difficulty") == 1)
            PalmRotator.textRef.text = "Moves: " + (++PalmRotator.moveCount);
        }
        else if (lr == null && ud != null)
        {
          ud = null;
          PalmRotator.canRotate = true;
          if(PlayerPrefs.GetInt("Difficulty") == 1)
            PalmRotator.textRef.text = "Moves: " + (++PalmRotator.moveCount);
        }
        wait--;
      }
    }

}
