using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//fix vibe to properly
//fix frame to vector

public enum Mode
{
    MOVE = 0,
    FADE,
    SCALE,
    VIBE,
    ROTATE,
    NONE,
    SUM_FINISH
};

public enum CutInOut
{
    None,
    Cutin,
    Cutout
};

public enum Group
{
    X,
    Y,
    Z,
    XYZ
};

public enum OutMode 
{ 
    NONE,
    START,
    END,
    BOTH
};

public class AniImage : MonoBehaviour {

    [System.Serializable]
    public struct StatusAnimate {

        public bool finish;
        public bool clear;
        public bool loop;
        public bool reLoop;

        public Mode aniMode;
        public AnimPattern speedType;
        public OutMode outMode;

        public int aniId;
        public int vibeLevel;

        public Vector3 frame;
        public Vector3 frameMax;
        public Vector3 delay;

        public Vector3 startValue;
        public Vector3 targetValue;
        public Vector3 nowValue;
    }

    public const float DEFAULT_FRAMERATE = 60f;
    public const float CAST_FRAMERATE = 20f;
    public const float BG_FADEFRAME = 40f;
    public const float MASK_FADEFRAME = 60f;
    
    public List<StatusAnimate> m_Status;// = new List<StatusAnimate>();

    [HideInInspector]
    public Color AniColor = Color.white;

    [HideInInspector]
    public Color StartColor = Color.white;
    [HideInInspector]
    public Color TargetColor = Color.white;

    [HideInInspector]
    public Vector3 NextPosition = Vector3.zero;

    [HideInInspector]
    public Vector3 NextScale = Vector3.one;

    [HideInInspector]
    public Vector3 NextRotate = Vector3.zero;

    [HideInInspector]
    public Vector2 NextCenter = Vector3.zero;

    [HideInInspector]
    public Vector3 AdjustPos = Vector3.zero;

    //[HideInInspector]
    public float FrameRate = CAST_FRAMERATE;

    [HideInInspector]
    public float DefFrameRate = CAST_FRAMERATE;

    [HideInInspector]
    public float NextAlpha = 1f;

    [HideInInspector]
    public float StartAlpha = 0f;

    //[HideInInspector]
    public float MaxAlpha = 1f;

    [HideInInspector]
    public CutInOut SetCutScene = CutInOut.Cutin;

    [HideInInspector]
    public bool NoWait = false;

    [HideInInspector]
    public bool NotSetParent = false;

    [HideInInspector]
    public bool SetDestroy = false;

    [HideInInspector]
    public bool SetActive = true;

    [HideInInspector]
    public bool ReFade = false;

    [HideInInspector]
    public bool SetFollowParent = true;

    [HideInInspector]
    public bool Clear = false;

    [HideInInspector]
    //auto destroy when fade in.
    public bool AutoDestroy = false;

    [HideInInspector]
    //pause animation.
    public bool PauseAni = false;

    [HideInInspector]
    public bool Init = false;

    [HideInInspector]
    public bool IsFinish = false;

    Vector3 m_Position = Vector3.zero;

    protected virtual void Awake()
    {
        //vInit();
    }

    public void vInit(bool forceInit = false)
    {
        if (Init && !forceInit)
            return;

        m_Status = new List<StatusAnimate>();

        //if (name.Contains("Goomba"))
          //Debug.Log(transform .name+ " = " + transform.localPosition);

        Vector3 lastPosition = transform.localPosition;

        if (GetComponent<RectTransform>() != null)
            lastPosition = GetComponent<RectTransform>().anchoredPosition;

        /*vSetUpValue(Mode.MOVE, transform.localPosition);
        vSetUpValue(Mode.SCALE, transform.localScale);
        vSetUpValue(Mode.ROTATE, transform.localEulerAngles);
        vSetUpValue(Mode.NONE, Vector3.zero);*/

        m_Position = transform.localPosition;

        transform.localPosition = lastPosition;
        Init = true;

        if (GetComponent<RectTransform>() != null)
            GetComponent<RectTransform>().anchoredPosition = lastPosition;
    }

    // Use this for initialization
    protected virtual void Start() {
        vInit();
    }

    //fix to set scale time to 0, anim not play.
    public void vSetAniToCoroutine() 
    {
        StartCoroutine(ieUpdate());
    }

    public void vSetUpValue(Mode mode, float setValue, bool loop = false,bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        vSetUpValue(mode,new Vector3(setValue, m_Status[(int)mode].nowValue.y, m_Status[(int)mode].nowValue.z), loop, reloop, outMode);
    }

    public void vSetUpValue(Mode mode, Vector3 setValue, bool loop = false,bool reloop = false, OutMode outMode = OutMode.NONE) {

        StatusAnimate statusAnim = new StatusAnimate();

        statusAnim.aniMode = mode;

        statusAnim.startValue = setValue;
        statusAnim.nowValue = setValue;
        statusAnim.targetValue = setValue;

        statusAnim.loop = loop;
        statusAnim.reLoop = reloop;
        statusAnim.outMode = outMode;

        //set to end animate.
        statusAnim.frame = statusAnim.frameMax;

        statusAnim.frame = statusAnim.delay = Vector3.zero;

        //set to finish.
        statusAnim.finish = true;
        
        if (mode == Mode.MOVE)
        {
            m_Position = setValue;
            transform.localPosition = setValue;
        }

        m_Status.Add(statusAnim);
    }

