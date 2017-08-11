using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bob : MonoBehaviour {

    List<float> val = new List<float>() {1, 2 , 3};
    List<float> gray = new List<float>() {0.25f, 0.5f, 0.25f};
    List<float> prev = new List<float>();

    public float red, blue, yellow;
    public List<float> cVals = new List<float>();
    public List<GameObject> buckets = new List<GameObject>();

    public bool tut;

    public Color co;

    public GameObject spawn;

    void Start () {
        if (!tut)
        {
            spawn = GameObject.Find("Spawner");
            
            List<float> source = new List<float>() {1, 0, 0};
            List<float> tempv = source.OrderBy(x => Random.value).ToList();
            co = mix(tempv[0], tempv[1], tempv[2]);
            red = tempv[0];
            blue = tempv[1];
            yellow = tempv[2];
            setBob(co);
        } else
        {
            co = mix(red, blue, yellow);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", mix(red, blue, yellow));
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", mix(red, blue, yellow));
        }
    }

    public void init()
    {
        cVals = spawn.GetComponent<Spawn>().cVals;
        buckets = spawn.GetComponent<Spawn>().buckets;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            bool hitinfo = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (hitinfo)
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    GameObject.Find("Comp").GetComponent<Comp>().bucket = this.gameObject;
                    GameObject.Find("Comp").GetComponent<Comp>().init();
                }
            }
        }
    }

    public void rand()
    {
        Random.InitState((int)System.DateTime.Now.Second);
        
        List<float> tempv = gen();
        co = mix(tempv[0], tempv[1], tempv[2]);
        red = tempv[0];
        blue = tempv[1];
        yellow = tempv[2];
        setBob(co);
    }

    public int check(Color color)
    {
        float a, b, c;

        if (!tut)
        {
            a = Mathf.Max(color.r, co.r) - Mathf.Min(color.r, co.r);
            b = Mathf.Max(color.g, co.g) - Mathf.Min(color.g, co.g);
            c = Mathf.Max(color.b, co.b) - Mathf.Min(color.b, co.b);
        } else
        {
            a = Mathf.Max(color.r, co.r) - Mathf.Min(color.r, co.r);
            b = Mathf.Max(color.g, co.g) - Mathf.Min(color.g, co.g);
            c = Mathf.Max(color.b, co.b) - Mathf.Min(color.b, co.b);
        }

        if (a < 0.05 && b < 0.05 && c < 0.05)
        {
            return 1;
        } else if (a < 0.15 && b < 0.15 && c < 0.15)
        {
            return 2;
        } else
        {
            return 3;
        }
    }

    List<float> gen()
    {
        init();
        Debug.Log("list");
        foreach (float f in cVals)
        {
            Debug.Log(f);
        }
        int index1 = Random.Range(0, cVals.Count);
        float a = cVals[index1];

        List<int> val2 = new List<int>();
        for (int i = 0; i < cVals.Count; i++)
        {
            if (cVals[i] <= (1f - (a + cVals[i])))
            {
                val2.Add(i);
            }
        }
        int index2 = val2[Random.Range(0, val2.Count)];
        float b = cVals[index2];

        float c = 1f - a - b;
        if (cVals.Contains(c) || c == 0)
        {
            float r1, b1, y1;
            if (c > 0)
            {
                int index3 = cVals.IndexOf(c);
                r1 = buckets[index1].GetComponent<Mouse>().red + buckets[index2].GetComponent<Mouse>().red + buckets[index3].GetComponent<Mouse>().red;
                b1 = buckets[index1].GetComponent<Mouse>().blue + buckets[index2].GetComponent<Mouse>().blue + buckets[index3].GetComponent<Mouse>().blue;
                y1 = buckets[index1].GetComponent<Mouse>().yellow + buckets[index2].GetComponent<Mouse>().yellow + buckets[index3].GetComponent<Mouse>().yellow;
            } else
            {
                r1 = buckets[index1].GetComponent<Mouse>().red + buckets[index2].GetComponent<Mouse>().red;
                b1 = buckets[index1].GetComponent<Mouse>().blue + buckets[index2].GetComponent<Mouse>().blue;
                y1 = buckets[index1].GetComponent<Mouse>().yellow + buckets[index2].GetComponent<Mouse>().yellow;
            }
            
            
            List<float> result = new List<float>() { r1, b1, y1 };
            if (result[0] == gray[0] && result[1] == gray[1] && result[2] == gray[2])
            {
                Debug.Log("GRAYY");
                return gen();
            } else if (result == prev)
            {
                Debug.Log("Prev!!");
                return gen();
            } else
            {
                prev = result;
                return result;
            }
        } else
        {
            Debug.Log("No C");
            return gen();
        }
    }

    public Color mix(float red, float blue, float yellow, GameObject touch = null)
    {
        Color co1 = new Color32(255, 0, 0, 255);
        Color co2 = new Color32(0, 124, 247, 255);
        Color co3 = new Color32(255, 237, 0, 255);

        float k1, k2, k3, k4, c1, c2, c3, c4, m1, m2, m3, m4, y1, y2, y3, y4, r, g, b;

        k1 = 1f - (Mathf.Max(co1.r, co1.g, co1.b));
        k2 = 1f - (Mathf.Max(co2.r, co2.g, co2.b));
        k3 = 1f - (Mathf.Max(co3.r, co3.g, co3.b));

        c1 = (1f - co1.r - k1) / (1f - k1);
        c2 = (1f - co2.r - k2) / (1f - k2);
        c3 = (1f - co3.r - k3) / (1f - k3);

        m1 = (1f - co1.g - k1) / (1f - k1);
        m2 = (1f - co2.g - k2) / (1f - k2);
        m3 = (1f - co3.g - k3) / (1f - k3);

        y1 = (1f - co1.b - k1) / (1f - k1);
        y2 = (1f - co2.b - k2) / (1f - k2);
        y3 = (1f - co3.b - k3) / (1f - k3);

        if (touch)
        {
            c4 = (c1 * red + c2 * blue + c3 * yellow) * 1 / touch.GetComponent<Mouse>().value;
            m4 = (m1 * red + m2 * blue + m3 * yellow) * 1 / touch.GetComponent<Mouse>().value;
            y4 = (y1 * red + y2 * blue + y3 * yellow) * 1 / touch.GetComponent<Mouse>().value;
            k4 = (k1 * red + k2 * blue + k3 * yellow) * 1 / touch.GetComponent<Mouse>().value;
        } else
        {
            c4 = (c1 * red + c2 * blue + c3 * yellow);
            m4 = (m1 * red + m2 * blue + m3 * yellow);
            y4 = (y1 * red + y2 * blue + y3 * yellow);
            k4 = (k1 * red + k2 * blue + k3 * yellow);
        }

        r = (1f - c4) * (1f - k4);
        g = (1f - m4) * (1f - k4);
        b = (1f - y4) * (1f - k4);

        Color c = new Color(r, g, b);

        return c;
    }

    void setBob(Color co)
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", co);
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", co);
    }
}
