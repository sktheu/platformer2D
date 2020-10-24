using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{   
    private float seconds = 0;
    private int minutes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string timeText = "Time ";
        seconds += Time.deltaTime;
        if (seconds >= 60f){
            seconds = 0;
            minutes++;
        }

        if (minutes < 10){
            timeText += "0"+minutes+":";
        }else{
            timeText += minutes+":";
        }

        if (seconds < 10){
            timeText += "0"+(int)(seconds);
        }else{
            timeText += (int)(seconds);
        }

        gameObject.GetComponent<Text>().text = timeText;

    }
}
