using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public BoxCollider2D bloodTrigger;
    public Rigidbody2D body;
    public ParticleSystem splat;
    public ParticleSystem collide;
    public Animator anim;
    public AnimationClip meltAnimation;
    public AnimationClip spawnAnimation;
    private SpriteRenderer mySpriteRenderer;
    public PauseMenu pauseMenu;

    public Action looseBlood;

    Vector3 PreviousFramePosition = Vector3.zero;

    bool hSpeed, vSpeed, horizontalMovement;
    public float triggerOffsetNeg, triggerOffsetPos;
    public int speed = 1;
    private bool isSprinting;
    float mspeed = 0f;
    float lastX = 0;
    float lastY = 0;
    public bool attackActive;
    public bool started;

    void Start()
    {
        Time.timeScale = 1f;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        isSprinting = false;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("WaitForSpawnAnimation");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackActive != true && !pauseMenu.gameIsPaused)
        {
            attackActive = true;
            StartCoroutine(PauseMovement());
            
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5; 
            isSprinting = true;
            if (SoundEffects.instance != null)
            {
                SoundEffects.instance.walkSound.pitch = 3;
            }
            Debug.Log("Sprinting");
        }
        else
        {
            speed = 2; 
            isSprinting = false;
            if (SoundEffects.instance != null)
            {
                SoundEffects.instance.walkSound.pitch = 2;
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        var moveVector = new Vector3(x, y, 0);

        if (lastY < Mathf.Abs(y) || Mathf.Abs(x) == 0)
        {
            horizontalMovement = false;
        }
        if (lastX < Mathf.Abs(x) || Mathf.Abs(y) == 0)
        {
            horizontalMovement = true;
        }

        if (horizontalMovement)
        {
            moveVector.y = 0;  
        }
        else
        {
            moveVector.x = 0;
        }

        lastX = Mathf.Abs(x);
        lastY = Mathf.Abs(y);

        body.MovePosition(new Vector2((transform.position.x + moveVector.x * speed * Time.deltaTime),
                   transform.position.y + moveVector.y * speed * Time.deltaTime));
        
        if (isSprinting == true && mspeed > 4 && moveVector.magnitude < 1.1)
        {
            if (moveVector.x > 0.5 && isSprinting == true && vSpeed == false)
            {
                bloodTrigger.offset = new Vector2(triggerOffsetPos, 0f);
            }
            else if (moveVector.x < -0.5 && isSprinting == true && vSpeed == false)
            {
                bloodTrigger.offset = new Vector2(triggerOffsetNeg, 0f);
            }
            else if (moveVector.y > 0.5 && isSprinting == true && hSpeed == false)
            {
                bloodTrigger.offset = new Vector2(0f, triggerOffsetPos);
            }
            else if (moveVector.y < -0.5 && isSprinting == true && hSpeed == false)
            {
                bloodTrigger.offset = new Vector2(0f, triggerOffsetNeg);
            }
            else
            {
                bloodTrigger.offset = new Vector2(0f, 0f);
            }
        }

        else
        {
            bloodTrigger.offset = new Vector2(0f, 0f);
        }
        if (horizontalMovement)
        {
            hSpeed = true;
            vSpeed = false;
        }
        else
        {
            hSpeed = false;
            vSpeed = true;
        }

        if (x > 0 && mspeed > 1 && hSpeed == true)
        {
            anim.SetInteger("Right", 1);
            mySpriteRenderer.flipX = true;
            collide.transform.eulerAngles = new Vector3(0, 0, 90);
            Debug.Log("Moving right");
        }
        else if (x < 0 && mspeed > 1 && hSpeed == true)
        {
            anim.SetInteger("Right", -1);
            mySpriteRenderer.flipX = false;
            collide.transform.eulerAngles = new Vector3(0, 0, 270);
            Debug.Log("Moving left");
        }
        else
        {
            anim.SetInteger("Right", 0);
        }

        if (y > 0 && mspeed > 1 && vSpeed == true && hSpeed == false)
        {
            anim.SetInteger("Up", 1);
            collide.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (y < 0 && mspeed > 1 && vSpeed == true && hSpeed == false)
        {
            anim.SetInteger("Up", -1);
            collide.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            anim.SetInteger("Up", 0);
        }

        if (x == 0 && y == 0)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
            if (SoundEffects.instance != null)
                SoundEffects.instance.WalkSound();
        }
        
        if (mspeed > 0)
        {
            anim.SetBool("Moving", true);
            if (SoundEffects.instance != null)
                SoundEffects.instance.WalkSound();
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        float movementPerFrame = Vector3.Distance(PreviousFramePosition, transform.position);
        mspeed = movementPerFrame / Time.deltaTime;
        PreviousFramePosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blood" && isSprinting == true && mspeed > 2)
        {
            if (SoundEffects.instance != null)
                SoundEffects.instance.CollisionSound();
            if(!collide.isPlaying)
                collide.Play();
            looseBlood.Invoke();
            anim.SetTrigger("Collision");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (other.gameObject.tag == "Blood" && attackActive == true)
        {
            if (SoundEffects.instance != null)
                SoundEffects.instance.Attacksound();
            if (!splat.isPlaying)
                splat.Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    IEnumerator PauseMovement()
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.6f);
        bloodTrigger.size = new Vector2(2.5f, 2.5f);
        

        yield return new WaitForSeconds(0.2f);
        bloodTrigger.size = new Vector2(0.61f, 0.61f);
        looseBlood.Invoke();
        attackActive = false;

        yield return new WaitForSeconds(0.1f);
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator WaitForSpawnAnimation()
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetBool("Started", true);

        yield return new WaitForSeconds(spawnAnimation.length);
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        started = true;
    }
}
