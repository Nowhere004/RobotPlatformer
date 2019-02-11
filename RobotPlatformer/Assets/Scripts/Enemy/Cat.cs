using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Enemy
{

	// Use this for initialization
	public override void Start () {
        base.Start();
        EnemyHealth = 3;
        EnemyAnimator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
        ChangeDirection();
    }

}
