using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用事件所需

public class ExplodeEffect : MonoBehaviour
{
    Character ExplodeObject;
    private GameObject mainCamera;
    private CameraShake camerashake;
    private GameObject APP;
   
    private GlobalCache global;
    private Player playerAttribute;

    public void Start()
    {
        global = GlobalCache.instance;
        APP = global.APP;
        mainCamera = global.Camera;
        playerAttribute = global.playerAttribute;
       
        switch (gameObject.tag)
        {
            case "Enemy":
                break;
            case "npc":
                break;
            case "Player":
                ExplodeObject = GetComponent<Player>();
                break;
                //case "SnowBall":
                //ExplodeObject = GetComponent<SnowBall>();
                //break;
        }
    }

    public void PlayerDead(object sender, EventArgs e)
    {
        if((this != null)&&(gameObject.CompareTag("Player")))
        {
            Explode();
            
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (ExplodeObject == null) { Debug.LogError("脚本内没有注册Explode组件"); }
        if ((this != null) && (ExplodeObject.killers.Contains(other.gameObject.tag)))//被天敌碰撞时
        {
            Debug.Log("调用碰撞爆炸");
            Explode();



        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)//当物体的Trigger被打开时生效
    {
        if (ExplodeObject == null) { Debug.LogError("脚本内没有注册Explode组件"); }
        if ((this != null) &&(other.isTrigger==true)&& (ExplodeObject.killers.Contains(other.gameObject.tag)))//被天敌碰撞时
        {

            Debug.Log("调用碰撞爆炸");
            Explode();

        }
    }
    




    public void Explode()
    {
        

        if (mainCamera.GetComponentInChildren<CameraShake>() != null)
        {
            mainCamera.GetComponentInChildren<CameraShake>().shake(this.gameObject, true);
        }


        Instantiate(ExplodeObject.explodeEffect, ExplodeObject.transform.position, Quaternion.identity);


    }
}
