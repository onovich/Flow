using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theDOOR : MonoBehaviour
{
    public Sprite[] doors;
    public Sprite[] doorRings;

    public Sprite[] doorsWhenPlayerIn;
    public Sprite[] doorsRingsWhenPlayerIn;

    public SpriteRenderer doorRing;

    private SpriteMask mask;

 
    [HideInInspector]
    public bool doorIsOpen;
    public MusicPlayer MP;
    public BirdManager BM;

    private float numOfGoIn;
    private bool hasOpenForPlayer = false;

    public BirdSetting setting;

    public SceneTransManager STM;
    public string nextMapName;

    //public delegate void OnAllGoEventHandler();
    //public event OnAllGoEventHandler OnAllGoEvent;

    private bool PlayerCanGo;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((doorIsOpen)&&(other.CompareTag("Flow"))&&(!other.gameObject.GetComponent<Bird>().ifDead))
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<Bird>().ifMoving = false;
            other.gameObject.GetComponent<Bird>().GoToTheDoor.Play();
            other.gameObject.GetComponent<Bird>().trailRenderer.enabled = false;
            MP.FlowGoesPlay();
             numOfGoIn += 1;
            Debug.Log("毁灭啦:第"+numOfGoIn+"个");
            if ((numOfGoIn + BM.FlowDeadNum == BM.Flows.Length)&&(!hasOpenForPlayer))
            {

                //OnAllGoEvent?.Invoke();//广播「所有Flow都进门了」
                OpenForPlayer();
            }
            Debug.Log("此时玩家能进门吗:"+PlayerCanGo);
            other.gameObject.GetComponent<Bird>().ifDead = true;

        }
        if ((doorIsOpen) && (PlayerCanGo) && (other.CompareTag("Player")) && (!other.GetComponent <Player>().ifDead))
        {
            MP.PlayerGoesPlay();
 
            other.gameObject.GetComponent<Player>().GoToTheDoor.Play();
            other.gameObject.GetComponent<Player>().Trail.GetComponent<TrailRenderer>().enabled = false;
            other.gameObject.GetComponent<Player>().render.enabled = false;
            other.gameObject.GetComponent<Player>().circleRender.enabled = false;
            
            Debug.Log("过关！");
            STM.goesto(nextMapName);
        }
    }




    private void Start()
    {
        mask = GetComponent<SpriteMask>();
        doorIsOpen = false;
        //Open();
        BM.OnOpenDoorEvent += Open;
        BM.OnCloseDoorEvent += Close;
        numOfGoIn = 0;
        PlayerCanGo = false;
        hasOpenForPlayer = false;
        mask.sprite = doors[0];

    }

    private void Open()
    {
        StopAllCoroutines();
        StartCoroutine(openMask());
    }

    IEnumerator openMask()
    {
        doorIsOpen = true;
        MP.DoorOpenPlay();
        for (int i = 0; i < doors.Length; i++)
        {
            mask.sprite = doors[i];
            //yield return null;
            yield return new WaitForSecondsRealtime(setting.openDoorSpeed);
        }
        for(int i = 0; i < doorRings.Length; i++)
        {
            doorRing.sprite = doorRings[i];
            //yield return null;
            yield return new WaitForSecondsRealtime(setting.openDoorSpeed);

        }
        
    }


    IEnumerator openMaskForPlayer()
    {
        hasOpenForPlayer = true;
        MP.DoorChangePlay();

        for (int i = 0; i < doorsWhenPlayerIn.Length; i++)
        {
            mask.sprite = doorsWhenPlayerIn[i];
            //yield return null;
            yield return new WaitForSecondsRealtime(setting.openDoorSpeed);


        }
        for (int i = 0; i < doorsRingsWhenPlayerIn.Length; i++)
        {
            doorRing.sprite = doorsRingsWhenPlayerIn[i];
            //yield return null;
            yield return new WaitForSecondsRealtime(setting.openDoorSpeed);

        }
        PlayerCanGo = true;
    }


    private void Close()
    {
        StopAllCoroutines();
        StartCoroutine(closeMask());
    }
    
    
    IEnumerator closeMask()
    {
        doorIsOpen = false;
        for (int i = doorRings.Length-1; i >= 0; i--)
        {
            doorRing.sprite = doorRings[i];
            yield return null;

        }
        for (int i = doors.Length-1; i >=0 ; i--)
        {
            mask.sprite = doors[i];
            yield return null;
        }
        
        
    }

    private void OpenForPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(openMaskForPlayer());
    }

    



}
