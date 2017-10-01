using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private Vector3 touchPosition;

    public GenerationEngine ge;

    public float speed = 5;

	// Use this for initialization
	void Start () {
        touchPosition = gameObject.transform.position;
	}

    // Update is called once per frame
    void Update() {

        MoveToTouchPos(touchPosition);

        if(gameObject.transform.position.x > 2.5f)
        {
            gameObject.transform.position = new Vector3(2.5f,transform.position.y,transform.position.z);
        }
        if (gameObject.transform.position.x < -2.5f)
        {
            gameObject.transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        }
        if (gameObject.transform.position.y > 4f)
        {
            gameObject.transform.position = new Vector3(transform.position.x , 4f, transform.position.z);
        }
        if(gameObject.transform.position.y < -4f)
        {
            gameObject.transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y,10));  
            }
        }
	}

    private void MoveToTouchPos(Vector3 touchPosition)
    {
         gameObject.transform.position = Vector3.MoveTowards(transform.position, touchPosition, speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Orb"))
        {
            Destroy(collision.gameObject);
            ge.SpawnOrb();
            ge.PlusScore();
        }
    }
}
