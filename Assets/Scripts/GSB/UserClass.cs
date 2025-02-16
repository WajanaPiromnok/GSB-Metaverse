
using UnityEngine;

[System.Serializable]
public class ReqRegister
{
	//public string email;
	//public string password;
	//public string phone;

	public ReqRegisterUser user;

	public static RegisterClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<RegisterClass>(jsonString);
	}
}

[System.Serializable]
public class ReqRegisterUser
{
	public string email;
	public string password;
	public string phone;
}


[System.Serializable]
public class RegisterClass
{
	public int status_code;
	public string message;
	public string authen_code;
	/*
	public string id;
	public string email;
	public string phone;
	public string fullname;
	public string nickname;
	public string gender;
	public string bio;*/

	public static RegisterClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<RegisterClass>(jsonString);
	}
}

[System.Serializable]
public class ReqLogin
{
	public ReqLoginUser user;

	public static ReqLogin CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ReqLogin>(jsonString);
	}
}

[System.Serializable]
public class ReqLoginUser
{
	public string email;
	public string password;
}

[System.Serializable]
public class ResLoginClass
{
	public int status_code;
	public string message;
	public string id;
	//public string email;
	//public string fullname;
	//public string nickname;
	//public string gender;
	//public string phone;
	//public string bio;

	public static ResLoginClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ResLoginClass>(jsonString);
	}
}

[System.Serializable]
public class ResRequestResetPassword
{
	public int status_code;
	public string message;
	public string id;

	public static ResRequestResetPassword CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ResRequestResetPassword>(jsonString);
	}
}

[System.Serializable]
public class ReqResetPassword
{
	public ReqUserResetPassword user;

	public static ReqResetPassword CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ReqResetPassword>(jsonString);
	}
}

[System.Serializable]
public class ReqUserResetPassword
{
	public string id;
	public string password;
}

[System.Serializable]
public class ResResetPassword
{
	public int status_code;
	public string message;

	public static ResResetPassword CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ResResetPassword>(jsonString);
	}
}

[System.Serializable]
public class GetCoinClass
{
	public int status_code;
	public string message;
	public GetUserCoinClass user;

	public static GetCoinClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<GetCoinClass>(jsonString);
	}
}

[System.Serializable]
public class GetUserCoinClass
{
	public int coin;
}

[System.Serializable]
public class ResError
{
	public int status_code;
	public string message;

	public static ResError CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ResError>(jsonString);
	}
}

[System.Serializable]
public class AddCoinClass
{
	public int status_code;
	public string message;
	public AddUserCoinClass user;

	public static AddCoinClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<AddCoinClass>(jsonString);
	}
}

[System.Serializable]
public class AddUserCoinClass
{
	public int coin;
}

[System.Serializable]
public class ResetCoinClass
{
	public int status_code;
	public string message;
	public ResetUserCoinClass user;

	public static ResetCoinClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ResetCoinClass>(jsonString);
	}
}

[System.Serializable]
public class ResetUserCoinClass
{
	public int coin;
}

[System.Serializable]
public class NFTClass
{
	public int status_code;
	public int chance;
	public string message;

	public static NFTClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<NFTClass>(jsonString);
	}
}

[System.Serializable]
public class RewordClass
{
	public int status_code;
	public int chance;
	public string message;

	public static RewordClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<RewordClass>(jsonString);
	}
}

[System.Serializable]
public class ForgetClass
{
	public int status_code;
	public string message;

	public static ForgetClass CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<ForgetClass>(jsonString);
	}
}




