using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AnimPattern
{
	SinSlowEnd,
	SinRapidStartSlowEnd,
	EqualSpeed,
	SlowStartRapidEnd,
	RapidStartSlowEnd,
	SlowStartSlowEnd,
	Vibe
}
public class AnimSystem : MonoBehaviour {

	enum eAnimSpd
	{				//		Prm1				/ Prm2
		Slow_Lv5,	// 強	ゆっくり動き始める	/ ゆっくり動き終える
		Slow_Lv4,	// ↑	ゆっくり動き始める	/ ゆっくり動き終える
		Slow_Lv3,	//		ゆっくり動き始める	/ ゆっくり動き終える
		Slow_Lv2,	// ↓	ゆっくり動き始める	/ ゆっくり動き終える
		Slow_Lv1,	// 弱	ゆっくり動き始める	/ ゆっくり動き終える
		NoAccel,	//		直線的な動きをする
		Rapid_Lv1,	// 弱	急に動き始める		/ 急に動き終える
		Rapid_Lv2,	// ↑	急に動き始める		/ 急に動き終える
		Rapid_Lv3,	//		急に動き始める		/ 急に動き終える
		Rapid_Lv4,	// ↓	急に動き始める		/ 急に動き終える
		Rapid_Lv5,	// 強	急に動き始める		/ 急に動き終える
	};

	public struct animWork
	{
		// 現在の値
		public float				value;
		// 開始値
		public float				startValue;
		// 終了値
		public float				targetValue;

		// 全フレーム数
		public int					maxFrame;
		// 現在のフレーム数
		public int					curFrame;
		// アニメーション速度
		public int					m_speed;
		// アニメパターン
		public AnimPattern		pattern;
		// バイブが左右に揺れる強さ(0～3)
		public int					vibeLevel;
		// リバースループ時、0～1 なら false、1～0 なら true
		public bool				reverseValue;

		// アニメーション終了時のコールバック
		//animEndCallback		endCallback;
		// アニメーション終了時のコールバック引数
		//void*				endCallbackArg;

		// true .. ループ	0 ～ 1/0 ～ 1/0 ～ 1　…
		public bool				isLoop;
		// true .. リバースループ（行ったり来たりループ）	0 ～ 1 ～　0 ～ 1 ～　…
		public bool				isRevLoop;
		// true .. アニメ終了
		public bool				isEnd;
		// true .. バイブモード
		public bool				isVibe;
		// true .. 停止中
		public bool				isStop;
		// true .. 今回値をゲットした（ゲットしなければ更新しない）
		public bool				isGet;

		public float 			lastDeg;

	};

	static float val;
    static float rad;
    static float deg;

	static int[][] nPasTgl = new int[12][];
    static float[] __y = new float[4];

    static bool s_init = false;

    static float s_accelGet(float y1, float y2, float t, int n)
	{

        if (!s_init)
            sInit();

		float b = t > 1 ? 1 : (t < 0 ? 0 : t);
		float a = 1 - b;
		float ay = 0;
        __y[0] = 0;
        __y[1] = y1;
        __y[2] = y2;
        __y[3] = 1;
		int m;
		for (int i = 0; i <= n; i++)
		{
			m = i==0 ? 0 : (i==n ? 3 : (i <= n / 2 ? 1 : 2)); //yの添え字決定
			ay += ((float)nPasTgl[n][i]) * Mathf.Pow (a,n-i) * Mathf.Pow (b,i) * __y[m]; //※1
		}
		return ay;
	}

	static float s_accelGet(eAnimSpd ePrm1, eAnimSpd ePrm2, float fRate)
	{
		// n次元指定
		int		n  = 3;
		float	y1 = 0f, y2 = 0f;
		switch (ePrm1)
		{
			case eAnimSpd.Slow_Lv5  : y1 = 0;          n = 11;break; //11次元
			case eAnimSpd.Slow_Lv4  : y1 = 0;          n = 9; break; //9次元
			case eAnimSpd.Slow_Lv3  : y1 = 0;          n = 7; break; //7次元
			case eAnimSpd.Slow_Lv2  : y1 = 0;          n = 5; break; //5次元
			case eAnimSpd.Slow_Lv1  : y1 = 0;          n = 3; break; //3次元
			case eAnimSpd.NoAccel   : y1 = 0.333333f;  n = 3; break; //直線の場合は3次元中1/3の点
			case eAnimSpd.Rapid_Lv1 : y1 = 1;          n = 3; break; //3次元
			case eAnimSpd.Rapid_Lv2 : y1 = 1;          n = 5; break; //5次元
			case eAnimSpd.Rapid_Lv3 : y1 = 1;          n = 7; break; //7次元
			case eAnimSpd.Rapid_Lv4 : y1 = 1;          n = 9; break; //9次元
			case eAnimSpd.Rapid_Lv5 : y1 = 1;          n = 11;break; //11次元
		}
		switch (ePrm2)
		{
			case eAnimSpd.Slow_Lv5  : y2 = 1;          	n = 11;break; //11次元
			case eAnimSpd.Slow_Lv4  : y2 = 1;         	n = 9; break; //9次元
			case eAnimSpd.Slow_Lv3  : y2 = 1;         	n = 7; break; //7次元
			case eAnimSpd.Slow_Lv2  : y2 = 1;          	n = 5; break; //5次元
			case eAnimSpd.Slow_Lv1  : y2 = 1;          	n = 3; break; //3次元
			case eAnimSpd.NoAccel   : y2 = 0.6666667f;  n = 3; break; //直線の場合は3次元中2/3の点
			case eAnimSpd.Rapid_Lv1 : y2 = 0;          	n = 3; break; //3次元
			case eAnimSpd.Rapid_Lv2 : y2 = 0;          	n = 5; break; //5次元
			case eAnimSpd.Rapid_Lv3 : y2 = 0;          	n = 7; break; //7次元
			case eAnimSpd.Rapid_Lv4 : y2 = 0;          	n = 9; break; //9次元
			case eAnimSpd.Rapid_Lv5 : y2 = 0;          	n = 11;break; //11次元
		}
		return s_accelGet (y1, y2, fRate, n);
	}

