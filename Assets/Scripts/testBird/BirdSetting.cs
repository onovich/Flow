using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class BirdSetting : ScriptableObject
{

    public float playerAngleSpeed = 2f;
    public float playerMoveSpeed = 80f;
    public float playerMoveSpeedUp = 170f;

    public float maxSteerForce = 5f;
    public float minSpeed = 2;
    public float maxSpeed = 5;

    public float fleeWeight = 2;
    public float gatherWeight = 1;
    public float fixedWeight = 1;
    public float playerGuideWeight = 3;

    public bool ifHasPlayer = true;
    public int birdsNum = 80;

    public float playerViewR = 1f;
    public float playerLostR = 2f;
    public float warmSpeed = 0.01f;
    public float coolSpeed = 0.02f;

    public float ChangeTemDelay = 0.1f;

    public float speedScaleFromHeat = 1;
    public float PlayerGuideScaleFromeHeat = 2;

    public float heatWhenDoorClose = 0.2f;
    public float heatWhenDoorOpen = 0.8f;

    public float openDoorSpeed = 0.1f;

    public float PlayerGuidMin = 0.1f;


}
