using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float spawnTime = 0.5f;
    public GameObject bubble;
    public GameObject bob;

    private List<float> values = new List<float>();
    private List<Color32> colours = new List<Color32>();

    public List<float> cVals = new List<float>();
    public List<GameObject> buckets = new List<GameObject>();

    public int current;
    public int max;

    Vector3 rando;
    int a, b;
    float r;
    Color c;

    Color32 red, blue, yellow;

    RaycastHit hit;
    bool hitinfo;

    void Start()
    {
        bob = GameObject.Find("Bobs");
        values.Add(0.25f);
        values.Add(0.5f);
        values.Add(0.75f);
        values.Add(0.125f);

        blue = new Color32(0, 124, 247, 255);
        red = new Color32(255, 0, 0, 255);
        yellow = new Color32(255, 237, 0, 255);
        colours.Add(red);
        colours.Add(blue);
        colours.Add(yellow);

        InvokeRepeating("Spawning", 1.0f, spawnTime);
        current = 0;
        max = 10;
        Random.InitState((int) System.DateTime.Now.Second);
        QualitySettings.antiAliasing = 4;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawning()
    {
        Random.InitState((int)System.DateTime.Now.Millisecond);
        rando = new Vector3(Random.Range(-3, 3), Random.Range(-6, 5), -1);
        a = Random.Range(0, 4);
        b = Random.Range(0, 3);
        r = values[a];
        c = colours[b];
        if (current < 10) {

            hit = new RaycastHit();
            hitinfo = Physics.SphereCast(Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(rando)), 1.5f, out hit);

            if (hitinfo)
            {

                if (hit.transform.gameObject.tag == "bubble")
                {
                    while (hit.transform.gameObject.tag == "bubble")
                    {
                        rando = new Vector3(Random.Range(-3, 3), Random.Range(-6, 4), -1);
                        hit = new RaycastHit();
                        hitinfo = Physics.SphereCast(Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(rando)), 1.5f, out hit);
                    }
                }

                GameObject clone = Instantiate(bubble, rando, Quaternion.identity);
                clone.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color",c);
                clone.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
                clone.GetComponent<Mouse>().value = r;
                clone.GetComponent<Mouse>().colour = c;
                if (b == 0)
                {
                    clone.GetComponent<Mouse>().red = r;
                } else if (b == 1)
                {
                    clone.GetComponent<Mouse>().blue = r;
                }
                else if (b == 2)
                {
                    clone.GetComponent<Mouse>().yellow = r;      
                }
                clone.name = "Bucket";
                clone.SetActive(true);
                current += 1;
                cVals.Add(r);
                buckets.Add(clone);

                return;
            } else
            {
                Debug.Log("hit nothing");
            }
        } else
        {

        }
        
    }

}
