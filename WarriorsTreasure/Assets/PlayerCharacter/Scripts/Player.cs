using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{


    private static Player instance;

    public static Player Instance // creating a singleton Instance of the player so everything can be accessed within this code 
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
                
               // DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    public override bool isDead
    {
        get
        {
            return GameManager.Instance.PlayerHealth <= 0;
            //return healthStat.CurrentVal <= 0;// return the isDead variable as true if the player has less 
        }
    }

    public int fallDamage;

    private bool immortal = false; // a bool to check and set the player to no being able to take damage
    [SerializeField]
    private float immortalTime;//how long the immortality lasts
    public float VertMovement;
    // variable to determine how far the charcter will move left right and up
    
	//a varible to see if an axe is already been thrown
	public bool axeHasHit {get; set;}
    public Transform[] groundPoints;
    // an array of transform objects that are childs of the character at his feet to check if he is touching the ground
    public Transform centerGroundPoint;
    public float groundRadius;
    // a varible to decided how close to the ground the character has to be to jump
    
    public LayerMask groundLayers;
    //a variable to decide what is a ground layer 
    private Vector3 lastLandedSpace;
    private Vector3 spawnPos;
    public BoxCollider2D col;
    //unknown why i have this in here

    public bool airControl;
    //bool to determine if you are jump attacking

    private SpriteRenderer spriteRenderer;
    
    //varible for the animator
    //private bool attack;
    public Rigidbody2D MyRigidBody { get; set; }
    public int MyPropery { get; set;}
    // the characters rigid body

    public bool Jump { get; set; }

    public bool OnGround { get; set;}

    public float Health { get; set; }
    
    
    
    public Rigidbody2D axeHitting;
    // varibles used for the hook shot
    // public GameObject hookShotPrefab;
    public AudioClip HurtClip;
    public AudioSource PlayerAudioSource;
    public AudioClip DeadClip;
    public AudioClip coinPickUpClip;

    Scene scene;


    void Awake()
    {
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            //Destroy(gameObject);
        }
    }


    public override void Start()
    {
        base.Start();
        MyRigidBody = GetComponent<Rigidbody2D>();
        //setting the rigidBody as the player game object
        col = GetComponent<BoxCollider2D>();
        //setting the collider variable as the players collider
        spriteRenderer = GetComponent<SpriteRenderer>();
        //gets the spriteReneder this script is attached to
        lastLandedSpace = transform.position;
        spawnPos = transform.position;
        //healthStat.CurrentVal = GameManager.Instance.PlayerHealth;
        scene = SceneManager.GetActiveScene();




    }
    void Update()
    {
        if(!TakingDamage && !isDead)
        {
            if(transform.position.y <= -30f)
            {

               StartCoroutine(TakeDamage(fallDamage));
                MyRigidBody.velocity = Vector2.zero;
                transform.position = lastLandedSpace;
            }
            HandleInput();//calls handleInput every frame
        }
        


    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(!TakingDamage && !isDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            // Input.GetAxis has pre set controls set up in Unity and returns them as a postive or a negative float between -1 and 1 
            //for left on Horizontal it return a negative and for right it returns a postive 
            //if not being pressed horizontal becomes 0
            float climb = Input.GetAxis("Vertical");
            OnGround = isGrounded();// sets the varible for grounded afters isGround is run
            HandleMovement(horizontal);
            Flip(horizontal);


            HandleLayers();
        }

        
     }
    //a function to check if the player is touching the ground
    private bool isGrounded()
    {
        int numberPointsOnGround = 0;
        //checks to see if the player is falling or nor moving vertically
        if (MyRigidBody.velocity.y <= 0)
        {
            //checks all three grounded points on the player
            foreach (Transform point in groundPoints)
            {
                
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundLayers);
                //looks at the physics engine to see if any part of the empty grounded objects is touching the ground
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        numberPointsOnGround += 1;
                    }
                }
            }
        }

        if(numberPointsOnGround >= 1)
        {
            if (numberPointsOnGround >= 3)
            {
                lastLandedSpace = transform.position;
                lastLandedSpace.y += 1f;
            }
            return true;
        }
        return false;
    }

    private void HandleMovement(float horizontal)//a function to move the charcter and change to move animation
    {
        if(MyRigidBody.velocity.y < 0) // if the player is not moving vertically will set the land boolean in the animator to true
        {
            MyAnimator.SetBool("land", true);
        }
        if(!Attack &&(OnGround || airControl)) // allows the player to move
        {
            MyRigidBody.velocity = new Vector2(horizontal * HorzMovement, MyRigidBody.velocity.y);
        }
        if(Jump && MyRigidBody.velocity.y == 0) // if the jump key is pressed and the player is not moving the player can jump
        {
            MyRigidBody.AddForce(new Vector2(0, VertMovement));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void HandleInput()//a function to handle jumping attacking and other input that is nor moving left and right
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            MyAnimator.SetTrigger("attack");//if the space baris pressed the attack trigger is triggered
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump"); // if the spacer bar is pressed the jump trigger is triggered
        }
        // want to change this eventually so you switch between throwing and attacking
        if(Input.GetKeyDown(KeyCode.E)) // if the left Control key is pressed the character will throw
        {
            MyAnimator.SetTrigger("throw");
        }
        

    }
    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private void HandleLayers()// a function to switch between animation layers
    {
        if(!OnGround)// if the player is not on the ground sets the layer wieght of layer 1 (which is the in airLayer) to weight 1
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else// else it sets the layer weight of layer 1 (which is the inAirLayer) to 0
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public void ThrowAxe(int value)// Thrown Axe is calle through and animation event and the value determines 
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            if (facingRight)// if the player is facing right will go thru this line of code to see the direction the axe should be thrown
            {
                
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))// first sees if the player is holding the right and up arrow key to throw the axe diag to the top right
                {
                    
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(1, 1));
                }
               /* else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))// sees if player is holding up and left to throw in that diection
                {
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(-1, 1));
                }*/
                else if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))// then sees if the player is holding down and right to throw the axe down to the right
                {
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(1, -1));
                }
                else if (Input.GetKey(KeyCode.UpArrow))// if the player is holding up will throw the axe upwards
                {
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(Vector2.up);
 
                }
                else// else just throws the axe to the right
                {
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(Vector2.right);
                }
            }
            else
            {
                
                if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))// sees if player is holding up and left to throw in that diection
                {
                    
					GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(-1, 1));
                }
               /* else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))// first sees if the player is holding the right and up arrow key to throw the axe diag to the top right
                {
                    GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(1, 1));
                }*/
                else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))// sees if the player is holding down and left and throws in that direction
                {
					GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(new Vector2(-1, -1));
                }
                else if (Input.GetKey(KeyCode.UpArrow))// if the player is just holding up throws the axe up
                {
					GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(Vector2.up);
                }
                else// else throws the axe to the left
                {
					GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
                    tmp.GetComponent<ThrownAxe>().Initialized(Vector2.left);
                }
            }
        }

    }

    private IEnumerator IndicateImmortal()
    {
        while(immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);

        }
    }

    public override IEnumerator TakeDamage(int damageAmount)
    {
        if(!immortal)
        {
            //healthStat.CurrentVal -= damageAmount;
            // GameManager.Instance.PlayerHealth = healthStat.CurrentVal;
            GameManager.Instance.PlayerHealth -= damageAmount;
            //Debug.Log("Players health is " + healthStat.CurrentVal);
            Debug.Log("The Players health is " + GameManager.Instance.PlayerHealth);
            if (!isDead)
            {
                MyAnimator.SetTrigger("damage");
                PlayerAudioSource.clip = HurtClip;
                PlayerAudioSource.Play();
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                PlayerAudioSource.clip = DeadClip;
                PlayerAudioSource.Play();
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }
        }
    }

    public override void Death()// a function that runs after the player dies probably going to need some tweeks to get what i want in the end
        //for now it sets the players health back to the health he had when the game started and spawns him at the location he started when the game was first played
    {
        MyRigidBody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        GameManager.Instance.PlayerHealth = 50;
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
		Cursor.visible = true;
        //healthStat.CurrentVal = healthStat.MaxVal;
        //GameManager.Instance.PlayerHealth = healthStat.CurrentVal;
        //transform.position = spawnPos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "movingPlatform")//makes the player a child object of the moving platfor allowing him to jump of it
        {
            transform.parent = other.transform;
        }
        if (other.gameObject.tag == "Treasure")
        {
            GameManager.Instance.CollectTreasure += 500;
            PlayerAudioSource.clip = coinPickUpClip;
            PlayerAudioSource.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Coin")
        {
            GameManager.Instance.CollectTreasure += 100;
            PlayerAudioSource.clip = coinPickUpClip;
            PlayerAudioSource.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "HealthPotion")
        {
            //healthStat.CurrentVal += 25;
            //GameManager.Instance.PlayerHealth = healthStat.CurrentVal;
            GameManager.Instance.PlayerHealth += 25;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            int dmgAmount = 10;
            StartCoroutine(TakeDamage(dmgAmount));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "movingPlatform")//makes the player no longer a child of the moving platforms once he exits the collision
        {
            transform.parent = null;
        }
   
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        /*if(other.gameObject.tag == "Entrance")// when the player enters one of the entrance or exits currently the game pauses
        {
            Time.timeScale = 0f;
        }*/
    }
}

