using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CameraMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float Speed;
    [SerializeField] GameObject fist;

    private float vertical;

    public bool intersection;

    public bool blockInput = false;
    public bool exiting;
    public bool button;
    private Rigidbody rigidbody;
    public bool can_punch = false;
    private bool holding_throwable = false;
    private PickUpScript throwable_object;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Application.targetFrameRate = 15;
        Screen.SetResolution(190, 162, FullScreenMode.FullScreenWindow);
        Speed = 2; 
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (blockInput) return;
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * Speed;
        vertical = Input.GetAxisRaw("Vertical") * Speed;

        if (vertical > 0)
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
        }
        else if (vertical < 0)

        {
            transform.position -= transform.forward * Time.deltaTime * Speed;
        }

        if (!GameManager.Instance.Hold)
        {
            if (horizontal > 0)
                transform.position += transform.right * Time.deltaTime * Speed;
            else if (horizontal < 0)
                transform.position -= transform.right * Time.deltaTime * Speed;
        }
        else
        {
            if (horizontal > 0)
            {
                transform.Rotate(transform.up, 5);
            }
            else if (horizontal < 0)
            {
                transform.Rotate(transform.up, -5);
            }
        }


        if (horizontal != 0 || vertical != 0)
        {
            //Debug.Log(button + ", " + horizontal + ", " + vertical);
            if (GameManager.Instance.Hold && vertical == 0)
            {
                TimeManager.Instance.Scale = 0;
                TimeManager.Instance.MovementDetected = false;
            }
            else
            {
                TimeManager.Instance.MovementDetected = true;
                TimeManager.Instance.Scale = 1;
            }
        }
        else
        {
            TimeManager.Instance.Scale = 0;
            TimeManager.Instance.MovementDetected = false;
        }

        Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.red);
        
        Vector3 punch_origin = transform.position;
        punch_origin += transform.forward;
        float punch_radius = 2.0f;
        
        Collider[] hit_colliders = Physics.OverlapSphere(punch_origin, punch_radius);

        can_punch = false;
        fist.SetActive(false);

        for (int i = 0; i < hit_colliders.Length; i++)
        { 
            if (hit_colliders[i].gameObject.tag == "Enemy")
            {
                if (!holding_throwable)
                {
                    can_punch = true;

                    fist.SetActive(true);
                }
            }
            i++;
        }

        if (GameManager.Instance.Throw)
        {
            if (holding_throwable)
            {
                throwable_object.Throw();
            }
            else if (!holding_throwable)
            {
                if (can_punch)
                {
                    for (int i = 0; i < hit_colliders.Length; i++)
                    {
                        if (hit_colliders[i].gameObject.tag == "Enemy")
                        {
                            float force_value = 20.0f;

                            hit_colliders[i].gameObject.GetComponent<Rigidbody>().AddForce((hit_colliders[i].gameObject.transform.position - this.transform.position) * force_value, ForceMode.Impulse);
                        }
                    }
                }
            }
        }        
    }


    IEnumerator Rotate(bool right)
    {
        int rot = 1;
        blockInput = true;
        while (rot % 90 != 0)
        {
            if (right)
            {
                transform.Rotate(transform.up, 5);
            }
            else
            {
                transform.Rotate(transform.up, -5);
            }

            yield return new WaitForEndOfFrame();

            rot = (int) (transform.rotation.eulerAngles.y);
        }

        transform.rotation.Set(0, Mathf.RoundToInt(transform.rotation.eulerAngles.y), 0, 0);
        blockInput = false;

        yield return null;
    }


    IEnumerator MoveOut()
    {
        transform.rotation.Set(0, Mathf.RoundToInt(transform.rotation.eulerAngles.y), 0, 0);
        blockInput = true;
        while (intersection)
        {
            transform.position += transform.forward * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        blockInput = false;
    }


    IEnumerator GoToIntersection(Transform tr)
    {
        blockInput = true;
        Vector3 dirVector3 = tr.position - transform.position;
        dirVector3.Normalize();
        while (Vector3.Distance(transform.position, tr.position) > 0.1f)
        {
            transform.position += dirVector3 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = tr.position;
        blockInput = false;
        yield return null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.gameObject.tag == "Enemy" || collision.rigidbody.gameObject.tag == "Bullet")
            {
                GameManager.Instance.GameOver();
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Intersection")
        {
            intersection = true;
            StartCoroutine(GoToIntersection(col.transform));
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Intersection")
        {
            intersection = false;
        }
    }

    public void UpdateThrowingObject(bool _object, PickUpScript _object_script)
    {
        holding_throwable = _object;

        throwable_object = _object_script;
    }

    public bool GetThrowingObject()
    {
        throwable_object = null;

        return holding_throwable;
    }
};