	AnimPattern m_eAnimPattern;
	eAnimSpd     m_eAnimSpd;

	private static AnimSystem instance = null;
	public  static AnimSystem Instance 
	{
		get { return instance; }
	}

	void Awake ()
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
			return;
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

	}

    public static void sInit() 
    {
        for (int i = 0; i < nPasTgl.Length; i++)
            nPasTgl[i] = new int[i + 1];

        nPasTgl[0] = new int[1] { 1 };
        nPasTgl[1] = new int[2] { 1, 1 };
        nPasTgl[2] = new int[3] { 1, 2, 1 };
        nPasTgl[3] = new int[4] { 1, 3, 3, 1 };
        nPasTgl[4] = new int[5] { 1, 4, 6, 4, 1 };
        nPasTgl[5] = new int[6] { 1, 5, 10, 10, 5, 1 };
        nPasTgl[6] = new int[7] { 1, 6, 15, 20, 15, 6, 1 };
        nPasTgl[7] = new int[8] { 1, 7, 21, 35, 35, 21, 7, 1 };
        nPasTgl[8] = new int[9] { 1, 8, 28, 56, 70, 56, 28, 8, 1 };
        nPasTgl[9] = new int[10] { 1, 9, 36, 84, 126, 126, 84, 36, 9, 1 };
        nPasTgl[10] = new int[11] { 1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1 };
        nPasTgl[11] = new int[12] { 1, 11, 55, 165, 330, 464, 464, 330, 165, 55, 11, 1 };

        s_init = true;
    }

	public static float SinSlowEnd (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		val = curFrame * (90.0f / maxFrame);
		rad = val * (Mathf.PI / 180);
		deg = Mathf.Sin(rad);

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float SinRapidStartSlowEnd (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		val = 90.0f + curFrame * (180.0f / maxFrame);
		rad = val * (Mathf.PI / 180);
		deg = (1.0f - Mathf.Sin(rad)) / 2.0f;

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float EqualSpeed (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		deg = s_accelGet(eAnimSpd.NoAccel, eAnimSpd.NoAccel, curFrame / maxFrame);

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float SlowStartRapidEnd (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		deg = s_accelGet(eAnimSpd.Slow_Lv5, eAnimSpd.Rapid_Lv1, curFrame / maxFrame);

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float RapidStartSlowEnd (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		deg = s_accelGet(eAnimSpd.Rapid_Lv4, eAnimSpd.Slow_Lv2, curFrame / maxFrame);

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float SlowStartSlowEnd (float curFrame, float maxFrame, float startValue, float targetValue)
	{
		deg = s_accelGet(eAnimSpd.Slow_Lv5, eAnimSpd.Slow_Lv5, curFrame / maxFrame);

		float  myVal = startValue + (targetValue - startValue) * deg;
		return myVal;
	}

    public static float Vibe (float curFrame, float maxFrame, float startValue, float targetValue, int vibeLevel) // vibeLevel 0-3
	{
		float[] vibe_strcoef = new float[] {3, 7, 10, 14};
		float   vibe_str     = vibe_strcoef[vibeLevel];
		val = curFrame * (360.0f * vibe_str / maxFrame);
		int round = (int)val / 360;
		val = val % 360;

		float width = targetValue * (vibe_str - round) / vibe_str;
		rad = val * (Mathf.PI / 180);
		deg = Mathf.Sin(rad);

		float  myVal = startValue + width * deg;
		return myVal;
	}

    public static float GetValByType (AnimPattern pat, float curFrame, float maxFrame, float startValue, float targetValue)
	{
		float ret = 0;

		switch (pat)
		{
		case AnimPattern.EqualSpeed : 				ret = EqualSpeed(curFrame, maxFrame, startValue, targetValue);				break;
		case AnimPattern.RapidStartSlowEnd : 		ret = RapidStartSlowEnd(curFrame, maxFrame, startValue, targetValue);		break;
		case AnimPattern.SinRapidStartSlowEnd : 	ret = SinRapidStartSlowEnd(curFrame, maxFrame, startValue, targetValue);	break;
		case AnimPattern.SinSlowEnd : 				ret = SinSlowEnd(curFrame, maxFrame, startValue, targetValue);				break;
		case AnimPattern.SlowStartRapidEnd : 		ret = SlowStartRapidEnd(curFrame, maxFrame, startValue, targetValue);		break;
		case AnimPattern.SlowStartSlowEnd : 		ret = SlowStartSlowEnd(curFrame, maxFrame, startValue, targetValue);		break;
		}

		return ret;
	}
}
