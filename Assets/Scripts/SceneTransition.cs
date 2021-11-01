using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    Animator animator;
    public GameObject black;
    GameObject player;
    GlobalCache global;
    Transform playerTrans;
    GameObject cameraHolder;
    Transform cameraTrans;
    Player playerAttribute;
    CameraMove CM;
    GlowController playerGC;
    ScreenColorManager SCM;
    GameObject APP;


    private void Start()
    {
        animator = black.GetComponent<Animator>();
        global = GlobalCache.instance;
        player = global.player;
        playerTrans = player.transform;
        cameraHolder = global.CameraHolder;
        cameraTrans = cameraHolder.transform;
        playerAttribute = global.playerAttribute;
        CM = global.CM;
        playerGC = player.GetComponent<GlowController>();
        APP = global.APP;
        SCM = APP.GetComponent<ScreenColorManager>();

    }

    //以下两则为自然过渡切换，由动画末尾帧调用
    /*
    public void BigDone()
    {
        animator.SetBool("bigDone",true);
    }
    public void SmallDone()
    {
        animator.SetBool("smallDone", true);
    }
    */
    public void UnBlackDone()
    {
        animator.SetBool("UnBlackDone", true);
    }



    //以下两则为场景切换，由场景管理方法调用
    /*
    public void beBig()
    {
        animator.SetBool("beBig", true);
    }
    public void beSmall()
    {
        animator.SetBool("beSmall", true);
    }
    */
    public void BeBlack()
    {
        animator.SetBool("beBlack", true);
    }
    public void UnBlack()
    {
        animator.SetBool("beBlack", false);
    }

    public void placePlayer()
    {

    }



    IEnumerator Load()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Level1-1");
        yield return new WaitForEndOfFrame();
        op.allowSceneActivation = true;
    }



    public void resetPlayer()
    {


        StartCoroutine(Load());

        /*
        playerAttribute.SkillMovementOn = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Collider2D>().enabled = true;
        playerAttribute.Trail.GetComponent<TrailRenderer>().enabled = true;
        playerAttribute.MaxHP = playerAttribute.originalMaxHP;
        playerAttribute.HP = playerAttribute.MaxHP;
        SCM.GameReset();
        playerGC.glowDown();
        playerTrans.localScale = new Vector3(1, 1, 1);
        playerTrans.position = new Vector3(0, 0, 0);
        cameraTrans.position = new Vector3(0, 0, 0);
        playerTrans.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        cameraTrans.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        
        CM.ScareTo(5,10,0);
        */

    }

    //场景管理方法，由其他触发方法调用
    public void SceneNext()
    {
        //beSmall();
        BeBlack();
        Invoke(nameof(resetPlayer), 1f);
        Invoke(nameof(UnBlack), 1.5f);
        //UnBlack();
    }
    public void SceneRestart()
    {
        //beSmall();
        BeBlack();
        Invoke(nameof(resetPlayer), 1f);
        Invoke(nameof(UnBlack), 1.5f);

        //UnBlack();


    }
    public void GameOverSceneRestart()
    {
        //beSmall();
        BeBlack();
        Invoke(nameof(resetPlayer), 1f);
        Invoke(nameof(UnBlack), 1.5f);

        //UnBlack();


    }



}
