using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillScores : MonoBehaviour {

    public Text[] positions = new Text[5];

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Score1", 10);
        PlayerPrefs.SetInt("Score2", 20);
        PlayerPrefs.SetInt("Score3", 30);
        PlayerPrefs.SetInt("CureScore", 40);
        int[] scores = new int[5];
        int numOfScores = 0;
        if (PlayerPrefs.HasKey("Score1"))
        {
            numOfScores = 1;
            scores[0] = PlayerPrefs.GetInt("Score1");
        }
        if (PlayerPrefs.HasKey("Score2"))
        {
            numOfScores = 2;
            scores[1] = PlayerPrefs.GetInt("Score2");
        }
        if (PlayerPrefs.HasKey("Score3"))
        {
            numOfScores = 3;
            scores[2] = PlayerPrefs.GetInt("Score3");
        }
        if (PlayerPrefs.HasKey("Score4"))
        {
            numOfScores = 4;
            scores[3] = PlayerPrefs.GetInt("Score4");
        }
        if (PlayerPrefs.HasKey("Score5"))
        {
            numOfScores = 5;
            scores[4] = PlayerPrefs.GetInt("Score5");
        }

        int curScore = PlayerPrefs.GetInt("CurScore");
        int indexInTop = -1;
        if(numOfScores < 5)
        {
            indexInTop = numOfScores
        }
        for(int i = 0; i < numOfScores; i++)
        {
            if (scores[i] > curScore)
                indexInTop = i;
        }

        if(indexInTop != -1)
        {
            numOfScores++;
            if (numOfScores > 5) numOfScores = 5;
        }

        for(int i = 0; i < 5; i++)
        {
            if(indexInTop != -1 && i == indexInTop)
            {
                positions[indexInTop].text = i + ": " + curScore;
                positions[indexInTop].color = new Color(1, 1, 0);
            }
            if(i >= numOfScores)
            {
                positions[i].enabled = false;
            }
            else if(indexInTop != -1 && i > indexInTop)
            {
                positions[i].text = i + ": " + scores[i - 1];
            }
            else
            {
                positions[i].text = i + ": " + scores[i];
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
