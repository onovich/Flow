using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDGPS : MonoBehaviour
{

    private GameObject[] enemies;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        

            
    }

    void Update()
    {
        //需要优化，仅在玩家移动时执行

        if (enemies != null)
        {
            /*
            foreach(GameObject enemy in enemies)
            {
                Debug.Log(enemy.GetComponent<Transform>().position);
            }
            */


            for(int i = 0; i < enemies.Length; i++)
            {
                //Debug.Log(i+"号敌人坐标："+enemies[i].GetComponent<Transform>().position);

            }


        }




    }
}
