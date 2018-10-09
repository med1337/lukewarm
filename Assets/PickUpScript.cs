using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    public Camera playerCam;
    public Sprite broken;
    public float speed = 5;
    public float throwSpeed = 0.1f;
    Vector3 pos;
    Vector3 offset = new Vector3(0.15f, -0.19f, 0.35f);
    Vector3 throwDir;
    bool pickup = false;
    bool thrown = false;
    // Use this for initialization
    void Start()
    {

        pos = transform.position;
        //grabPos = playerCam.transform.position + offset;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<bullet_movement>())
        {
            return;
        }

        if (col == playerCam.GetComponent<BoxCollider>())
            pickup = true;

        else if (thrown)
        {
            SpriteRenderer myRenderer = transform.GetComponent<SpriteRenderer>();
            myRenderer.sprite = broken;

            if (col.gameObject.tag == "Enemy")
            {
                float force_value = 20.0f;
                col.gameObject.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position) * force_value, ForceMode.Impulse);
            }
            
            Destroy(this.gameObject, 0.2f);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && thrown == false)
        {
            if (transform.parent != null && transform.localPosition == offset)
            {
                thrown = true;
                throwDir = playerCam.transform.forward;
                SphereCollider myCollider = transform.GetComponent<SphereCollider>();
                myCollider.radius = 0.1f;
            }
        }
        if (!TimeManager.Instance.MovementDetected) return;
        if (thrown)
        {
            transform.parent = null;
            transform.position += throwDir * Time.deltaTime * throwSpeed; // (0, 0, throwSpeed); 
        }
        else if (pickup)
        {
            transform.parent = playerCam.transform;
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, offset, step);
        }
    }
}

