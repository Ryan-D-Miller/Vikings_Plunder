using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character // the enemy class inherits from the character class and the character class inherits from the monobehavior
{

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    public float meleeRange;
    public float throwRange;
    public float meleeHorzMovement;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    private Canvas healthCanvas;
    public AudioClip DeadClip;
    public AudioSource EnemyAudioSource;
    public AudioClip bowFire;

    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }
    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;
        }
    }

    public override bool isDead
    {
        get
        {
            return healthStat.CurrentVal <= 0;//returns isDead as true if the enemy has 0 or less health
        }
    }

    // Use this for initialization
    public override void Start () // the start function needs an override to make sure that both start function for both the character script and the enemy script are called
    {
        base.Start();// calls the base start function from the character class
       

        ChangeState(new IdleState());//calls the changeState function and changes the new state to an IdleState

        healthCanvas = transform.GetComponentInChildren<Canvas>();
        healthCanvas.enabled = false;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute(); // the execute function is called in whatever current class is set by currentState I.E. at the start that basically reads IdleState.Execute();
            }
            LookAtTarget();
        }
	}

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDIr = Target.transform.position.x - transform.position.x; // finds the x position of the target and subtracts it from the x position of the enemys position which gives you a number either larger or smalle than 0

            if(xDIr < 0 && facingRight || xDIr > 0 && !facingRight) // if xDirection is smaller than 0 and the enemy is facing right or it is larger than 0 and im facing left then change the direction
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)//sees if the enemy has a current state and if it does it exits the state
        {
            currentState.Exit();
        }

        currentState = newState; //changes the current state to the new state

        currentState.Enter(this);//then uses the enter function to enter the new state and is given this enemy as its paramater 
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
            else if (currentState is PatrolState)
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
    }
    public void MeleeMove()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))//code to check if the enemy is at the edge of tha platform and facing off the edge and stops him from running of the edge
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (meleeHorzMovement * Time.deltaTime));
            }
            else if(currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
    }

    public override IEnumerator TakeDamage(int damageAmount)
    {
        if(!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }
        healthStat.CurrentVal -= damageAmount;// the enemy loses health equal to the type of weapon
        Debug.Log("The enemies health is " + healthStat.CurrentVal);
        if(!isDead)
        {
            MyAnimator.SetTrigger("damage");// if isDead is false you go into the take dmg function each time you take dmg
        }
        else
        {
            
            int pickDrop = Random.Range(0, 100);
            if(pickDrop <= 60)
            {
                GameObject coin = (GameObject)Instantiate(DropManager.Instance.CointPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else if(pickDrop <= 80 && pickDrop > 60)
            {
                GameObject potion = (GameObject)Instantiate(DropManager.Instance.HealthPotionPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(potion.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else
            {
                GameObject chest = (GameObject)Instantiate(DropManager.Instance.TreasureChestPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(chest.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

            MyAnimator.SetTrigger("die");// if isDead is true the enemy dies
            EnemyAudioSource.clip = DeadClip;
            EnemyAudioSource.Play();
            yield return null;// returns nothing to make the function for the IEnumerator happy this will be different for the player
        }
    }

    public override void Death()
    {
        
        
        Destroy(gameObject);

    }

}
