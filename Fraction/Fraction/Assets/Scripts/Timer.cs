using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float time;
    public int time2;
    private Text text;
    // Use this for initialization
    private GameObject[] bubbles;
    GameObject spawn;



    void Start () {
        spawn = GameObject.Find("Spawner");
        text = this.GetComponent<Text>();
        time2 = (int)time;
        text.text = time2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(spawn);
            bubbles = GameObject.FindGameObjectsWithTag("bubble");
            foreach (GameObject bubble in bubbles)
            {
                Destroy(bubble);
            }
        } else
        {
            time2 = (int)time;
            text.text = time2.ToString();
        }
        
    }
}
