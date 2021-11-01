using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PlayerPositionDisplay : MonoBehaviour
{

    Text positionDisplay;
    GameObject player;
    float x;
    float y;
    string x0;
    string y0;
    float xn = 192.4551f;
    float yn = 789.1203f;
    private GlobalCache global;
    private StringBuilder sb;
    private Transform trans;

    void Start()
    {
        global = GlobalCache.instance;
        player = global.player;
        //player = GameObject.FindGameObjectWithTag("Player");
        positionDisplay = GetComponent<Text>();
        //x0 = (player.GetComponent<Transform>().position.x+xn).ToString("f4");
        //y0 = (player.GetComponent<Transform>().position.y+yn).ToString("f4");
        //x = player.GetComponent<Transform>().position.x;
        //y = player.GetComponent<Transform>().position.y;
        sb = new StringBuilder(20);
        trans = global.player.GetComponent<Transform>();
    }
    
    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            if (player != null)
            {
                //x0 = (player.GetComponent<Transform>().position.x + xn).ToString("f4");
                //y0 = (player.GetComponent<Transform>().position.y + yn).ToString("f4");

                //x0 = (trans.position.x + xn).ToString("f4");
                //y0 = (trans.position.y + yn).ToString("f4");

                //x = player.GetComponent<Transform>().position.x;
                //y = player.GetComponent<Transform>().position.y;

                //positionDisplay.text = x0 + ", " + y0;
                sb.Remove(0, sb.Length);
                sb.Append(trans.position.x + xn);
                sb.Append(", ");
                sb.Append(trans.position.y + yn);
                positionDisplay.text = sb.ToString();
            }
            

        }
    }
}
