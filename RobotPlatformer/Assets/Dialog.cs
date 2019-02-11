using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textDisplay;
    [SerializeField]
    private string[] sentences;
    [SerializeField]
    private float speed;
    private int index;
    [SerializeField]
    private GameObject continueTextSkip;
    [SerializeField]
    private Animator textDisplayAnim;
    [SerializeField]
    private GameObject hurtEffect;
    [SerializeField]
    private GameObject elshockSound;
    [SerializeField]
    private GameObject longElshockSound;
    [SerializeField]
    private GameObject screamSound;
    [SerializeField]
    private GameObject attackSound;
    private float waitTime=3; 
    private GameObject playerPos;
    int counter = 0;
    int counter2 = 0;


    void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        textDisplay.text = "";
        StartCoroutine(Type());
    }
	
	
	void Update () {

        hurtEffect.transform.position = Vector2.MoveTowards(hurtEffect.transform.position,playerPos.transform.position,speed*Time.deltaTime);
        NextSentence();
	}

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
    public void NextSentence()
    {
        
        if (textDisplay.text==sentences[index])
        {
         
            
            continueTextSkip.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                continueTextSkip.SetActive(false);
                if (index<sentences.Length-1)
                {
                    index++;
                    if (index==sentences.Length-1)
                    {
                        
                        StartCoroutine(AttackWait());

                    }
                    else
                    {
                        textDisplay.text = "";
                        StartCoroutine(Type());
                    }
                  
                }
                else
                {
                    textDisplay.text = "";
                    continueTextSkip.SetActive(false);
                }

            }      
            if (index == 4)
            {
                
                hurtEffect.SetActive(true);
                if (counter==0)
                {
                    GameObject elShockSoundEffect = Instantiate(elshockSound);
                    GameObject screamSoundEffect = Instantiate(screamSound);                  
                    Destroy(screamSoundEffect, 3.5f);
                    Destroy(elShockSoundEffect,4f);
                    counter++;
                }
              
                hurtEffect.SetActive(true);
                
                textDisplayAnim.SetTrigger("falldown");                
            }
            if (index==5)
            {
                hurtEffect.SetActive(false);
            }
            if (index==6)
            {
        
                textDisplayAnim.ResetTrigger("falldown");
                textDisplayAnim.SetTrigger("getup");
            }
            if (index == 7)
            {
                hurtEffect.SetActive(true);
                textDisplayAnim.ResetTrigger("getup");         
                textDisplayAnim.SetTrigger("run");
                if (counter2 == 0)
                {
                    GameObject longelShockSoundEffect = Instantiate(longElshockSound);
                    GameObject screamSoundEffect = Instantiate(screamSound);
                    Destroy(screamSoundEffect, 3.5f);
                    Destroy(longelShockSoundEffect, 10f);
                    counter2++;
                }
            }
            /*if (index==sentences.Length-1)
            {
                StartCoroutine(AttackWait());
            }*/


        }
    }
   
    IEnumerator AttackWait()
    {        
        yield return new WaitForSeconds(1f);
        textDisplayAnim.SetTrigger("attack");
        GameObject attackSoundEffect = Instantiate(attackSound);
        Destroy(attackSoundEffect,2f);
        hurtEffect.SetActive(false);
        yield return new WaitForSeconds(2f);        
        textDisplayAnim.SetTrigger("idle");
        textDisplay.text = "";
        StartCoroutine(Type());
    }

}
