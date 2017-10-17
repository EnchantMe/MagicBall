using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOrb : MonoBehaviour {

    public float speed = 1.5f;

    private int randomMovement;

    private Animator anim;
    private bool dead = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        randomMovement = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if(randomMovement == 1)
            {
                anim.SetBool("flip", true);
            }
            else if(randomMovement == 2)
            {
                anim.SetBool("flip", false);
            }

            float step = speed * Time.deltaTime;

            if(transform.position.x == GenerationEngine.borderX)
            {
                randomMovement = 2;
            }

            if (transform.position.x == -GenerationEngine.borderX)
            {
                randomMovement = 1;
            }

            if (randomMovement == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(GenerationEngine.borderX, transform.position.y, transform.position.z), step);
            }
            else if (randomMovement == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-GenerationEngine.borderX, transform.position.y, transform.position.z), step);
            }
  
        }
   
    }
    
    
    public void AttackAnimation()
    {
        dead = true;
        anim.SetBool("attack", true);
    }
}
