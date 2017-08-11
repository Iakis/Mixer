using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BobBackup : MonoBehaviour
{

    //public List<Color> values = new List<Color>();
    public List<Color> Bobs = new List<Color>();
    List<float> val = new List<float>() { 0f, 0.25f, 0.5f, 0.75f, 1f };
    List<float> gray = new List<float>() { 0.25f, 0.5f, 0.25f };
    List<List<float>> comps = new List<List<float>>();

    //float r, g, b;
    public float red, blue, yellow;
    int x, y, z;

    public bool tut;

    public Color co;

    public GameObject bob1, bob2, bob3;



    // Use this for initialization
    void Start()
    {

        //Random.InitState((int)System.DateTime.Now.Second);
        //blue = new Color32(0, 124, 247, 255);
        //red = new Color32(255, 0, 0, 255);
        //yellow = new Color32(255, 237, 0, 255);
        //values.Add(red);
        //values.Add(blue);
        //values.Add(yellow);
        if (!tut)
        {
            bob1 = GameObject.Find("Bob (1)");
            bob2 = GameObject.Find("Bob (2)");
            bob3 = GameObject.Find("Bob (3)");
            List<float> tempv = new List<float>();
            tempv = gen();
            co = mix(tempv[0], tempv[1], tempv[2]);
            Bobs.Add(co);
            tempv = gen();
            co = mix(tempv[0], tempv[1], tempv[2]);
            Bobs.Add(co);
            tempv = gen();
            co = mix(tempv[0], tempv[1], tempv[2]);
            Bobs.Add(co);
            tempv = gen();
            co = mix(tempv[0], tempv[1], tempv[2]);
            Bobs.Add(co);
            red = comps[0][0];
            blue = comps[0][1];
            yellow = comps[0][2];
            setBob();
        }
        else
        {
            co = mix(red, blue, yellow);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", mix(red, blue, yellow));
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", mix(red, blue, yellow));
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        //while (values[x] == co)
        //{
        //    x = Random.Range(0, values.Count);
        //}
        List<float> tempv = gen();
        co = mix(tempv[0], tempv[1], tempv[2]);
        Bobs.Add(co);
        Bobs.Remove(Bobs[0]);
        comps.Remove(comps[0]);
        Bobs.TrimExcess();
        comps.TrimExcess();
        red = comps[0][0];
        blue = comps[0][1];
        yellow = comps[0][2];
        setBob();
    }

    public int check(Color color)
    {
        //Debug.Log("Check");
        //Debug.Log(color);
        //Debug.Log(co);
        float a, b, c;

        if (!tut)
        {
            a = Mathf.Max(color.r, Bobs[0].r) - Mathf.Min(color.r, Bobs[0].r);
            b = Mathf.Max(color.g, Bobs[0].g) - Mathf.Min(color.g, Bobs[0].g);
            c = Mathf.Max(color.b, Bobs[0].b) - Mathf.Min(color.b, Bobs[0].b);
        }
        else
        {
            a = Mathf.Max(color.r, co.r) - Mathf.Min(color.r, co.r);
            b = Mathf.Max(color.g, co.g) - Mathf.Min(color.g, co.g);
            c = Mathf.Max(color.b, co.b) - Mathf.Min(color.b, co.b);
        }

        if (a < 0.05 && b < 0.05 && c < 0.05)
        {
            return 1;
        }
        else if (a < 0.15 && b < 0.15 && c < 0.15)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    List<float> gen()
    {
        float a = val[Random.Range(0, val.Count)];
        List<float> val2 = new List<float>();
        foreach (float f in val)
        {
            if (f <= (1f - (a + f)))
            {
                val2.Add(f);
            }
        }
        float b = val2[Random.Range(0, val2.Count)];
        float c = 1f - a - b;
        List<float> source = new List<float>() { a, b, c };
        var result = source.OrderBy(x => Random.value).ToList();
        if (result[0] == gray[0] && result[1] == gray[1] && result[2] == gray[2])
        {
            Debug.Log("GRAYY");
            return gen();
        }
        else
        {
            if (comps.Contains((List<float>)result))
            {
                return gen();
            }
            else
            {
                comps.Add((List<float>)result);
                return (List<float>)result;
            }
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
        }
        else
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

    void setBob()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Bobs[0]);
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Bobs[0]);
        bob1.GetComponent<Renderer>().material.SetColor("_Color", Bobs[1]);
        bob1.GetComponent<Renderer>().material.SetColor("_EmissionColor", Bobs[1]);
        bob2.GetComponent<Renderer>().material.SetColor("_Color", Bobs[2]);
        bob2.GetComponent<Renderer>().material.SetColor("_EmissionColor", Bobs[2]);
        bob3.GetComponent<Renderer>().material.SetColor("_Color", Bobs[3]);
        bob3.GetComponent<Renderer>().material.SetColor("_EmissionColor", Bobs[3]);
    }
}
