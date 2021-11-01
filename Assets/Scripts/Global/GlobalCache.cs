using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCache : MonoBehaviour
{
    //初始化
    #region Singleton
    public static GlobalCache instance;
  


    private void Awake()
    {
        instance = this;
        playerAttribute = instance.player.GetComponent<Player>();
        cameraAttribute = instance.Camera.GetComponent<Camera>();
        playerMove = instance.player.GetComponent<PlayerMovement>();
        CM = instance.APP.GetComponent<CameraMove>();
        CR = instance.CameraHolder.GetComponent<CameraRotate>();
        SceneTrans = SceneTransition.GetComponent<SceneTransition>();
        BirdManager = GetComponent<BirdManager>();
        timeScale = 1f;
        unSaledTimeScale = 1f;
    }
    #endregion

    public GameObject player;
    public GameObject Camera;
    public GameObject APP;
    public GameObject ColorManager;
    public GameObject CameraHolder;
    [HideInInspector]
    public Player playerAttribute;//用于直接调用player的属性，避免GetComponent
    [HideInInspector]
    public Camera cameraAttribute;
    [HideInInspector]
    public float deltaTime;
    [HideInInspector]
    public PlayerMovement playerMove;
    [HideInInspector]
    public CameraMove CM;
    [HideInInspector]
    public CameraRotate CR;
    [HideInInspector]
    public float timeScale;
    [HideInInspector]
    public float unSaledTimeScale;
    [HideInInspector]
    public float unscaledDeltaTime;
    public GameObject SceneTransition;
    [HideInInspector]
    public SceneTransition SceneTrans;
    [HideInInspector]
    public BirdManager BirdManager;
    


private void Update()
    {
        deltaTime = Time.deltaTime * timeScale;
        unscaledDeltaTime = Time.unscaledDeltaTime * unSaledTimeScale;
    }

}
