﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float Speed;

    private float vertical;

    public bool intersection;

    public bool blockInput = false;
    public bool exiting;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Application.targetFrameRate = 15;
        Screen.SetResolution(190,162,FullScreenMode.FullScreenWindow);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (blockInput) return;

        horizontal = Input.GetAxisRaw("Horizontal") * Speed;
        vertical = Input.GetAxisRaw("Vertical") * Speed;
        if (!intersection)
        {
            if (horizontal > 0)
                transform.position += transform.right * Time.deltaTime ;
            else if (horizontal < 0)
                transform.position -= transform.right * Time.deltaTime;

            if (vertical > 0)
            {
                transform.position += transform.forward * Time.deltaTime;
            }
            else if (vertical < 0)

            {
                transform.position -= transform.forward * Time.deltaTime;
            }
        }
        else
        {
            if (vertical > 0)
            {
                StartCoroutine(MoveOut());
            }

            if (horizontal > 0)
            {
                StartCoroutine(Rotate(true));
            }
            else if (horizontal < 0)
            {
                StartCoroutine(Rotate(false));
            }
        }

        if (horizontal != 0 || vertical != 0)
        {

            TimeManager.Instance.MovementDetected = true;
            TimeManager.Instance.Scale = 1;
        }
        else
        {

            TimeManager.Instance.Scale = 0;
            TimeManager.Instance.MovementDetected = false;
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
        transform.rotation.Set(0,Mathf.RoundToInt(transform.rotation.eulerAngles.y),0,0);
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
};