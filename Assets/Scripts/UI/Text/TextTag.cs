using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum textDisplayAuto
{
    AutoOnTrigger,//用于区域触发
    AutoOnCollision,//用于碰撞触发
    waitToCall//等待调用

}
/*
public enum textType
{
    Tip,//解释性文本
    Speak//发言


}
*/


public class TextTag : MonoBehaviour
{
    public int textID;
    //public float waitMoreSeconds;
    public textDisplayAuto auto = textDisplayAuto.waitToCall;
    //public textType type = textType.Speak;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

     
}
