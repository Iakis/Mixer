using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour {

    public List<GameObject> part = new List<GameObject>();
    public List<GameObject> text = new List<GameObject>();

    private int i;
    // Use this for initialization
    void Start () {
        part.Add(GameObject.Find("Time"));
        part.Add(GameObject.Find("Score"));
        part.Add(GameObject.Find("Comp"));
        part.Add(GameObject.Find("Bob"));
        part.Add(GameObject.Find("Bucket"));
        part.Add(GameObject.Find("Start"));

        text.Add(GameObject.Find("TimeT"));
        text.Add(GameObject.Find("ScoreT"));
        text.Add(GameObject.Find("CompT"));
        text.Add(GameObject.Find("Target"));
        text.Add(GameObject.Find("BucketT"));
        text.Add(GameObject.Find("Info"));
        text.Add(GameObject.Find("Hint"));
        foreach (GameObject g in part)
        {
            g.SetActive(false);

        }
        foreach (GameObject a in text)
        {
            a.SetActive(false);

        }
        i = 0;
        //GameObject.Find("Start").SetActive(false);
        GameObject.Find("Tap").SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (i < part.Count - 1)
            {
                part[i].SetActive(true);
            }
            if (i < text.Count)
            {
                text[i].SetActive(true);
                if (i - 1 >= 0 && i < 6){
                    text[i - 1].SetActive(false);
                }
            }
            
            i += 1;
            if (i == text.Count)
            {
                GameObject.Find("Tap").SetActive(false);
                part[5].SetActive(true);
                i += 1;
            }
        }
        
    }
}
