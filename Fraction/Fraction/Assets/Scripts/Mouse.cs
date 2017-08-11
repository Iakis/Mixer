using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Mouse : MonoBehaviour {

    public AudioClip smix;
    public AudioClip drop;
    AudioSource audioSource;

    public float value;
    public float value2;
    public float fade;
    private int curr;
    public float fillTo;

    public float red;
    public float blue;
    public float yellow;

    private Vector3 temp;
    private Vector3 temp2;
    

    public Color colour;
    public Color colour2;
    public Color c;

    private bool make;
    private bool mixed;
    public bool tut;
    public GameObject touch;

    public GameObject bucket;
    public GameObject spawn;
    public GameObject bob;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        spawn = GameObject.Find("Spawner");
        bob = GameObject.Find("Bobs");
        mixed = false;
        init();
    }

    void init()
    {
        make = false;
        fillTo = value;
        fade = 0;
        touch = null;
        if (value % 1 > 0)
        {
            fillTo -= Mathf.Floor(value);
        }else
        {
            fillTo = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (value > 1)
        {
            fillTo = -1;
        }
        fill();
        if (touch != null)
        {
            Dump();
        }
    }

    void OnTriggerEnter (Collider col)
    {
        audioSource.PlayOneShot(smix, 0.8F);
        if (gameObject.GetComponent<BoxCollider>().isTrigger == true)
        {
            if (col.gameObject.GetComponent<Mouse>().value >= 1)
            {
                touch = null;
            }
            else {
                
                touch = col.gameObject;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                col.gameObject.GetComponent<BoxCollider>().enabled = false;
                
                
            }
            //savedPos = touch.transform.position;
        }
        
        
        
        
    }

    void Dump()
    {
        if (touch != null)
        {
            if (gameObject.GetComponent<Move>().mov == false && touch.GetComponent<Move>().mov == false)
            {
                if (touch.GetComponent<Mouse>().make == false && this.make == false)
                {
                    make = true;
                }
                value2 = touch.GetComponent<Mouse>().value;
                colour2 = touch.GetComponent<Mouse>().colour;
                Destroy(gameObject, 0.5f);
                //Destroy(touch, 0.5f);
                combine();
                
            }
        }
    }

    void combine() {
        transform.position = Vector3.LerpUnclamped(transform.position, touch.transform.position, 0.15f);
    }

    void OnDestroy()
    {
        if (make == true)
        {
            //GameObject clone = Instantiate(bucket, gameObject.transform.position, Quaternion.identity);
            touch.GetComponent<BoxCollider>().enabled = true;
            touch.GetComponent<BoxCollider>().isTrigger = false;
            touch.GetComponent<Move>().cont = false;
            touch.GetComponent<Move>().mov = false;
            touch.GetComponent<Mouse>().value = this.value + this.value2;
            touch.GetComponent<Mouse>().red = touch.GetComponent<Mouse>().red + this.red;
            touch.GetComponent<Mouse>().blue = touch.GetComponent<Mouse>().blue + this.blue;
            touch.GetComponent<Mouse>().yellow = touch.GetComponent<Mouse>().yellow + this.yellow;
            float r = touch.GetComponent<Mouse>().red;
            float b = touch.GetComponent<Mouse>().blue;
            float y = touch.GetComponent<Mouse>().yellow;
            c = mix(r, b, y, touch);
            touch.GetComponent<Mouse>().init();

            Color tempc;
            if (value >= value2)
            {
                tempc = colour;
            } else
            {
                tempc = colour2;
            }
            
            touch.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", tempc);
            touch.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", tempc);
            touch.GetComponent<Mouse>().colour = c;
            
            touch.GetComponent<Mouse>().colour2 = colour2;
            touch.GetComponent<Mouse>().c = c;
            touch.GetComponent<Mouse>().mixed = true;
            touch.GetComponent<Mouse>().fill();
            touch.SetActive(true);
            touch.GetComponent<Mouse>().audioSource.PlayOneShot(drop, 0.8F);
            touch.GetComponent<MatChange>().init();
            if (!tut)
            {
                GameObject.Find("Comp").GetComponent<Comp>().bucket = touch;
                GameObject.Find("Comp").GetComponent<Comp>().init();
            }
            


            //touch.name = "Bucket";
            //touch.SetActive(true);
            if (!tut)
            {
                if (spawn)
                {
                    spawn.GetComponent<Spawn>().current -= 1;
                    int index = spawn.GetComponent<Spawn>().buckets.IndexOf(this.gameObject);
                    spawn.GetComponent<Spawn>().buckets.Remove(this.gameObject);
                    spawn.GetComponent<Spawn>().cVals.RemoveAt(index);
                    spawn.GetComponent<Spawn>().buckets.TrimExcess();
                    spawn.GetComponent<Spawn>().cVals.TrimExcess();

                }
                
            }
        }
    }

    void fill()
    {
        if (value > 1)
        {
            fillTo -= Mathf.Floor(value);
        }
        temp = gameObject.transform.GetChild(1).transform.localScale;
        temp2 = new Vector3(-0.1f, 0.5f, fillTo);
        gameObject.transform.GetChild(1).transform.localScale = Vector3.LerpUnclamped(temp, temp2, 0.02f);
        if (mixed == true)
        {
            Color tc = Color.Lerp(colour2, c, fade);
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", tc);
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", tc);
            if (fade < 1)
            {
                fade += Time.deltaTime / 1f;
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
}
