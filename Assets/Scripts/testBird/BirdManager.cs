using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdManager : MonoBehaviour
{

    public Bird[] Flows;
    public Bird[] DarkFlows;
    public BirdSetting setting;
    private GlobalCache global;
    private PlayerMovement PM;
    public theDOOR DOOR;

    [HideInInspector]
    public int FlowDeadNum;

    [HideInInspector]
    public int DarkFlowDeadNum;


    public bool DOOROpenWhenEnoughHeat = true;
    public bool DOOROpenWhenEnemiesAllDie = false;

    public delegate void OnOpenDoorEventHandler();
    public event OnOpenDoorEventHandler OnOpenDoorEvent;

    public delegate void OnCloseDoorEventHandler();
    public event OnOpenDoorEventHandler OnCloseDoorEvent;

    public W0L2Text W0L2text;

    

    private void Start()
    {
        StartCoroutine(refreshCold());
        global = GlobalCache.instance;
        PM = global.playerMove;
        PM.OnWaveShockEvent += refresh;
        
    }
 

    public int numOfCanGoDoor()
    {
        int num = 0;
        for(int i = 0; i < Flows.Length; i++)
        {
            if ((Flows[i].canGoDoor)||(Flows[i].ifDead))
            {
                num += 1;
            }
        }
        return (num);
    }

    public bool anyCanNotGoDoor()
    {
        for (int i = 0; i < Flows.Length; i++)
        {
            if ((Flows[i].canNotGoDoor)&&(!Flows[i].ifDead))
            {
                return(true);
            }
        }
        return (false);
    }
    
    IEnumerator refreshCold()
    {
        while (true)
        {
            for (int i = 0; i < Flows.Length; i++)
            {
                
                float dis = Flows[i].disFromPlayer;
                if ((dis>=setting.playerLostR) && (!Flows[i].ifColorCold))
                {
                    Flows[i].refreshColorCold();
                    //Debug.Log("变冷");
                }
                
            }


            //通关判定:当过关方式为「热能过关」时，在每次冷却刷新时，判定是否关门或开门
            if (DOOROpenWhenEnoughHeat)
            {
                if ((DOOR.doorIsOpen) && (anyCanNotGoDoor()))
                {
                    //Debug.Log("关门");
                    OnCloseDoorEvent?.Invoke(); //广播「当前处于CloseDoor状态
                }
                else
                {
                    if ((!DOOR.doorIsOpen) && (numOfCanGoDoor() == Flows.Length))
                    {
                        //Debug.Log("开门");
                        OnOpenDoorEvent?.Invoke();//广播「当前处于OpenDoor状态」

                    }
                }
                //Debug.Log("门是否开："+DOOR.doorIsOpen+"是否存在任意一个bird不符合开门条件："+anyCanNotGoDoor());
            }
            if ((DOOROpenWhenEnemiesAllDie)&&(DarkFlowDeadNum==DarkFlows.Length)&&(FlowDeadNum!=Flows.Length))
            {
                if ((DOOR.doorIsOpen) && (anyCanNotGoDoor()))
                {
                    //Debug.Log("关门");
                    OnCloseDoorEvent?.Invoke(); //广播「当前处于CloseDoor状态
                }
                else
                {
                    if ((!DOOR.doorIsOpen) && (numOfCanGoDoor() == Flows.Length))
                    {
                        //Debug.Log("开门");
                        OnOpenDoorEvent?.Invoke();//广播「当前处于OpenDoor状态」

                    }
                }
                //Debug.Log("门是否开："+DOOR.doorIsOpen+"是否存在任意一个bird不符合开门条件："+anyCanNotGoDoor());
            }



            //yield return null;
            yield return new WaitForSecondsRealtime(setting.ChangeTemDelay);
        }
       
    }
    

    public void refresh()
    {
        for (int i = 0; i < Flows.Length; i++)
        {

            float dis = Flows[i].disFromPlayer;

            if ((dis < setting.playerViewR) && (!Flows[i].ifColorHot))
            {
                Flows[i].refreshColorHot();
                //Debug.Log("变热");
            }
             

        }

    }











    /*

    private GameObject[] birds;
    [HideInInspector]
    public GameObject birdBass;
    //private int birdsNum;
    GlobalCache global;
    Camera mainCamera;
    public BirdSetting setting;
    //public GameObject playerBird;
    private void Produce()
    {

        float width = UnityEngine.Screen.width;
        float height = UnityEngine.Screen.height;
        birds = new GameObject[setting.birdsNum + 1];
        global = GlobalCache.instance;
        mainCamera = global.cameraAttribute;
        for (int i = 0; i < setting.birdsNum; i++)
        {
            float randX = Random.Range(0, width);
            float randY = Random.Range(0, height);
            Vector3 v = new Vector3(randX, randY);
            Vector3 pos = mainCamera.ScreenToWorldPoint(v);
            birds[i] = Instantiate(birdBass);
            birds[i].transform.position = pos;
            float angle = Random.Range(0, 12) * 30f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            birds[i].transform.rotation = q;

        }
    }

    */
}
