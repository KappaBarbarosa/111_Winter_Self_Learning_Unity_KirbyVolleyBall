using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float movespeed;
    [SerializeField] bool isgrounded;
    [SerializeField] bool isleft,isright;
    [SerializeField] float jumpower;
    [SerializeField] bool ispush;
    [SerializeField] int pushcount;
    [SerializeField] AudioClip jumpeffect;
    [SerializeField] AudioClip pusheffect;
    [SerializeField] AudioClip spikeffect;
    AudioSource audiosource;
    string name;
    Rigidbody2D cocobody;

    //[SerializeField] bool ispowerspike;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        movespeed = 5f;
        jumpower = 20000f;
        isgrounded = true;
        isleft = false;
        isright = false;
        ispush = false;
        pushcount = 0;
        audiosource = GetComponent<AudioSource>();
        name = gameObject.tag;
        //dispowerspike = false;
        cocobody = this.GetComponent<Rigidbody2D>();
    }
    bool playerRight(string name)
    {
        return (Input.GetKey(KeyCode.D) && name == "1P") || (Input.GetKey(KeyCode.RightArrow) && name == "2P");
    }
    bool playerLeft(string name)
    {
        return (Input.GetKey(KeyCode.A) && name == "1P") || (Input.GetKey(KeyCode.LeftArrow) && name == "2P");
    }
    bool playerUp(string name)
    {
        return (Input.GetKey(KeyCode.Space) && name == "1P") || (Input.GetKey(KeyCode.UpArrow) && name == "2P");
    }
    bool playerSpike(string name)
    {
        return (Input.GetKey(KeyCode.U) && name == "1P") || (Input.GetKey(KeyCode.RightShift) && name == "2P");
    }
    // Update is called once per frame
    void Update()
    {
        if (ispush)
        {
            pushcount++;
            if (pushcount == 45)
            {
                ispush = false;
                pushcount = 0;
                GetComponent<Animator>().SetBool("push", false);
            }
        }
        else
        {
            if (playerRight(name) && !isleft)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                isright = false;
                transform.Translate(movespeed * Time.deltaTime, 0, 0);
                if (playerSpike(gameObject.tag) && isgrounded && !ispush)
                {
                    ispush = true;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpower , 0));
                    GetComponent<Animator>().SetBool("push", true);
                    audiosource.PlayOneShot(pusheffect);
                    
                }
                //GetComponent<Animator>().SetBool("run", true);
            }
            else if (playerLeft(name) && !isright)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                isleft = false;
                transform.Translate(-1 * movespeed * Time.deltaTime, 0, 0);
                //GetComponent<Animator>().SetBool("run", true);
                if (playerSpike(name) && isgrounded && !ispush)
                {
                    ispush = true;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * jumpower, 0));
                    GetComponent<Animator>().SetBool("push", true);
                    audiosource.PlayOneShot(pusheffect);
                }
            }
            if (playerUp(name) && isgrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpower));
                isgrounded = false;
                GetComponent<Animator>().SetBool("jump", true);
                audiosource.PlayOneShot(jumpeffect);
            }
        }
        
        
        
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bottom")
        {
            isgrounded = true;
            GetComponent<Animator>().SetBool("jump", false);
        }
        else if (other.gameObject.tag == "ball")
        {
            cocobody.velocity = new Vector2(0, 0);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bound")
        {
            if (other.contacts[0].normal == new Vector2(1f, 0f)) isright = true;
            else if (other.contacts[0].normal == new Vector2(-1f, 0f)) isleft = true;
        }
        else if (other.gameObject.tag == "ball")
        {
            Debug.Log("碰到球!!!");
            cocobody.velocity = new Vector2(0, 0);
            if (playerSpike(name) && !isgrounded)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(-350 * other.contacts[0].normal);
                //Debug.Log(other.contacts[0].normal);
                Vector2 location = this.transform.position;
                Vector2 colsetpoint = other.collider.ClosestPoint(location);
                other.gameObject.GetComponent<Ball>().SpawnEffect(colsetpoint);
                Debug.Log("殺球!!!");
                audiosource.PlayOneShot(spikeffect);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ball")
        {
            cocobody.velocity = new Vector2(0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bound")
        {
            if (name == "1P") isleft = true;
            else if (name == "2P") isright = true;
           // Debug.Log("隱形~");
        }
    }
}
