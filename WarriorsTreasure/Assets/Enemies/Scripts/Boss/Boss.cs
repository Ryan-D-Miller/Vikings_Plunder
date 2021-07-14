using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character {
    private IBossState currentState;

    public GameObject Target { get; set; }

    public float AttackTimer { get; set; }
    public float AttackDuration { get; set;}

    public float meleeRange;
    public float meleeHorzMovement;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    private Canvas healthCanvas;
    public AudioClip DeadClip;
    public AudioSource EnemyAudioSource;
    public BoxCollider2D Exit;
    public SpriteRenderer blocked;
    private BoxCollider2D myBoxCollider;
    private Rigidbody2D myRigidBody;

    public AudioSource BossAudioSource;
    public AudioClip HurtClip;

    // Use this for initialization
    public override void Start()
    {
        base.Start();// calls the base start function from the character class


       ChangeState(new BossIdle());//calls the changeState function and changes the new state to an IdleState

        healthCanvas = transform.GetComponentInChildren<Canvas>();
        healthCanvas.enabled = false;
        AttackDuration = Random.Range(10f, 20f);
        myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        AttackTimer += Time.deltaTime;
        if (!isDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute(); // the execute function is called in whatever current class is set by currentState I.E. at the start that basically reads IdleState.Execute();
            }
            //LookAtTarget();
        }
        if(isDead)
        {
            MyAnimator.SetTrigger("die");
        }

    }
    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (HorzMovement * Time.deltaTime));
            }
            else if (currentState is BossPatrol)
            {
                ChangeDirection();
            }
        }
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left; // a short hand for an if statement that says if facing right is true return right and if it is false return left
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
        if (other.tag == "FallingRock")
        {
            int dmgAmount = 10;
            StartCoroutine(TakeDamage(dmgAmount));
        }
    }
    /*private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDIr = Target.transform.position.x - transform.position.x; // finds the x position of the target and subtracts it from the x position of the enemys position which gives you a number either larger or smalle than 0

            if (xDIr < 0 && facingRight || xDIr > 0 && !facingRight) // if xDirection is smaller than 0 and the enemy is facing right or it is larger than 0 and im facing left then change the direction
            {
                ChangeDirection();
            }
        }
    }*/
    public void ChangeState(IBossState newState)
    {
        if (currentState != null)//sees if the enemy has a current state and if it does it exits the state
        {
            currentState.Exit();
        }

        currentState = newState; //changes the current state to the new state

        currentState.Enter(this);//then uses the enter function to enter the new state and is given this enemy as its paramater 
    }
    public override IEnumerator TakeDamage(int damageAmount)
    {
        if (!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }
        healthStat.CurrentVal -= damageAmount;// the enemy loses health equal to the type of weapon
        Debug.Log("The Bosses health is " + healthStat.CurrentVal);
        if (!isDead)
        {
            BossAudioSource.clip = HurtClip;
            BossAudioSource.Play();
            MyAnimator.SetTrigger("damage");// if isDead is false you go into the take dmg function each time you take dmg
        }
        else
        { 
            int pickDrop = Random.Range(0, 100);
            if (pickDrop <= 5)
            {
                GameObject coin = (GameObject)Instantiate(DropManager.Instance.CointPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else
            {
                GameObject chest = (GameObject)Instantiate(DropManager.Instance.TreasureChestPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(chest.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            MyAnimator.SetTrigger("die");// if isDead is true the enemy dies
            EnemyAudioSource.clip = DeadClip;
            EnemyAudioSource.Play();
            myRigidBody.isKinematic = true;
            myBoxCollider.enabled = false;
            blocked.enabled = false;
            Exit.enabled = true;
            yield return null;// returns nothing to make the function for the IEnumerator happy this will be different for the player
        }
    }
    public override bool isDead
    {
        get
        {
            return healthStat.CurrentVal <= 0;//returns isDead as true if the enemy has 0 or less health
        }
    }
    public override void Death()
    {


        Destroy(gameObject);

    }

}
