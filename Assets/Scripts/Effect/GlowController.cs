using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlowColor
{
    white,
    red,
    yellow,
}

public enum renderType
{
    Sprite,
    Trail,
    Particle,
}



public class GlowController : MonoBehaviour
{
    [HideInInspector]
    public Material glowMa;
    [HideInInspector]
    public Material glowMaShare;
    //private GameObject player;
    [HideInInspector]
    public Color glowColor;
    float i;
    float upOrDown = 1;
    float currentI;
    private GlobalCache global;
    public bool initIfBloom = false;
    public bool MateriaIfShare = true;
    public float intensity = 1.2f;
    [HideInInspector]
    public float r;
    [HideInInspector]
    public float g;
    [HideInInspector]
    public float b;
    //float a;
    [HideInInspector]
    public float factor;
    public GlowColor color = GlowColor.white;

    public renderType render = renderType.Sprite;



    public void glowFastUp()
    {
        getGlow(8f, 4f);
    }

    public void glowFastDown()
    {
        getGlow(0f, 4f);

    }

    public void glowUp()
    {
        //getGlow(4f);
        getGlow(8f,0.5f);
    }
    public void glowDown()
    {
        getGlow(0f,0.5f);
    }


    private void Start()
    {
        //intensity = 0.5f;
        //intensity = 1.2f;
        //intensity = 0f;
        factor = Mathf.Pow(2, intensity);

        initBloom();
    }


    public virtual void initGlowMa()
    {
        switch (render)
        {
            case renderType.Sprite:
                glowMa = GetComponent<SpriteRenderer>().sharedMaterial;
                break;
            case renderType.Particle:
                glowMa = GetComponent<Renderer>().sharedMaterial;
                break;
            case renderType.Trail:
                glowMa = GetComponent<TrailRenderer>().sharedMaterial;
                break;
        }


    }



    public virtual void initBloom()
    {
        if (!initIfBloom)
        {
            //初始化发光材质，使其不发光
            //glowMa = GetComponent<SpriteRenderer>().sharedMaterial;
            initGlowMa();
            r = 0;
            g = 0;
            b = 0;
            //a = 0;

            //glowColor.r = 0;
            //glowColor.g = 0;
            //glowColor.b = 0;

            //glowColor = new Color(r, g, b);
            //glowColor = new Color(r * factor, g * factor, b * factor);

            glowColor.r = r * factor;
            glowColor.g = g * factor;
            glowColor.b = b * factor;

            glowMa.SetColor("Color_9EE04840", glowColor);
        }
        else
        {
            //初始化发光材质，使其发光
            //glowMa = GetComponent<SpriteRenderer>().sharedMaterial;
            initGlowMa();
            //glowColor.r = 0.4f;
            //glowColor.g = 0.4f;
            //glowColor.b = 0.4f;

            switch (color )
            {
                case GlowColor.red:
                    r = 1;
                    g = 0;
                    b = 0;
                    //a = 0;
                break;

                case GlowColor.white:
                    r = 1;
                    g = 1;
                    b = 1;
                    //a = 0;
                break;

                case GlowColor.yellow:
                    r = 1;
                    g = 0.9f;
                    b = 0.5f;
                    //a = 0;
                    break;
            }



            //glowColor.r = 0.9f;
            //glowColor.g = 0.9f;
            //glowColor.b = 0.9f;

            glowColor.r = r * factor;
            glowColor.g = g * factor;
            glowColor.b = b * factor;



            //glowColor = new Color(r, g, b);
            //glowColor = new Color(r * factor, g * factor, b * factor,a*factor);

            glowMa.SetColor("Color_9EE04840", glowColor);
        }
    }


    //public void getGlow(GameObject glower, float intensity, bool MateriaIfShare)//传入发光对象、发光强度
    public void getGlow(float inten,float speed)//传入发光对象、发光强度、发光速度
    {
        global = GlobalCache.instance;
        

        
        if (MateriaIfShare)
        {
            glowMaShare = GetComponent<SpriteRenderer>().sharedMaterial;
            glowColor = glowMaShare.GetColor("Color_9EE04840");//这个Color_9EE04840是shader里的给定名称，可以去shader里改的
        }
        else
        {
            glowMa = GetComponent<SpriteRenderer>().material;
            glowColor = glowMa.GetColor("Color_9EE04840");


        }

        //factor = Mathf.Pow(2, inten);


        currentI = glowColor.r;
        i = inten / 10 * factor;
        if (currentI > inten) { upOrDown *= -1; }
        else { upOrDown = 1; }
        StartCoroutine(glowTo(MateriaIfShare,speed));
    }


    IEnumerator glowTo(bool IfShare,float speed)
    {
        if((upOrDown == 1))
        {
            while (currentI < i)
            {
                //currentI += upOrDown * global.deltaTime * 1 * 0.5f;
                currentI += upOrDown * global.deltaTime * 1 * speed;
                //currentI += upOrDown * Time.deltaTime * 1 * 0.1f;
                glowColor.r = currentI;
                glowColor.g = currentI;
                glowColor.b = currentI;

                if (IfShare)
                {
                    glowMaShare.SetColor("Color_9EE04840", glowColor);

                }
                else
                {
                    glowMa.SetColor("Color_9EE04840", glowColor);

                }

                //Debug.Log("r="+glowColor.r+ ",g=" + glowColor.g + ",b=" + glowColor.b);
                //Debug.Log("currentI=" + currentI + ",i=" + i);

                yield return null;

            }
            glowColor.r = i;
            glowColor.g = i;
            glowColor.b = i;
            if (IfShare)
            {
                glowMaShare.SetColor("Color_9EE04840", glowColor);

            }
            else
            {
                glowMa.SetColor("Color_9EE04840", glowColor);

            }
            yield return null;

        }
        if (upOrDown == -1)
        {
            while (currentI > i)
            {
                currentI += upOrDown * global.deltaTime * 1f;
                //currentI += upOrDown * Time.deltaTime * 0.2f;
                glowColor.r = currentI;
                glowColor.g = currentI;
                glowColor.b = currentI;

                if (IfShare)
                {
                    glowMaShare.SetColor("Color_9EE04840", glowColor);

                }
                else
                {
                    glowMa.SetColor("Color_9EE04840", glowColor);

                }

                //Debug.Log("r=" + glowColor.r + ",g=" + glowColor.g + ",b=" + glowColor.b);
                //Debug.Log("currentI=" + currentI + ",i=" + i);

                yield return null;

            }
            glowColor.r = i;
            glowColor.g = i;
            glowColor.b = i;
            if (IfShare)
            {
                glowMaShare.SetColor("Color_9EE04840", glowColor);

            }
            else
            {
                glowMa.SetColor("Color_9EE04840", glowColor);

            }
            yield return null;
        }
        

    }

    



}
