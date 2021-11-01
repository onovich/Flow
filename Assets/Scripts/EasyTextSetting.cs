using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EasyTextSetting : ScriptableObject
{
    
    public bool ifStartWhenAwake;
    public int AwakeTextId;
    public textSprite[] texts;
    public float backShowsSpeed = 1f;
    public float backShowsTimeWait = 0.1f;
    public float textShowsSpeed = 5f;
    public float textShowsTimeWait = 0.1f;
    public float pointAngleSpeed = 0.1f;
    public float pointAngleTimeWait = 0.1f;
    public float disappearTime = 4f;
    public bool ifGoNextMapAfterPress = false;
    public string nextMapNameIfGo;
}
