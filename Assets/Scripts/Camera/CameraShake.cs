using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraShake : MonoBehaviour
{
    /*
    private GameObject compass;

    public void Awake()
    {
        compass = GameObject.FindGameObjectWithTag("ComPass");
    }
    */

    public delegate void PlayerDestoried();
    public event EventHandler PlayerDestoriedEvent;
    private Vector3 originalPos;
    private GlobalCache global;
    public void Start()
    {
        originalPos = transform.localPosition;
        global = GlobalCache.instance;

    }


    public void shake(GameObject other, bool ifExplode)
    {
        StopAllCoroutines();
        StartCoroutine(Shake( other, ifExplode));
    }

    
   



    private IEnumerator Shake(GameObject other,bool ifExplode)//参数：待销毁对象
    {
        float elapsed = 0.000f;

        

        while (elapsed < 0.05f)
        {

            float x = 0.08f;
            float y = 0.08f;

            //float x = 0.5f;
            //float y = 0.5f;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += global.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;


        if (ifExplode)
        {
            if (other.CompareTag("Player"))
            {
                elapsed = 0.000f;
                while (elapsed < 0.05f)
                {
                    PlayerDestoriedEvent?.Invoke(this, EventArgs.Empty);//广播「Player炸了」
                    //Debug.Log("玩家炸了");
                    elapsed += 0.1f;
                    yield return null;
                }
                //other.GetComponent<Player>().Trail.GetComponent<TrailRenderer>().enabled = false;

            }


            Destroy(other);
            //other.GetComponent<SpriteRenderer>().enabled = false;
            //other.GetComponent<Collider2D>().enabled = false;
            
            
        }
        


    }
}
