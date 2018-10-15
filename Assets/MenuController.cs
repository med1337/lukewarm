using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] public Text luke;
    [SerializeField] public Text warm;
    [SerializeField] public Text start;
    [SerializeField] public Image img;

    private Text current_text;
    private float start_timer = 0.0f;
    [SerializeField] private float start_delay = 0.3f;
    private float tmr = 0.0f;
    [SerializeField] private float dl = 2.0f;
    private GameObject soundMaker; 


    // Use this for initialization
    void Start()
    {
        current_text = luke;
        current_text.enabled = true;
        soundMaker = GameObject.Find("AudioPlayer"); 
    }
    


    // Update is called once per frame
    void Update()
    {
        tmr += Time.deltaTime;
        start_timer += Time.deltaTime;

        if (start_timer > start_delay)
        {
            start_timer = 0.0f;

            start.enabled = !start.enabled;
        }

        if (tmr > dl)
        {
            tmr = 0.0f;
            current_text.fontSize += 25;

            if (current_text.fontSize > 299)
            {
                current_text.enabled = false;
                current_text.fontSize = 200;

                if (current_text.text == luke.text)
                {
                    soundMaker.GetComponent<audioPlayer>().PlaySound(5);
                    current_text = warm;
                }
                else
                {
                    soundMaker.GetComponent<audioPlayer>().PlaySound(4);
                    current_text = luke;
                }

                current_text.enabled = true;
            }
        }
       
    }
}