    // Update is called once per frame
    protected virtual void Update () {

        if (!PauseAni)
        {
            for (int i = 0; i < m_Status.Count; i++)
            {
                if (!m_Status[i].finish || m_Status[i].loop)
                {
                    if (m_Status[i].aniMode == Mode.FADE)
                        vFade(i);
                    else
                        vSetFrameAni(i);
                }
            }
            
            for (int i = 0; i < m_Status.Count; i++)
            {
                if (m_Status[i].finish && !m_Status[i].loop)
                {
                    m_Status.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }
	}

    IEnumerator ieUpdate() 
    {
        while (Mathf.Approximately(Time.timeScale,0f))
        {
            Update();
            yield return null;
        }
    }

    //fix clone animation to copy status animate.
    public void vCloneStatus(AniImage aniObj) 
    {
        for(int i=0;i< m_Status.Count;i++)
            aniObj.m_Status[i] = m_Status[i];
    }

    public void AniEnd()
    {
        GameObject objEnd = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        objEnd.transform.SetParent(transform);
        objEnd.transform.localScale = transform.localScale;
        objEnd.GetComponent<Image>().raycastTarget = false;
        AniImage aniEnd = objEnd.GetComponent<AniImage>();

        aniEnd.vSetScale(objEnd.transform.localScale.x, objEnd.transform.localScale.x * 2f);

        //auto destroy ani.
        aniEnd.FrameRate = 60f;
        aniEnd.vSetCutScene(CutInOut.Cutout, false, true);
    }

    //argMoveX
    public void vSetMove(Vector3 startValue, Vector3 targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue.x, targetValue.x, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.X, delay / 60f, loop, reloop, 1, outMode);
        vSetUp(startValue.y, targetValue.y, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.Y, delay / 60f, loop, reloop, 1, outMode);
        vSetUp(startValue.z, targetValue.z, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.Z, delay / 60f, loop, reloop, 1, outMode);
    }

    //argMoveX
    public void vSetMoveX(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false,bool reloop = false, OutMode outMode = OutMode.NONE) 
    {
        if (!Init)
            vInit();

        vSetUp( startValue, targetValue, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.X, delay / 60f, loop, reloop, 1, outMode);
	}
    //argMoveY
    public void vSetMoveY(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE) 
    {
        if (!Init)
            vInit();

        vSetUp( startValue, targetValue, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.Y, delay / 60f, loop, reloop, 1, outMode);
	}

    public void vSetMoveZ(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.MOVE, speedType, Group.Z, delay / 60f, loop, reloop, 1, outMode);
    }
    //argScale
    public void vSetScale(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.SCALE, speedType, Group.XYZ, delay / 60f, loop, reloop, 1, outMode);
    }

    public void vSetScaleTarget(float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(transform.localScale.x, targetValue, 0, maxFrame / 60f, Mode.SCALE, speedType, Group.XYZ, delay / 60f, loop, reloop, 1, outMode);
    }

    public void vSetScaleLoopDefault(float startValue, float targetValue, bool reloop = false)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, 1f, Mode.SCALE, AnimPattern.SinRapidStartSlowEnd, Group.XYZ, 0f, true, reloop, 1, OutMode.NONE);
    }

    public void vSetScaleLoop(float startValue, float targetValue, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, 1f, Mode.SCALE, AnimPattern.SinRapidStartSlowEnd, Group.XYZ, delay / 60f, loop, reloop, 1, outMode);
    }

    public void vSetScaleX(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.SCALE, speedType, Group.X, delay / 60f, loop, reloop, 1, outMode);
    }

    public void vSetScaleY(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.SCALE, speedType, Group.Y, delay / 60f, loop, reloop, 1, outMode);
    }
    //argRot
    //(float startValue, float targetValue, bool reloop = false)
    public void vSetRotateLoopDefault(float startValue, float targetValue, bool reloop = false)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, 1f, Mode.ROTATE, AnimPattern.SinRapidStartSlowEnd, Group.Z, 0f, true, reloop, 1, OutMode.NONE);
    }

    public void vSetRotate(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.ROTATE, speedType, Group.Z, delay / 60f, loop, reloop, 1, outMode);
    }

    float[] target = new float[] { 0.05f, 0.05f, 0.25f, 0.375f, 0.5f};
    //argVibeX
    public void vSetVibeX(int level, float startFrame, float endFrame, float maxFrame = 60f, float delay = 0f, bool loop = false, bool reloop = false)
    {
        if (!Init)
            vInit();

        float mutilpy = 100f; //(GetComponent<Image>() == null) ? 100f :
        AnimPattern speedType = AnimPattern.Vibe;
        vSetUp(0, target[level] * mutilpy, startFrame / maxFrame, endFrame / maxFrame, Mode.VIBE, speedType, Group.X, delay / 60f, loop, reloop);
    }
    //argVibeY
    public void vSetVibeY(int level, float startFrame, float endFrame, float maxFrame = 60f, float delay = 0f, bool loop = false, bool reloop = false)
    {
        if (!Init)
            vInit();
        float mutilpy = 100f;//(GetComponent<Image>() == null) ? 1f : 
        AnimPattern speedType = AnimPattern.Vibe;
        vSetUp(0, target[level] * mutilpy, startFrame / maxFrame, endFrame / maxFrame, Mode.VIBE, speedType, Group.Y, delay / 60f, loop, reloop);
    }
    //use at button.
    public void vSetVibeXDefault(int level)
    {
        vSetVibeX(level, 0f, 30f);
    }

    //argNoWait
    public void vSetNoWait(bool noWait)
    {
        NoWait = noWait;
    }

    public Vector3 veGetPosition() 
    {
        return m_Position;
    }
    //argPos
    public void vSetPos(Vector3 pos,bool setLocal = true,bool resetValue = true) 
    {
        if (!Init)
            vInit();

        m_Position = pos;
        if (setLocal)
            transform.localPosition = m_Position + AdjustPos;
        else
            transform.position = pos;

        //reset value.
        if (resetValue)
        {
            //to stop move animate.
            for (int i = 0; i < m_Status.Count; i++)
            {
                if (m_Status[i].aniMode == Mode.MOVE)
                {
                    StatusAnimate status = m_Status[i];
                    status.nowValue = pos;
                    status.startValue = status.nowValue;
                    status.targetValue = status.nowValue;
                    status.frame = status.frameMax;
                    m_Status[i] = status;
                }
            }

            if (m_Status.Count == 0)
            {
                StatusAnimate status = new StatusAnimate();
                status.nowValue = pos;
                status.startValue = status.nowValue;
                status.targetValue = status.nowValue;
                status.frame = status.frameMax;
                m_Status.Add(status);
            }
        }
    }

    public Color coGetColor() 
    {
        Color setColor = AniColor;

        if (gameObject.GetComponent<Image>() != null)
            setColor = gameObject.GetComponent<Image>().color;
        else if (gameObject.GetComponent<Text>() != null)
            setColor = gameObject.GetComponent<Text>().color;
        else if (gameObject.GetComponent<SpriteRenderer>() != null)
            setColor = gameObject.GetComponent<SpriteRenderer>().color;
        else if (gameObject.GetComponent<TextMeshProUGUI>() != null)
            setColor = gameObject.GetComponent<TextMeshProUGUI>().color;
        else if (gameObject.GetComponent<MeshRenderer>() != null)
            setColor = gameObject.GetComponent<MeshRenderer>().material.color;

        return setColor;
    }
    public void vSetColor(Color setColor)
    {
        if (gameObject.GetComponent<Image>() != null)
            gameObject.GetComponent<Image>().color = setColor;
        else if (gameObject.GetComponent<Text>() != null)
            gameObject.GetComponent<Text>().color = setColor;
        else if (gameObject.GetComponent<SpriteRenderer>() != null)
            gameObject.GetComponent<SpriteRenderer>().color = setColor;
        else if (gameObject.GetComponent<TextMeshProUGUI>() != null)
            gameObject.GetComponent<TextMeshProUGUI>().color = setColor;
        else if (gameObject.GetComponent<MeshRenderer>() != null)
            gameObject.GetComponent<MeshRenderer>().material.color = setColor;

        AniColor = setColor;
    }

    //argRGB
    public void vSetRGB(float red,float green,float blue) 
    {
        Color setColor = coGetColor();

        setColor.r = red;
        setColor.g = green;
        setColor.b = blue;

        vSetRGBA(setColor);
    }

    public void vSetRGBA(Color setColor)
    {
        vSetColor(setColor);

        if (!NotSetParent)
            vSetChildColor(setColor, false);
    }

    public void vSetValue(float startValue, float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0f, bool loop = false, bool reloop = false, OutMode outMode = OutMode.NONE)
    {
        vSetUp(startValue, targetValue, 0, maxFrame / 60f, Mode.NONE, speedType, Group.X, delay / 60f, loop, reloop, 1, outMode);
    }

    //
    public float fGetTargetValue(Mode mode, Group numGroup = Group.X)
    {
        switch (numGroup) 
        {
            case Group.X:
                return m_Status[(int)mode].targetValue.x;
            case Group.Y:
                return m_Status[(int)mode].targetValue.y;
            case Group.Z:
                return m_Status[(int)mode].targetValue.z;
        }  

        return m_Status[(int)mode].targetValue.x;
    }

    public void vResetPosition() {

		vSetNowValue(Mode.MOVE, transform.localPosition.x);
		vSetNowValue(Mode.MOVE, transform.localPosition.y, Group.Y);
		vSetTargetValue(Mode.MOVE, transform.localPosition.x);
		vSetTargetValue(Mode.MOVE, transform.localPosition.y, Group.Y);
	}

	public bool bGetFinish(Mode mode) 
    {
        //if (mode == Mode.ROTATE)
        //  Debug.Log(gameObject.name+" = "+ m_Status[(int)mode].frame+","+ m_Status[(int)mode].frameMax+","+ gameObject.transform.localEulerAngles.z+","+ m_Status[(int)mode].targetValue);
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                return m_Status[i].frame.x >= m_Status[i].frameMax.x &&
                m_Status[i].frame.y >= m_Status[i].frameMax.y &&
                m_Status[i].frame.z >= m_Status[i].frameMax.z;
            }
        }
        return true;
	}

    public bool bGetFinishMode(Mode mode)
    {
        //return m_Status[(int)mode].finish;

        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                return m_Status[i].finish;
            }
        }
        return true;
    }

    public void vSetFinish(bool finish)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            StatusAnimate status = m_Status[i];

            status.finish = finish;
            //fix to finish is same frame.
            if (finish)
            {
                status.frame = status.frameMax;
                status.loop = false;
            }

            m_Status[i] = status;
        }
    }

    public void vSetFinishMode(Mode mode,bool finish)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                StatusAnimate status = m_Status[i];
                status.finish = finish;
                //fix to finish is same frame.
                if (finish)
                {
                    status.frame = status.frameMax;
                    status.loop = false;
                }
                m_Status[i] = status;
            }
        }
    }

    public bool bGetFinish()
    {
        if (NoWait)
            return true;

        for (int i = 0; i < m_Status.Count; i++)
            if (!m_Status[i].finish && !bGetFinish((Mode)i))//m_Status[i].frame != m_Status[i].frameMax)
                return false;

        return true;
    }

    void vFade(int index) {

        //if frame rate drop not play animate.
        if (Time.deltaTime >= 0.33f)
            return;

        float deltaTime = (Time.deltaTime > 0) ? Time.deltaTime : Time.fixedDeltaTime;

        StatusAnimate status = m_Status[index];

        if (status.delay.x > 0)
            status.delay.x -= deltaTime;
        else if (status.frame.x < status.frameMax.x) 
        {

			Color setColor = coGetColor();
            Vector3 nowValue = status.nowValue;
            Vector3 startValue = status.startValue;
            Vector3 targetValue = status.targetValue;

            status.frame.x += deltaTime;

            m_Status[index] = status;

            vSetAnimate(index, ref nowValue.x, Group.X);// setColor.a

            setColor.a = nowValue.x;
            
            if (!Mathf.Approximately( StartColor.r, TargetColor.r))
            {
                float calColor = TargetColor.r - StartColor.r;
                float nowColor = calColor  * status.frame.x / status.frameMax.x;
                setColor.r = StartColor.r + nowColor;
            }

            if (!Mathf.Approximately(StartColor.g, TargetColor.g))
            {
                float calColor =TargetColor.g - StartColor.g;
                float nowColor = calColor * status.frame.x / status.frameMax.x;
                setColor.g = StartColor.g + nowColor;
            }

            if (!Mathf.Approximately(StartColor.b, TargetColor.b))
            {
                float calColor = TargetColor.b - StartColor.b;
                float nowColor = calColor * status.frame.x / status.frameMax.x;
                setColor.b = StartColor.b + nowColor;
            }

            //update when animate running.
            vSetColor(setColor);

            if (!NotSetParent)
                vSetChildColor(setColor);

            status.nowValue = nowValue;
        }
        else if (status.frame.x >= status.frameMax.x)
        {
            //Debug.Log("m_Status[(int)Mode.FADE].frame = "+ m_Status[(int)Mode.FADE].frame+","+ m_Status[(int)Mode.FADE].frameMax + "," + StartColor + "," + TargetColor);
            if (m_Status[index].loop)
            {
                if (m_Status[index].reLoop)
                {
                    Vector3 targetValue = status.targetValue;
                    status.nowValue = targetValue;
                    status.targetValue = status.startValue;
                    status.startValue = targetValue;
                    Color targetColor = TargetColor;
                    TargetColor = StartColor;
                    StartColor = targetColor;
                }
                else
                {
                    status.nowValue = m_Status[index].startValue;
                }
                status.frame.x = 0f;
            }
            else
            {
                status.nowValue = m_Status[index].targetValue;
                status.finish = true;
            }

            if (status.finish)
            {
                gameObject.SetActive(SetActive);
                IsFinish = bGetFinish();
            }

            if (Clear)
            {
                status.frame = status.frameMax;
                status.finish = true;
                Clear = false;
            }

            if (SetDestroy && status.finish)
                vDestroy();

            if (AutoDestroy)
            {
                vSetCutScene(CutInOut.Cutout, false, true);
            }
        }

        m_Status[index] = status;
    }

    public void vSetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    void vSetFrameAni(int index) 
    {
        //if frame rate drop not play animate.
        if (Time.deltaTime >= 0.33f)
            return;

        float deltaTime = (Time.deltaTime > 0) ? Time.deltaTime : Time.fixedDeltaTime;

        StatusAnimate status = m_Status[index];

        //0 = x, 1 = y, 2 = z
        for (int i = 0; i < 3; i++)
        {
            float delay = i == 0 ? status.delay.x : i == 1 ? status.delay.y : status.delay.z;
            float frame = i == 0 ? status.frame.x : i == 1 ? status.frame.y : status.frame.z;
            float frameMax = i == 0 ? status.frameMax.x : i == 1 ? status.frameMax.y : status.frameMax.z;
            float nowValue = i == 0 ? status.nowValue.x : i == 1 ? status.nowValue.y : status.nowValue.z;
            float startValue = i == 0 ? status.startValue.x : i == 1 ? status.startValue.y : status.startValue.z;
            float targetValue = i == 0 ? status.targetValue.x : i == 1 ? status.targetValue.y : status.targetValue.z;

            if (delay > 0)
                delay -= deltaTime;
            else if (frame < frameMax)
                frame += deltaTime;
            else if (frame >= frameMax)
            {
                frame = frameMax;
                if (status.loop)
                {
                    if (status.reLoop)
                    {
                        float lastTargetValue = targetValue;
                        nowValue = targetValue;
                        targetValue = startValue;
                        startValue = lastTargetValue;
                    }
                    else
                    {
                        nowValue = startValue;
                    }
                    frame = 0f;
                }
            }

            status.delay = new Vector3(i == 0 ? delay : status.delay.x, i == 1 ? delay : status.delay.y, i == 2 ? delay : status.delay.z);
            status.frame = new Vector3(i == 0 ? frame : status.frame.x, i == 1 ? frame : status.frame.y, i == 2 ? frame : status.frame.z);
            status.frameMax = new Vector3(i == 0 ? frameMax : status.frameMax.x, i == 1 ? frameMax : status.frameMax.y, i == 2 ? frameMax : status.frameMax.z);
            status.nowValue = new Vector3(i == 0 ? nowValue : status.nowValue.x, i == 1 ? nowValue : status.nowValue.y, i == 2 ? nowValue : status.nowValue.z);
            status.startValue = new Vector3(i == 0 ? startValue : status.startValue.x, i == 1 ? startValue : status.startValue.y, i == 2 ? startValue : status.startValue.z);
            status.targetValue = new Vector3(i == 0 ? targetValue : status.targetValue.x, i == 1 ? targetValue : status.targetValue.y, i == 2 ? targetValue : status.targetValue.z);
        }

        if (status.frame == status.frameMax)
        {
            status.nowValue = status.targetValue;
            status.finish = true;
            IsFinish = bGetFinish();
        }

        Vector3 calValue = status.aniMode == Mode.SCALE ? Vector3.one : Vector3.zero;

        switch (status.aniMode) 
        {
            case Mode.MOVE:
                calValue = transform.localPosition + AdjustPos;
                break;
            case Mode.ROTATE:
                calValue = transform.localEulerAngles;
                break;
            case Mode.SCALE:
                calValue = transform.localScale;

                //if(name.Contains("ImDino"))
                //  Debug.Log(name +" = "+ transform.localScale+","+ m_Status[(int)mode].nowValue.x);

                //if (name.Contains("ImDino") && mode == Mode.SCALE)
                  //  Debug.Log(name + " = " + transform.localScale + "," + m_Status[(int)mode].startValue + "," + m_Status[(int)mode].nowValue + "," + m_Status[(int)mode].targetValue+","+ Init);

                break;
            case Mode.VIBE:
                calValue = m_Position;
                break;
        };

        //0 = x, 1 = y, 2 = z
        for (int i = 0; i < 3; i++)
        {
            float nowValue = i == 0 ? status.nowValue.x : i == 1 ? status.nowValue.y : status.nowValue.z;
            float targetValue = i == 0 ? status.targetValue.x : i == 1 ? status.targetValue.y : status.targetValue.z;
            float lastCalValue = i == 0 ? calValue.x : i == 1 ? calValue.y : calValue.z;
            if (!Mathf.Approximately(nowValue, targetValue))
            {
                m_Status[index] = status;
                vSetAnimate(index, ref nowValue, (Group)i);
                if (status.aniMode != Mode.VIBE)
                    lastCalValue = nowValue;
                else
                    lastCalValue += nowValue;
            }
            else if (status.aniMode != Mode.VIBE)
            {
                lastCalValue = targetValue;
            }

            status.nowValue = new Vector3(i == 0 ? nowValue : status.nowValue.x, i == 1 ? nowValue : status.nowValue.y, i == 2 ? nowValue : status.nowValue.z);
            status.targetValue = new Vector3(i == 0 ? targetValue : status.targetValue.x, i == 1 ? targetValue : status.targetValue.y, i == 2 ? targetValue : status.targetValue.z);
            calValue = new Vector3(i == 0 ? lastCalValue : calValue.x, i == 1 ? lastCalValue : calValue.y, i == 2 ? lastCalValue : calValue.z);
        }

        /*if (name.Equals("LatinAmerica(Clone)"))
            if (calValue.x == -Mathf.Infinity)
            ;*/
        
        switch (status.aniMode)
        {
            case Mode.MOVE:
                transform.localPosition = calValue + AdjustPos;
                m_Position = calValue;// - AdjustPos;
                //if (name.Equals("LatinAmerica(Clone)"))
                  //  Debug.Log("localPosition = " + transform.localPosition+","+ calValue+","+ AdjustPos);
                break;
            case Mode.ROTATE:
                transform.localEulerAngles = calValue;
                break;
            case Mode.SCALE:
                transform.localScale = calValue;

                //if (name.Contains("ImDino") && mode == Mode.SCALE)
                  //  Debug.Log(name + " = " + transform.localScale + "," + m_Status[(int)mode].startValue + "," + m_Status[(int)mode].nowValue + "," + m_Status[(int)mode].targetValue + "," + Init);

                break;
            case Mode.VIBE:
                transform.localPosition = calValue;
                break;
        };

        

        m_Status[index] = status;
    }
    //set to event animation
    void vSetCutSceneEvent(CutInOut cutScene)
    {
        vSetCutScene(cutScene, cutScene == CutInOut.Cutin);
    }

    public void vSetCutSceneBt(bool setcutScene)
    {
        vSetCutScene(setcutScene ? CutInOut.Cutin : CutInOut.Cutout, setcutScene);
    }

    //set auto setActive gameobj. 
    public void vSetCutSceneAuto(CutInOut cutScene, bool setDestroy = false, float delay = 0f, bool setAni = false) 
    {
        vSetCutScene(cutScene, cutScene == CutInOut.Cutin, setDestroy, delay, setAni);
    }

    //set auto to check activeself
    public void vSetCutSceneAutoCheck(CutInOut cutScene, bool setDestroy = false, float delay = 0f, bool setAni = false)
    {
        if (cutScene == CutInOut.Cutin && !gameObject.activeSelf)
            vSetCutScene(cutScene, cutScene == CutInOut.Cutin, setDestroy, delay, setAni);
        else if (cutScene == CutInOut.Cutout && gameObject.activeSelf)
            vSetCutScene(cutScene, cutScene == CutInOut.Cutin, setDestroy, delay, setAni);
    }

    //argCutIn
    //argCutOut
    public void vSetCutScene(CutInOut cutScene, bool setActive = true, bool setDestroy = false, float delay = 0f, bool setAni = false) 
    {
        if (!Init)
            vInit();

        SetCutScene = cutScene;

		Color setColor = coGetColor();

		SetActive = setActive;

        if (setActive && !gameObject.activeSelf)
            gameObject.SetActive(true);

        if (cutScene == CutInOut.Cutin) {

            if (FrameRate > 1 || setAni || DefFrameRate > 1)
            {
                setColor.a = StartAlpha;
            }

			NextAlpha = MaxAlpha;

            vSetColor(setColor);

            if (!NotSetParent)
				vSetChildColor(setColor);

			if (FrameRate <= 1f && !setAni) {

				gameObject.SetActive(true);

				setColor.a = NextAlpha;

                vSetColor(setColor);

                if (!NotSetParent)
					vSetChildColor(setColor);
				return;
			} else if (gameObject.activeInHierarchy) {
                vSetUp( StartAlpha, MaxAlpha, 0, FrameRate / 60f, Mode.FADE, AnimPattern.SinRapidStartSlowEnd, 0, delay / 60f);
			}
		} else if (cutScene == CutInOut.Cutout) {

			SetDestroy = setDestroy;

			//not set color to maxAlpha.
			//setColor.a = MaxAlpha;

			NextAlpha = 0f;

            vSetColor(setColor);

            if (!NotSetParent)
				vSetChildColor(setColor);

			if (FrameRate <= 1f && !setAni) 
            {
				if(SetDestroy)
					vDestroy();
				else 
					gameObject.SetActive(setActive);

				return;
			} 
            else if (gameObject.activeInHierarchy)
            {
                vSetUp( setColor.a, 0, 0, FrameRate / 60f, Mode.FADE, AnimPattern.SinRapidStartSlowEnd, 0, delay / 60f);
			}
		}
	}

    public void vSetAlphaLoopDefault(float startValue, float targetValue,bool reloop = false)
    {
        if (!Init)
            vInit();

        vSetUp(startValue, targetValue, 0, 1f, Mode.FADE, AnimPattern.SinRapidStartSlowEnd, 0, 0f, true, reloop);
    }

    //argAlpha
    public void vSetAlpha(float targetValue, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0,bool loop = false, bool reloop = false) 
    {
        if (!Init)
            vInit();

        Color setColor = coGetColor();
        vSetUp(setColor.a, targetValue, 0, maxFrame / 60f, Mode.FADE, speedType, 0, delay / 60f, loop, reloop);
    }

    public void vSetAniColor(Color targetColor, float maxFrame = 60f, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, float delay = 0, bool loop = false, bool reloop = false)
    {
        if (!Init)
            vInit();

        Color setColor = coGetColor();
        StartColor = setColor;
        TargetColor = targetColor;
        vSetUp(setColor.a, targetColor.a, 0, maxFrame / 60f, Mode.FADE, speedType, 0, delay / 60f, loop, reloop);
    }


    public void vReset(Mode mode) {

        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                StatusAnimate status = m_Status[i];
                status.startValue = status.nowValue;
                status.targetValue = status.nowValue;
                m_Status[i] = status;
            }
        }
	}
		
	public void vSetUp( float startTarget, float targetValue, float startFrame, float endFrame, Mode mode, AnimPattern speedType = AnimPattern.SinRapidStartSlowEnd, Group numGroup = Group.X, float delay = 0, bool loop = false, bool reloop = false, int vibeLevel = 1, OutMode outMode = OutMode.NONE)
    {
        StatusAnimate status = new StatusAnimate();

        bool addStatus = true;
        int numStatus = 0;

        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                status = m_Status[i];
                addStatus = false;
                numStatus = i;
                break;
            }
        }

        status.aniMode = mode;
        status.speedType = speedType;
        status.aniId++;
        status.finish = false;

        IsFinish = false;

        if (numGroup == Group.X || numGroup == Group.XYZ)
        {
            status.delay.x = delay;
            status.frame.x = startFrame;//0f;
            status.frameMax.x = endFrame;//FrameRate / (60f * speed);
        }

        if (numGroup == Group.Y || numGroup == Group.XYZ)
        {
            status.delay.y = delay;
            status.frame.y = startFrame;//0f;
            status.frameMax.y = endFrame;//FrameRate / (60f * speed);
        }

        if (numGroup == Group.Z || numGroup == Group.XYZ)
        {
            status.delay.z = delay;
            status.frame.z = startFrame;//0f;
            status.frameMax.z = endFrame;//FrameRate / (60f * speed);
        }

        status.loop = loop;
        status.reLoop = reloop;
        status.vibeLevel = vibeLevel;
        status.outMode = outMode;

        Vector3 startPos = status.startValue;
		Vector3 nowPos = status.nowValue;
		Vector3 targetPos = status.targetValue;

        if (m_Status.Count == 0)
        {
            //fix default start to properly. 
            startPos = mode == Mode.SCALE ? Vector3.one : Vector3.zero;
            nowPos = mode == Mode.SCALE ? Vector3.one : Vector3.zero;
            targetPos = mode == Mode.SCALE ? Vector3.one : Vector3.zero;
        }

        //if(name.Contains("Page"))
        //  Debug.Log(name+" = " + startPos+","+nowPos+","+targetPos);

        if (numGroup == Group.X || numGroup == Group.XYZ) {
			
			startPos.x = startTarget;
			nowPos.x = startTarget;
			targetPos.x = targetValue;

            //set yz to now value.
            startPos.y = nowPos.y;
            startPos.z = nowPos.z;
        } 

        if (numGroup == Group.Y || numGroup == Group.XYZ)
        {
            startPos.y = startTarget;
			nowPos.y = startTarget;
			targetPos.y = targetValue;

            //set xz to now value.
            startPos.x = nowPos.x;
            startPos.z = nowPos.z;
        }

        if (numGroup == Group.Z || numGroup == Group.XYZ)
        {
            startPos.z = startTarget;
            nowPos.z = startTarget;
            targetPos.z = targetValue;

            //set xy to now value.
            startPos.x = nowPos.x;
            startPos.y = nowPos.y;
        }

        //fix bug to possition reset to zero when start animate.
        if (addStatus && mode == Mode.MOVE)
        {
            if (numGroup != Group.X)
                startPos.x = nowPos.x = targetPos.x = transform.localPosition.x;
            if (numGroup != Group.Y)
                startPos.y = nowPos.y = targetPos.y = transform.localPosition.y;
            if (numGroup != Group.Z)
                startPos.z = nowPos.z = targetPos.z = transform.localPosition.z;
        }

        status.startValue = startPos;
        status.nowValue = nowPos;
        status.targetValue = targetPos;

        //reset position at start
        Vector3 valueObj = mode == Mode.SCALE ? Vector3.one : Vector3.zero;
        switch (mode)
        {
            case Mode.MOVE:
                valueObj = transform.localPosition;
                //if (name.Contains("AniBg"))
                  //  Debug.Log(name + " = " + transform.localPosition);
                break;
            case Mode.SCALE:
                valueObj = transform.localScale;
                
                break;
            case Mode.ROTATE:
                valueObj = transform.localEulerAngles;
                break;
        };

        if (numGroup == Group.X || numGroup == Group.XYZ)
        {
            valueObj.x = startPos.x;
            
        }
        if (numGroup == Group.Y || numGroup == Group.XYZ)
        {
            valueObj.y = startPos.y;
            
        }
        if (numGroup == Group.Z)
        {
            valueObj.z = startPos.z;
        }

        switch (mode)
        {
            case Mode.MOVE:
                transform.localPosition = valueObj;
                break;
            case Mode.SCALE:
                transform.localScale = valueObj;
                break;
            case Mode.ROTATE:
                transform.localEulerAngles = valueObj;
                break;
        };
        if (addStatus)
            m_Status.Add(status);
        else
            m_Status[numStatus] = status;
    }

	public float fGetNowValue( Mode mode, Group numGroup = Group.X)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                switch (numGroup)
                {
                    case Group.X:
                        return m_Status[i].nowValue.x;
                    case Group.Y:
                        return m_Status[i].nowValue.y;
                    case Group.Z:
                        return m_Status[i].nowValue.z;
                    default:
                        return m_Status[i].nowValue.x;
                };
            }
        }

        return -1;
	}

	public void vSetNowValue( Mode mode, float nowValue, Group numGroup = Group.X)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                StatusAnimate status = m_Status[i];
                switch (numGroup)
                {
                    case Group.X:
                        status.nowValue.x = nowValue;
                        break;
                    case Group.Y:
                        status.nowValue.y = nowValue;
                        break;
                    case Group.Z:
                        status.nowValue.z = nowValue;
                        break;
                    default:
                        status.nowValue.x = nowValue;
                        break;
                };
                m_Status[i] = status;
            }
        }
	}

    public float fGetNowFrame(Mode mode, Group numGroup = Group.X)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                switch (numGroup)
                {
                    case Group.X:
                        return m_Status[i].frame.x;
                    case Group.Y:
                        return m_Status[i].frame.y;
                    case Group.Z:
                        return m_Status[i].frame.z;
                    default:
                        return m_Status[i].frame.x;
                };
            }
        }

        return -1;
    }

    public void vSetTargetValue( Mode mode, float targetValue, Group numGroup = Group.X)
    {
        for (int i = 0; i < m_Status.Count; i++)
        {
            if (m_Status[i].aniMode == mode)
            {
                StatusAnimate status = m_Status[i];
                switch (numGroup)
                {
                    case Group.X:
                        status.targetValue.x = targetValue;
                        break;
                    case Group.Y:
                        status.targetValue.y = targetValue;
                        break;
                    case Group.Z:
                        status.targetValue.z = targetValue;
                        break;
                    default:
                        status.targetValue.x = targetValue;
                        break;
                };
                m_Status[i] = status;
            }
        }
	}

	public void vDestroy() {

        /*if (this.GetComponent<GameCast>() != null)
            this.GetComponent<GameCast>().vDestroy();

        if (this.GetComponent<GamePic>() != null)
            this.GetComponent<GamePic>().vDestroy();

        if (this.GetComponent<GameBg>() != null)
            this.GetComponent<GameBg>().vDestroy();
            */
        Destroy (this.gameObject);
	}

	public void vSetAnimate( int index, ref float numValue, Group numGroup = Group.X) {

		StatusAnimate status = m_Status[index];

        float frame = status.frame.x;
        float frameMax = status.frameMax.x;
        float startValue = status.startValue.x;
        float targetValue = status.targetValue.x;

        bool check = false;

        if (numGroup == Group.X || numGroup == Group.XYZ)
        {
            check = true;
        }

        if (numGroup == Group.Y || numGroup == Group.XYZ)
        {
            frame = status.frame.y;
            frameMax = status.frameMax.y;
            startValue = status.startValue.y;
            targetValue = status.targetValue.y;
            check = true;
        }

        if (numGroup == Group.Z || numGroup == Group.XYZ)
        {
            frame = status.frame.z;
            frameMax = status.frameMax.z;
            startValue = status.startValue.z;
            targetValue = status.targetValue.z;
            check = true;
        }

        if (!check)
            return;

        if (frame < frameMax)
        {
            switch (status.speedType)
            {
                case AnimPattern.EqualSpeed:
                    numValue = AnimSystem.EqualSpeed(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.RapidStartSlowEnd:
                    numValue = AnimSystem.RapidStartSlowEnd(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.SinSlowEnd:
                    numValue = AnimSystem.SinSlowEnd(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.SinRapidStartSlowEnd:
                    numValue = AnimSystem.SinRapidStartSlowEnd(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.SlowStartSlowEnd:
                    numValue = AnimSystem.SlowStartSlowEnd(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.SlowStartRapidEnd:
                    numValue = AnimSystem.SlowStartRapidEnd(frame, frameMax, startValue, targetValue);
                    break;
                case AnimPattern.Vibe:
                    numValue = AnimSystem.Vibe(frame, frameMax, startValue, targetValue, status.vibeLevel);
                    break;
            }
        }
        else
        {
            if (numGroup == Group.X || numGroup == Group.XYZ)
                numValue = status.targetValue.x;
            if (numGroup == Group.Y || numGroup == Group.XYZ)
                numValue = status.targetValue.y;
            if (numGroup == Group.Z || numGroup == Group.XYZ)
                numValue = status.targetValue.z;
        }
	}

	public void vSetChildColor(Color setColor, bool alphaOnly = true) {

		foreach (Image image in gameObject.GetComponentsInChildren<Image>()) {

			AniImage aniImage = image.GetComponent<AniImage>();

			Color imColor = image.color;

			if(!ReFade)
				imColor.a = setColor.a;
			else 
				imColor.a = 1f - setColor.a;

            if (!alphaOnly) 
            {
                imColor.r = setColor.r;
                imColor.g = setColor.g;
                imColor.b = setColor.b;
            }

			if(aniImage != null && image.gameObject != gameObject) {
				if (imColor.a > aniImage.MaxAlpha) {
					imColor.a = aniImage.MaxAlpha;
				}
			}

			if(aniImage == null || aniImage.SetFollowParent)
				image.color = imColor;
		}

		foreach (Text text in gameObject.GetComponentsInChildren<Text>()) {

			Color textColor = text.color;

			if(!ReFade)
				textColor.a = setColor.a;
			else 
				textColor.a = 1f - setColor.a;

            if (!alphaOnly)
            {
                textColor.r = setColor.r;
                textColor.g = setColor.g;
                textColor.b = setColor.b;
            }

            AniImage aniImage = text.GetComponent<AniImage>();

			if(!ReFade)
				textColor.a = setColor.a;
			else 
				textColor.a = 1f - setColor.a;

			if(aniImage != null && text.gameObject != gameObject) {
				if (textColor.a > aniImage.MaxAlpha) {
					textColor.a = aniImage.MaxAlpha;
				}

				if(textColor.a <= 0f && text.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
					textColor.a = text.GetComponent<AniImage>().MaxAlpha;
			}

			if(aniImage == null || aniImage.SetFollowParent) {

				text.color = textColor;

				text.GetComponent<CanvasRenderer>().SetAlpha(textColor.a);
			}
		}

        foreach (RawImage rawImage in gameObject.GetComponentsInChildren<RawImage>())
        {

            AniImage aniImage = rawImage.GetComponent<AniImage>();

            Color imageColor = rawImage.color;

            if (!ReFade)
                imageColor.a = setColor.a;
            else
                imageColor.a = 1f - setColor.a;

            if (!alphaOnly)
            {
                imageColor.r = setColor.r;
                imageColor.g = setColor.g;
                imageColor.b = setColor.b;
            }

            if (aniImage != null && rawImage.gameObject != gameObject)
            {

                if (imageColor.a > aniImage.MaxAlpha)
                    imageColor.a = aniImage.MaxAlpha;

                if (imageColor.a <= 0f && aniImage.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
                    imageColor.a = aniImage.GetComponent<AniImage>().MaxAlpha;
            }

            if (aniImage == null || aniImage.SetFollowParent)
            {
                rawImage.color = imageColor;
            }
        }

        foreach (SpriteRenderer sprRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            AniImage aniImage = sprRenderer.GetComponent<AniImage>();

            Color imageColor = sprRenderer.color;

            if (!ReFade)
                imageColor.a = setColor.a;
            else
                imageColor.a = 1f - setColor.a;

            if (!alphaOnly)
            {
                imageColor.r = setColor.r;
                imageColor.g = setColor.g;
                imageColor.b = setColor.b;
            }

            if (aniImage != null && sprRenderer.gameObject != gameObject)
            {
                if (imageColor.a > aniImage.MaxAlpha)
                    imageColor.a = aniImage.MaxAlpha;

                if (imageColor.a <= 0f && aniImage.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
                    imageColor.a = aniImage.GetComponent<AniImage>().MaxAlpha;
            }

            if (aniImage == null || aniImage.SetFollowParent)
            {
                sprRenderer.color = imageColor;
            }
        }

        foreach (TextMeshProUGUI textMeshProGUI in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        {
            AniImage aniImage = textMeshProGUI.GetComponent<AniImage>();

            Color imageColor = textMeshProGUI.color;

            if (!ReFade)
                imageColor.a = setColor.a;
            else
                imageColor.a = 1f - setColor.a;

            if (!alphaOnly)
            {
                imageColor.r = setColor.r;
                imageColor.g = setColor.g;
                imageColor.b = setColor.b;
            }

            if (aniImage != null && textMeshProGUI.gameObject != gameObject)
            {
                if (imageColor.a > aniImage.MaxAlpha)
                    imageColor.a = aniImage.MaxAlpha;
                //fix text to flig.
                //if (imageColor.a <= 0f && aniImage.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
                  //  imageColor.a = aniImage.GetComponent<AniImage>().MaxAlpha;
            }

            if (aniImage == null || aniImage.SetFollowParent)
            {
                textMeshProGUI.color = imageColor;
            }
        }

        foreach (TextMeshPro textMeshPro in gameObject.GetComponentsInChildren<TextMeshPro>())
        {
            AniImage aniImage = textMeshPro.GetComponent<AniImage>();

            Color imageColor = textMeshPro.color;

            if (!ReFade)
                imageColor.a = setColor.a;
            else
                imageColor.a = 1f - setColor.a;

            if (!alphaOnly)
            {
                imageColor.r = setColor.r;
                imageColor.g = setColor.g;
                imageColor.b = setColor.b;
            }

            if (aniImage != null && textMeshPro.gameObject != gameObject)
            {
                if (imageColor.a > aniImage.MaxAlpha)
                    imageColor.a = aniImage.MaxAlpha;

                if (imageColor.a <= 0f && aniImage.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
                    imageColor.a = aniImage.GetComponent<AniImage>().MaxAlpha;
            }

            if (aniImage == null || aniImage.SetFollowParent)
            {
                textMeshPro.color = imageColor;
            }
        }

        foreach (MeshRenderer meshRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            AniImage aniImage = meshRenderer.GetComponent<AniImage>();

            if (meshRenderer.GetComponent<TextMeshPro>() == null && !meshRenderer.gameObject.name.Contains("TMP SubMesh"))
            {
                //Debug.Log(" = "+ meshRenderer.gameObject.name);
                Color imageColor = meshRenderer.material.color;

                if (!ReFade)
                    imageColor.a = setColor.a;
                else
                    imageColor.a = 1f - setColor.a;

                if (!alphaOnly)
                {
                    imageColor.r = setColor.r;
                    imageColor.g = setColor.g;
                    imageColor.b = setColor.b;
                }

                if (aniImage != null && meshRenderer.gameObject != gameObject)
                {
                    if (imageColor.a > aniImage.MaxAlpha)
                        imageColor.a = aniImage.MaxAlpha;

                    if (imageColor.a <= 0f && aniImage.GetComponent<AniImage>().MaxAlpha > 0f && NextAlpha <= 0f)
                        imageColor.a = aniImage.GetComponent<AniImage>().MaxAlpha;
                }

                if (aniImage == null || aniImage.SetFollowParent)
                {
                    meshRenderer.material.color = imageColor;
                }
            }
        }
    }
}
