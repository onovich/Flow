using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager : MonoBehaviour
{
    
    
    public static GlobalEventManager instance;

    
    public event EventHandler OnHealEvent;
    public event EventHandler CopyOnMergeEvent;

    private void Awake()
    {
        instance = this;
    }

    public void OnHeal(object sender, EventArgs e)
    {
        OnHealEvent?.Invoke(this, EventArgs.Empty);//广播「正在治疗」需要回血
    }

    public void CopyOnMerge(object sender, EventArgs e)
    {
        CopyOnMergeEvent?.Invoke(this, EventArgs.Empty); //广播「Copy完成回收」需要回血
    }

    


}