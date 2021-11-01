using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用事件所需


public class GameOver : MonoBehaviour//玩家死后将画面变成黑白（其他效果待写，如弹出重玩按钮）
{


    private GameObject APP;
    private ScreenColorManager SCM;
    private GameObject mainCamera;
    private CameraMove CM;
    private GlobalCache global;
    private SceneTransition ST;

    private void Start()
    {
        global = GlobalCache.instance;
        APP = global.APP;
        mainCamera = global.Camera;
        //APP = GameObject.FindGameObjectWithTag("APP");
        SCM = APP.GetComponent<ScreenColorManager>();
        CM = APP.GetComponent<CameraMove>();
        //mainCamera = GameObject.FindGameObjectWithTag("Camera");
        mainCamera.GetComponent<CameraShake>().PlayerDestoriedEvent += BlackAndWhite;//event
        ST = global.SceneTrans;

    }


    void BlackAndWhite(object sender, EventArgs e)//event
    {


        SCM.GameOverDisplay();
        CM.ScareTo(7, 1, 0);
        Invoke(nameof(SceneRestart), 3f);


        //StartCoroutine(SCM.SetCurse());


    }

    void SceneRestart()
    {
        ST.GameOverSceneRestart();
    }

}
