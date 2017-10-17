using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrb : MonoBehaviour
{

	public float speed = 1.5f;
	
	[HideInInspector]
	public enum StatesOfMovement
	{
		Left,Right,Up,Down
	}

	[HideInInspector] public StatesOfMovement state;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float step = speed * Time.deltaTime;
		
		switch (state)
		{
				case StatesOfMovement.Down:
					transform.position = Vector3.MoveTowards(transform.position,
						new Vector3(transform.position.x, -GenerationEngine.borderY-2f, transform.position.z), step);
					break;
				case StatesOfMovement.Up:
					transform.position = Vector3.MoveTowards(transform.position,
						new Vector3(transform.position.x, GenerationEngine.borderY+2f, transform.position.z), step);
					break;
				case StatesOfMovement.Left:
					transform.position = Vector3.MoveTowards(transform.position,
						new Vector3(-GenerationEngine.borderX-2f, transform.position.y, transform.position.z), step);
					break;
				case StatesOfMovement.Right:
					transform.position = Vector3.MoveTowards(transform.position,
						new Vector3(GenerationEngine.borderX+2f, transform.position.y, transform.position.z), step);
					break;
		}
	}
}
