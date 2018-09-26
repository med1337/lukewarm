using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    [SerializeField] private SpriteRenderer sprite;
    private int index = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!TimeManager.Instance.MovementDetected) return;

	    sprite.color = !TimeManager.Instance.MovementDetected ? Color.black : Color.red;

	    transform.position += Vector3.up * 5.0f * TimeManager.Instance.Time;
	}
}
