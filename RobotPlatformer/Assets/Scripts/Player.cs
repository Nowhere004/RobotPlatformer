using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance==null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }



    private Animator myAnimator;
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private GameObject shootPrefab;
    [SerializeField]
    private GameObject shootDestroyEffect;
    [SerializeField]
    private Transform shootPos;
    private Shake shake;
    private float shakeAmt = 0.3f;
    [SerializeField]
    private GameObject shootSound;


    [Space]
    [Header("Health System")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int numOfHearts;
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite empHeart;

    [Space]
    [Header("Immortal")]
    private bool immortal = false;
    [SerializeField]
    private float immortalTime;
    private SpriteRenderer spriteRenderer;
    private bool isDead;

    public Rigidbody2D MyRigidbody { get; set; }    
    public bool Attack { get; set; }
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    public int Health
    {
        get
        {
            return health;
        }

        private set
        {
            health = value;
        }
    }




    // Use this for initialization
    void Start () {
        
        facingRight = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        shake = GameObject.FindGameObjectWithTag("ShakeManager").GetComponent<Shake>();
        
	}
    private void Update()
    {
        StartCoroutine(IsPlayerDead());
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate () {
      
        float horizontal = Input.GetAxisRaw("Horizontal");
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        
      
        HandleLayers();
       
    }
    void LateUpdate()
    {
        HealtSystem();
    }

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y<0)
        {
            myAnimator.SetBool("land",true);
        }
        if (!Attack &&!Slide)
        {
            MyRigidbody.velocity = new Vector2(horizontal*movementSpeed*Time.fixedDeltaTime,MyRigidbody.velocity.y);
            Flip(horizontal);
        }
        if (Jump && MyRigidbody.velocity.y==0)
        {
            MyRigidbody.AddForce(new Vector2(0,jumpForce));
        }
        myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimator.SetTrigger("attack");
            
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            myAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAnimator.SetTrigger("shoot");     
        }

    }
    private void Flip(float horizontal)
    {
        if (horizontal>0 &&!facingRight || horizontal<0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f,180f,0f);
        }

    }

    
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                   
                        return true;
                    }

                }
            }
        }       
            return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1,1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
    public void ShootBullet(int value)
    {
        if (!OnGround && value==1 || OnGround && value==0)
        {
            if (facingRight)
            {
                GameObject tmp = Instantiate(shootPrefab, shootPos.position, Quaternion.identity);
                GameObject shootSoundeffect = Instantiate(shootSound,shootPos.position,Quaternion.identity);
                tmp.GetComponent<Shoot>().Initialize(Vector2.right);
                Destroy(shootSoundeffect,0.75f);
                //shake.CamShake(shakeAmt, 0.4f);
                
            }
            else
            {
                GameObject tmp = Instantiate(shootPrefab, shootPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                GameObject shootSoundeffect = Instantiate(shootSound, shootPos.position, Quaternion.identity);
                tmp.GetComponent<Shoot>().Initialize(Vector2.left);
                Destroy(shootSoundeffect, 0.75f);


            }
        }
       
    } 

    private void HealtSystem()
    {
        if (Health > numOfHearts)
        {
            Health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = empHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag=="Enemy")
        {
            StartCoroutine(TakeDamage());
            
        }
    }

    IEnumerator TakeDamage()
    {
        if (!isDead)
        {
        if (!immortal)
        {
           
            Health--;
            immortal = true;
            if (OnGround)
            {
              if (facingRight)
                MyRigidbody.AddForce(new Vector2(-700f, 200f));
              else
                MyRigidbody.AddForce(new Vector2(700f, 200f));
            }
            StartCoroutine(FlashImmortal());
            yield return new WaitForSeconds(immortalTime);
            immortal = false;
        }
        }
  
    }
    private IEnumerator FlashImmortal()
    {
        while (immortal)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(),true);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), false);
    }
    private IEnumerator IsPlayerDead()
    {
        if (Health<=0)
        {
            movementSpeed = 0;
            isDead = true;
            myAnimator.SetBool("dead", true);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), true);
            yield return new WaitForSeconds(immortalTime);
            Destroy(gameObject);

        }
        else
        {
            isDead = false;
           
        }
        yield return null;
    }
}
