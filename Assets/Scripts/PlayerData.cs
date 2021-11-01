using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//本数据用于Player数据的跨地图传输，仅在场景切换前进行写入，在场景切换后、黑屏消失前读取
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float HP;
    public float MaxHP;

    //以及技能列表
    //暂不记录分身状态，也就是说，地图传输后Player的分身会自动收回





}
