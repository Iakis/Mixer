using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp : MonoBehaviour
{
    public float fillTo;
    public float fillTo1;
    public float fillTo2;

    public float value;
    public float value1;
    public float value2;

    public Color c;

    private Vector3 temp1;
    private Vector3 temp;
    private Vector3 temp2;
    private Color tempc;

    public GameObject bucket;
    public GameObject paint;
    public GameObject paint1;
    public GameObject paint2;

    public float r;
    public float b;
    public float y;

    public List<float> col = new List<float>();
    public List<KeyValuePair<float, Color>> pairs = new List<KeyValuePair<float, Color>>();

    // Use this for initialization
    void Start()
    {
        bucket = null;
        pairs.Clear();
    }

    public void init()
    {
        temp1 = new Vector3(-0.1f, 0.5f, 0);
        gameObject.transform.GetChild(0).transform.localScale = temp1;
        gameObject.transform.GetChild(1).transform.localScale = temp1;
        gameObject.transform.GetChild(2).transform.localScale = temp1;
        pairs.Clear();
        if (bucket)
        {
            if (bucket.GetComponent<Mouse>())
            {
                r = bucket.GetComponent<Mouse>().red;
                b = bucket.GetComponent<Mouse>().blue;
                y = bucket.GetComponent<Mouse>().yellow;
            } else
            {
                r = bucket.GetComponent<Bob>().red;
                b = bucket.GetComponent<Bob>().blue;
                y = bucket.GetComponent<Bob>().yellow;
            }

            var red = new KeyValuePair<float, Color>(r, new Color32(255, 0, 0, 255));
            var blue = new KeyValuePair<float, Color>(b, new Color32(0, 124, 247, 255));
            var yellow = new KeyValuePair<float, Color>(y, new Color32(255, 237, 0, 255));
            pairs.Add(red);
            pairs.Add(blue);
            pairs.Add(yellow);
            pairs.Sort((a, b) => a.Key.CompareTo(b.Key));

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bucket)
        {
            fill();
            fill1();
            fill2();
        }

    }
    
    //touch.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", c2);
    //touch.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", c2);


    void fill()
    {
        if (bucket.name == "Bob" || bucket.GetComponent<Mouse>().value <= 1)
        {
            tempc = pairs[0].Value;
            //col.Remove(col[0]);
            temp = gameObject.transform.GetChild(0).transform.localScale;
            temp2 = new Vector3(-0.1f, 0.5f, pairs[0].Key);
            gameObject.transform.GetChild(0).transform.localScale = Vector3.LerpUnclamped(temp, temp2, 0.05f);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", tempc);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", tempc);
        } else
        {
            temp1 = new Vector3(-0.1f, 0.5f, 0);
            gameObject.transform.GetChild(0).transform.localScale = temp1;
            gameObject.transform.GetChild(1).transform.localScale = temp1;
            gameObject.transform.GetChild(2).transform.localScale = temp1;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }
    }

    void fill1()
    {
        if (bucket.name == "Bob" || bucket.GetComponent<Mouse>().value <= 1)
        {
            tempc = pairs[1].Value;
            //col.Remove(col[1]);
            temp = gameObject.transform.GetChild(1).transform.localScale;
            temp2 = new Vector3(-0.1f, 0.5f, pairs[1].Key + pairs[0].Key);
            gameObject.transform.GetChild(1).transform.localScale = Vector3.LerpUnclamped(temp, temp2, 0.05f);
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", tempc);
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", tempc);
        } else
        {
            temp1 = new Vector3(-0.1f, 0.5f, 0);
            gameObject.transform.GetChild(0).transform.localScale = temp1;
            gameObject.transform.GetChild(1).transform.localScale = temp1;
            gameObject.transform.GetChild(2).transform.localScale = temp1;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }

    }

    void fill2()
    {
        if (bucket.name == "Bob" || bucket.GetComponent<Mouse>().value <= 1)
        {
            tempc = pairs[2].Value;
            //col.Remove(col[2]);
            temp = gameObject.transform.GetChild(2).transform.localScale;
            temp2 = new Vector3(-0.1f, 0.5f, pairs[2].Key + pairs[1].Key + pairs[0].Key);
            gameObject.transform.GetChild(2).transform.localScale = Vector3.LerpUnclamped(temp, temp2, 0.05f);
            gameObject.transform.GetChild(2).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", tempc);
            gameObject.transform.GetChild(2).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", tempc);
        } else
        {
            temp1 = new Vector3(-0.1f, 0.5f, 0);
            gameObject.transform.GetChild(0).transform.localScale = temp1;
            gameObject.transform.GetChild(1).transform.localScale = temp1;
            gameObject.transform.GetChild(2).transform.localScale = temp1;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }
    }

    Color getC(float a)
    {
        c = new Color32(0, 0, 0, 0);
        if (a == r)
        {
            c = new Color32(255, 0, 0, 255);
            return c;
        } else if (a == b)
        {
            c = new Color32(0, 124, 247, 255);
            return c;
        } else if (a == y) {
            c = new Color32(255, 237, 0, 255);
            return c;
        }
        c = new Color32(0, 0, 0, 0);
        return c;
    }
}
