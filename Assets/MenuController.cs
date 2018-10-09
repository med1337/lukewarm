using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Text luke;
    [SerializeField] Text warm;
    [SerializeField] Text start;

    private Text current_text;
    private float start_timer = 0.0f;
    [SerializeField] private float start_delay = 0.3f;


    // Use this for initialization
    void Start()
    {
        current_text = luke;
        current_text.enabled = true;
    }
    


    // Update is called once per frame
    void Update()
    {
        start_timer += Time.deltaTime;

        if (start_timer > start_delay)
        {
            start_timer = 0.0f;

            start.enabled = !start.enabled;
        }

        current_text.fontSize += 5;

        if (current_text.fontSize > 299)
        {
            current_text.enabled = false;
            current_text.fontSize = 200;

            if (current_text.text == luke.text)
            {
                current_text = warm;
            }
            else
            {
                current_text = luke;
            }

            current_text.enabled = true;
        }
    }
}