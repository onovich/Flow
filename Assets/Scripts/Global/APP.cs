using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APP : MonoBehaviour
{
    private UIManager mainUI;
    GlobalCache global;
    Scene scene;
    //private GameObject mainCamera;
    private CameraMove CM;
    public GameObject player;
    GlowController playerGC;
    private SceneTransition ST;



    public bool ifTest = false;

    void Start()
    {
        global = GlobalCache.instance;

        Screen.SetResolution(1920, 1080, true);
        CM = GetComponent<CameraMove>();
        //mainUI = global.UI.GetComponent<UIManager>();
        //mainUI.canvas.SetActive(true);
        player = global.player;
        playerGC = player.GetComponent<GlowController>();

        SceneManager.GetActiveScene();
        ST = global.SceneTrans;

    }






    // Update is called once per frame

    void Update()
    {
        if ((scene.name == "textBoard")||(ifTest))
        {
            if (Input.GetKey(KeyCode.P))
            {
                Time.timeScale = 0;
                //global.timeScale = 0;
            }
            if (Input.GetKeyUp(KeyCode.P))
            {
                Time.timeScale = 1;
                //global.timeScale = 1;

            }

            if (Input.GetKey(KeyCode.L))
            {
                CM.ScareTo(2,1,0);
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                CM.ScareTo(5, 1, 0);
            }

            if (Input.GetKey(KeyCode.U))
            {
                playerGC.glowFastUp();
            }
            if (Input.GetKeyUp(KeyCode.U))
            {
                playerGC.glowDown();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                ST.GameOverSceneRestart();

            }

        }

            

        


    }
    
}
