using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField]
    protected float movSpeed;
    public GameObject Target { get; set; }
    public Animator EnemyAnimator { get; set; }
    public float EnemyHealth { get; set; }
    [SerializeField]
    private Transform[] patrolPoints;
    private Rigidbody2D enemyRb;
    private float idleWaitTime;
    [SerializeField]
    private float startIdleWaitTime;
    private int stateIndex;
    private int rand;
    private bool facingRight;
    private bool isAttacked;
    private bool isAttackArea;
    private GameObject enemyTarget;
    private float xAxis;
    [SerializeField]
    private float raycastDistance;
    private RaycastHit2D hitInfo;
    private bool isEnemyDead=false;


    private void Awake()
    {
       // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    public virtual void Start () {
        Physics2D.queriesStartInColliders = false;
        facingRight = true;
        xAxis = transform.position.x;
        EnemyHealth = 5;
        rand = Random.Range(0,patrolPoints.Length);
        idleWaitTime = startIdleWaitTime;
       stateIndex=IdleState();
       EnemyAnimator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
       enemyRb = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody2D>();
    }
    public virtual void Update () {
        isTargetDead();
        ChangeEnemyState(stateIndex);
	}
    
    public int IdleState()//Idle State 0
    {

        if (idleWaitTime<=0)
        {
            idleWaitTime = startIdleWaitTime;
            stateIndex = 1;
            movSpeed = 5;
            EnemyAnimator.SetBool("patrol",true);
            EnemyAnimator.SetBool("idle",false);
            
        }
        else
        {       
                movSpeed = 0;
                stateIndex = 0;
                idleWaitTime -= Time.deltaTime;        
           
        }
      
        return 0;

    }
    public int PatrolState()//Patrol State 1
    {
        if (!isEnemyDead)
        {

        
        transform.position = Vector2.MoveTowards(transform.position,patrolPoints[rand].position,movSpeed*Time.deltaTime);
        if (Vector2.Distance(transform.position,patrolPoints[rand].position)<=0.02f)
        {
            xAxis = transform.position.x;
            rand = Random.Range(0, patrolPoints.Length);
            stateIndex = 0;
            EnemyAnimator.SetBool("patrol", false);
            EnemyAnimator.SetBool("idle", true);            
        }
        else
        stateIndex = 1;
       }
        return 1;
    }
    public int AttackState()//Attack State 2
    {
        if (!isEnemyDead)
        {
        
        if (isAttacked)
        {
            EnemyAnimator.SetBool("attack",true);
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, movSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position,enemyTarget.transform.position)<=1.8f)
            {
                if (hitInfo.collider==null)
                {
                    EnemyAnimator.SetBool("attack", false);
                    EnemyAnimator.SetBool("patrol", true);
                    EnemyAnimator.SetBool("idle", false);
                    isAttacked = false;
                    stateIndex = 1;
                    return 1;
                }
                else if (hitInfo.collider.tag=="Player")
                {
                    isAttackArea = true;
                    isAttacked = false;
                }
           
            }
        }
        if (isAttackArea)
        {
            if (hitInfo.collider!=null)
            {
            if (hitInfo.collider.tag=="Player")
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, movSpeed * Time.deltaTime);
            }
            }
            else if (hitInfo.collider==null)
            {
                //Debug.Log("AREA");
                EnemyAnimator.SetBool("attack", false);
                EnemyAnimator.SetBool("patrol", true);
                EnemyAnimator.SetBool("idle", false);
                isAttackArea = false;
                stateIndex = 1;
                return 1;
            }
           
            
        }
        }

        stateIndex = 2;
        return 2;


    }

    public void ChangeEnemyState(int index)
    {
        if (facingRight)
        {
            hitInfo = Physics2D.Raycast(transform.position, transform.right, raycastDistance);
            Debug.DrawLine(transform.position, transform.position + transform.right * raycastDistance, Color.red);
        }
        else
        {            
            hitInfo = Physics2D.Raycast(transform.position, transform.right, -raycastDistance);
            Debug.DrawLine(transform.position, transform.position + transform.right * -raycastDistance, Color.red);
        }
        if (hitInfo.collider!=null && hitInfo.collider.tag=="Player")
        {
            enemyTarget = GameObject.FindGameObjectWithTag("Player");
            movSpeed = 5;
            EnemyAnimator.SetBool("attack",true);
            EnemyAnimator.SetBool("idle", false);
            EnemyAnimator.SetBool("patrol", false);
            index = 2;
            isAttackArea = true;
        }

    
        if (index == 0)
        {
            IdleState();
        }
        else if (index == 1)
        {
            PatrolState();
        }
        else if (index == 2)
        {
            AttackState();
        }
       

    }
    public void TakeDamage()
    {
        if (EnemyHealth>0)
        {
            EnemyHealth--;            
        }
        else if (EnemyHealth<=1)
        {
            isEnemyDead = true;
            EnemyAnimator.SetBool("dead", true);
            EnemyAnimator.SetBool("idle", false);
            EnemyAnimator.SetBool("patrol", false);
            EnemyAnimator.SetBool("attack", false);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());         
            movSpeed = 0;
            enemyTarget = null;
            raycastDistance = 0;
        }       
        
    }



    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Bullet")
        {            
                TakeDamage();
                stateIndex = 2;
                isAttacked = true;
                enemyTarget = GameObject.FindGameObjectWithTag("Player");
                movSpeed = 8;
        }
        if (other.tag=="Sword")
        {         
                TakeDamage();
                Debug.Log(EnemyHealth);
                stateIndex = 2;
                isAttacked = true;
                enemyTarget = GameObject.FindGameObjectWithTag("Player");
                movSpeed = 8;
            
        }  
    }

    public void ChangeDirection()
    {
        
        if (xAxis>transform.position.x)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1,1,1);           
        }
        else if (xAxis < transform.position.x)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void isTargetDead()
    {
        if (Player.Instance!=null)
        {
            if (Player.Instance.Health <= 0)
            {
                enemyTarget = null;
            }
        }
    
    }

}
