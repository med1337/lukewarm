using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> positions = new List<Transform>();
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float speed;

    private int index = 0;


    // Use this for initialization
    void Start()
    {
        
        //positions = new List<Transform>(GetComponentsInChildren<Transform>());
        //positions.Remove(transform);
    }


    void Awake()
    {
        //positions = new List<Transform>(GetComponentsInChildren<Transform>());
        //positions.Remove(transform);    
    }


    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.Instance.MovementDetected) return;

        Move();
        sprite.color = !TimeManager.Instance.MovementDetected ? Color.black : Color.red;
    }


    void Move()
    {
        Vector3 direction = positions[index].position - transform.position;

        transform.position += direction * speed * TimeManager.Instance.TimeScale;
        if (Vector3.Distance(transform.position, positions[index].position) <= 0.1f)
        {
            transform.position = positions[index].position;
            index++;
            if (index >= positions.Count)
            {
                index = 0;
            }
        }
    }


    private void OnDrawGizmos()
    {
        foreach (var position in positions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(position.position, 0.1f);
        }
    }
}