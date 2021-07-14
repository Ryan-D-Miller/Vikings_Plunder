using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {


    

    [SerializeField]
    protected Transform ThrowPosition; // the empty game object for the position that an item will be thrown from will come from here
    [SerializeField]
    protected GameObject axePrefab;
    [SerializeField]
    protected float HorzMovement;

    [SerializeField]
    protected Stat healthStat;
    [SerializeField]
    private EdgeCollider2D MeleeCollider;
    [SerializeField]
    private List<string> damageSources;

    public abstract bool isDead { get;}
    public bool facingRight { get; set; }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimator { get; private set; }
    // Use this for initialization
    public virtual void Start()
    {
        facingRight = true;

        MyAnimator = GetComponent<Animator>();
        healthStat.Initilize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract IEnumerator TakeDamage(int damageAmount);

    public abstract void Death();

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // when change direction is called 

    }
    public void ThrowWeapon(int value) // allows for the player script to override this script and use its one ThrowWeapon function
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
            tmp.GetComponent<ThrowWeapon>().Initialized(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(axePrefab, ThrowPosition.position, Quaternion.identity);
            tmp.GetComponent<ThrowWeapon>().Initialized(Vector2.left);
        }
    }

	public IEnumerator MeleeAttack()
    {
		MeleeCollider.enabled = true;//!MeleeCollider.enabled;
        Vector3 tmpPos = MeleeCollider.transform.position;
        MeleeCollider.transform.position = new Vector3(MeleeCollider.transform.position.x + 0, 01, MeleeCollider.transform.position.y);
        MeleeCollider.transform.position = tmpPos;// this block of code enables the melee colider and also slightly vibrates the collider enough so if both characters are standing still it will still hit
		yield return new WaitForSeconds(.5f);
		MeleeCollider.enabled = false;
    }



    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))//damage sources is an array list that contains string names for source of dmg a character can take these can be set individually from the inspector
        {
            int damageAmount = 5;
            if(other.tag == "ThrownWeapon" || other.tag =="enemyThrownWeapon")
            {
                damageAmount = 5;
            }
            else if(other.tag == "MeleeAxe" || other.tag == "EnemyAxeMelee")
            {
                damageAmount = 10;
            }
            else if(other.tag == "BossSlam")
            {
                damageAmount = 30;
            }
            StartCoroutine(TakeDamage(damageAmount));
        }
    }
}