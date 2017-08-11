using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score;
    private Text text;
    public float time;

	// Use this for initialization
	void Start () {
        score = 0;
        text = this.GetComponent<Text>();
        text.text = score.ToString();
        time = GameObject.Find("Time").GetComponent<Timer>().time;

    }
	
	// Update is called once per frame
	void Update () {
        text.text = score.ToString();
        time = GameObject.Find("Time").GetComponent<Timer>().time;
        if (time < 0)
        {
            this.text.fontSize = 40;
            string temp = "Score: " + score.ToString();
            text.text = temp;
            Vector3 temp2 = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
            Vector3 centerPos = Camera.main.ScreenToWorldPoint(temp2);
            this.gameObject.transform.position = centerPos;
            text.alignment = TextAnchor.MiddleCenter;
        }
	}
}
