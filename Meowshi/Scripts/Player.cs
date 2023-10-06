using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AudioSource audSource;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip powerUp;
    public AudioClip shieldBreak;

    public GameObject pickupCloud;

    Animator myAnim;
    public Animator camAnim;

    public ParticleSystem pS;
    public GameObject riceTrail;

    public GameObject shield;

    private int health = 0;

    private Vector3 direction;

    public float gravity = -9.81f;

    public float strength = 5f;

    // Start is called before the first frame update
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        audSource = GetComponent<AudioSource>();
        pS = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        shield = transform.Find("shield").gameObject;
        DeactivateShield();
        myAnim.SetBool("isIdle", true);

    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
        health++;

        this.gameObject.GetComponent<Collider2D>().enabled = true;
        camAnim.SetBool("isShake", false);
        pS.Stop();
        riceTrail.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;

            //play jump sound
            audSource.PlayOneShot(jump, 2f);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        if(health == 0)
        {
            audSource.PlayOneShot(hit, 2f); //audio cue for death sound
            myAnim.SetTrigger("hitChopstick"); //change sprite to hurt animation
            FindObjectOfType<GameManager>().GameOver();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (HasShield() && health > 1)
            {
                camAnim.SetBool("isShake", true);
                audSource.PlayOneShot(shieldBreak, 2f);
                DeactivateShield();
                health = 2;
            }
            health--;
            myAnim.SetBool("isIdle", true);
        }

        if (other.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }


        //invincibility
        if (other.gameObject.CompareTag("item2"))
        {
            ActivateShield();
            health++;
        }

        //score multiplier
        if (GameManager.itemDur > 0)
        {

            trailOn();
        }

        if(GameManager.itemDur == 0)
        {

            trailOff();
        }

        if (other.gameObject.CompareTag("item") || other.gameObject.CompareTag("item2"))
        {
            //audio cue
            audSource.PlayOneShot(powerUp, 2f);

            //particle system
            Instantiate(pickupCloud, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

        }
    }

    private bool HasShield()
    {
        return shield.activeSelf;
    }
    private void ActivateShield()
    {
        myAnim.SetBool("isShield", true);
        shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        myAnim.SetBool("isShield", false);
        
        shield.SetActive(false);
    }


    private void trailOn()
    {
        riceTrail.SetActive(true);
        pS.Play();

    }

    private void trailOff()
    {
        //riceTrail.SetActive(false);
        pS.Stop();

    }

    //Used for when colliding with an item it collects it by destroing it.
    private void OnTriggerStay2D(Collider2D other)
    {


    }

}
