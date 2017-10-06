using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    public float speed = 0.5f;

    private int randomMovement;

    void Start () {

        randomMovement = Random.Range(1, 2);

    }
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;

        if (randomMovement == 1)
        {
            Vector3.MoveTowards(transform.position, new Vector3(GenerationEngine.borderX, transform.position.y, transform.position.z), step);
        }
        else if (randomMovement == 2)
        {
            Vector3.MoveTowards(transform.position, new Vector3(-GenerationEngine.borderX, transform.position.y, transform.position.z), step);
        }

    }
}
