using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Vector3 touchPosition;

    public GenerationEngine ge;

    public float speed = 5;

    private Animator anim;

	// Use this for initialization
	void Start () {
        touchPosition = gameObject.transform.position;
        anim = gameObject.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        if(transform.position.x < 0)
        {
            anim.SetBool("flip", true);
        }
        else if (transform.position.x >= 0)
        {
            anim.SetBool("flip", false);
        }
        Movement();
	}

    private void MoveToTouchPos(Vector3 touchPosition)
    {
         gameObject.transform.position = Vector3.MoveTowards(transform.position, touchPosition, speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreOrb"))
        {
            anim.SetBool("eat", true);
            collision.gameObject.SendMessage("DeathAnimation");
            Destroy(collision.gameObject);
            ge.SpawnOrbs();
            ge.PlusScore(25);
            anim.SetBool("eat", false);
        }
        if (collision.CompareTag("DeathOrb"))
        {
            StartCoroutine(Death(collision));
        }

        if (collision.CompareTag("BonusOrb"))
        {
            anim.SetBool("eat", true);
            DestroyObject(collision.gameObject);
            ge.PlusScore(50);
        }

    }

    IEnumerator Death(Collider2D collision)
    {
        anim.SetBool("death", true);
        collision.gameObject.SendMessage("AttackAnimation");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        ge.Death();
        StopCoroutine("Death");
    }

    private void Movement()
    {
        MoveToTouchPos(touchPosition);
        ValidatePosition();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y, 10));
            }
        }
    }

    private void ValidatePosition()
    {
        if (gameObject.transform.position.x > 2.5f)
        {
            gameObject.transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.x < -2.5f)
        {
            gameObject.transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.y > 4f)
        {
            gameObject.transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
        }
        if (gameObject.transform.position.y < -4f)
        {
            gameObject.transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }
    }

    private void Event_EatEnd()
    {
        anim.SetBool("eat", false);
    }
}
