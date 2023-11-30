using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System;
using Photon;
using Photon.Pun;
using StarterAssets;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class AvatarState
{
	public string skinCode = default(string);
	public string skinColorCode = default(string);
	public string hairCode = default(string);
	public string hairColorCode = default(string);
	public string faceCode = default(string);
	public string faceColorCode = default(string);
	public string topCode = default(string);
	public string topColorCode = default(string);
	public string bottomCode = default(string);
	public string bottomColorCode = default(string);
	public string shoesCode = default(string);
	public string shoesColorCode = default(string);
	public string nickName = default(string);

	public void Copy(AvatarState newstate)
	{
		skinCode = newstate.skinCode;
		skinColorCode = newstate.skinColorCode;
		hairCode = newstate.hairCode;
		hairColorCode = newstate.hairColorCode;
		faceCode = newstate.faceCode;
		faceColorCode = newstate.faceColorCode;
		topCode = newstate.topCode;
		topColorCode = newstate.topColorCode;
		bottomCode = newstate.bottomCode;
		bottomColorCode = newstate.bottomColorCode;
		shoesCode = newstate.shoesCode;
		shoesColorCode = newstate.shoesColorCode;
		nickName = newstate.nickName;
	}
}

public class CharctorMeshAndMaterialController : MonoBehaviourPun
{
	public SkinnedMeshRenderer[] originalSkinnedMeshRenderers;
	public SkinnedMeshRenderer[] skinnedMeshRendererList;
	public Material[] materialList;

	//private PhotonView photonView;

	public AvatarState state = new AvatarState();
	public TMP_Text nickName;

	private void Awake()
	{
		//photonView = GetComponent<PhotonView>();

		//if (photonView.InstantiationData != null)
		//{
		//	state = JsonUtility.FromJson<AvatarState>(photonView.InstantiationData[0].ToString());
		//	GetComponent<PlayerInput>().enabled = false;
		//}

		StartCoroutine(Init(false));//photonView.InstantiationData == null));
	}

	public IEnumerator Init(bool isInstantiationDataNull)
	{
		GameObject container = GameObject.Find("MeshMatContainer");
		while (container == null)
		{
			yield return new WaitForSeconds(0.5f);
			container = GameObject.Find("MeshMatContainer");
		}
		
		//Resources.LoadAll<Material>("material"); // 메테리얼 프리팹으로 만들어서 가져오는 법 새로 개발해야함 
		originalSkinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		skinnedMeshRendererList = container.GetComponentsInChildren<SkinnedMeshRenderer>();
		materialList = container.GetComponent<MaterialContainer>().materialList;
		Debug.Log("originalMeshRenderers : " + originalSkinnedMeshRenderers.Length +
		          "\nskinnedMeshRendererList : " + skinnedMeshRendererList.Length
		          + "\nmaterialList : " + materialList.Length);

		if (!isInstantiationDataNull)
		{
			CharacterSetting(state);
			//nickName.text = state.nickName;
			//Debug.Log("nickname set as : " + state.nickName);
		}
		else
		{
			if(!gameObject.name.Equals("CustomizingAvatar"))
				nickName.gameObject.SetActive(false);
		}
		yield return null;
	}

	#region UNITYC_CALLBACK


#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
        {
			
			SetRandomAvatarState();
			CharacterSetting(state);

		}
        //if (Input.GetKeyDown(KeyCode.Alpha1)) // bottom
        //{
        //    CharactorPartsChange("BOTTOM", "AB", "001");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    CharactorPartsChange("BOTTOM", "AA", "002");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3)) // skin
        //{
        //    CharactorPartsChange("SKIN", "AA", "", "#D13319");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    CharactorPartsChange("SKIN", "AA", "", "#FAE7D6");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    CharactorPartsChange("SKIN", "AA", "", "#D13319");
        //    CharactorPartsChange("TOP", "AA", "001");
        //    CharactorPartsChange("SHOES", "AA", "001");
        //    CharactorPartsChange("BOTTOM", "AA", "001");
        //    CharactorPartsChange("FACE", "AA", "002");
        //    CharactorPartsChange("HAIR", "AA", "001", "#434343");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    CharactorPartsChange("SKIN", "AA", "", "#FAE7D6");
        //    CharactorPartsChange("TOP", "AB", "001");
        //    CharactorPartsChange("SHOES", "AB", "001");
        //    CharactorPartsChange("BOTTOM", "AB", "001");
        //    CharactorPartsChange("FACE", "AA", "001");
        //    CharactorPartsChange("HAIR", "AB", "", "#434343");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha7))
        //{

        //    CharactorPartsChange("HAIR", "AC", "001", "#434343");
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha8))
        //{

        //    CharactorPartsChange("HAIR", "AD", "001", "#434343");
        //}
    }

	public void SetRandomAvatarState()
	{

		string[] ColorCode =
		{
		"#434343", // 0
        "#A43D3D",
		"#FFAE3D",
		"#0F6C3F",
		"#2B5B7B",
		"#D1A4E9",
		"#653625",
		"#B3CFCF", // 7

        "#FAE7D6", // 8
        "#F7DAC6",
		"#F5D5C2",
		"#F1D1B3",
		"#F0C8B4",
		"#EEC2AC",
		"#E7B49B",
		"#DBA58A",
		"#A17766",
		"#845C4E",
		"#614C39",
		"#443521",
		"#D13319",
		"#298A3A",
		"#4DB199",
		"#23759E", // 23
        "#4F2B18",
		"#463C33", // 25
        "#5E4834",
		"#81715E",
		"#675446",
		"#80593A", // 29

        "#603D2B",

	};

		string[] MeshCode =
		{
		"AA", //0
        "AB",
		"AC",
		"AD",
		"AE",
		"AF",
		"AG", //6
        "AH",
		"AI",
		"AJ"
	};
		/// ??? ??? ???? ???? ???
		string faceMat = Random.Range(1, 14).ToString("D3");
		string hairMesh = MeshCode[Random.Range(0, 7)];
		string bottomMesh = MeshCode[Random.Range(0, 4)];
		string bottomMat = bottomMesh == "AC" ? Random.Range(1, 7).ToString("D3") : Random.Range(1, 4).ToString("D3");
		string shoesMesh = MeshCode[Random.Range(0, 3)];
		string shoesMat = Random.Range(1, 5).ToString("D3");
		string topMesh = MeshCode[Random.Range(0, 5)];
		string topMat = (topMesh == "AA" || topMesh == "AB")
			? Random.Range(1, 5).ToString("D3")
			: Random.Range(1, 8).ToString("D3");
		string hairColor = ColorCode[Random.Range(0, 8)];
		string skinColor = ColorCode[12]; //Random.Range(8, 24)];

		/// 35?? ? ?? ?? ?? ??
		string hairMat = hairMesh != "AA" ? "001" : Random.Range(1, 4).ToString("D3");
		//switch (hairMesh)
		//{
		//    case "AA":
		//        switch (hairMat)
		//        {
		//            case "001":
		//                hairColor = ColorCode[25];
		//                break;
		//            case "002":
		//                hairColor = ColorCode[29];
		//                break;
		//            case "003":
		//                hairColor = ColorCode[24];
		//                break;
		//            case "004":
		//                hairColor = ColorCode[27];
		//                break;
		//        }

		//        break;
		//    case "AB":
		//        hairColor = ColorCode[24];
		//        break;
		//    case "AC":
		//        hairColor = ColorCode[25];
		//        break;
		//    case "AD":
		//        hairColor = ColorCode[26];
		//        break;
		//    case "AE":
		//        hairColor = ColorCode[27];
		//        break;
		//    case "AF":
		//        hairColor = ColorCode[28];
		//        break;
		//    case "AG":
		//        hairColor = ColorCode[29];
		//        break;
		//    case "AK":
		//        hairColor = ColorCode[30];
		//        break;
		//}
		/////

		state.hairCode = "HAIR_" + hairMesh + "_" + hairMat;
		state.hairColorCode = HairColorCodeSetter(state.hairCode);
		state.bottomCode = "BOTTOM_" + bottomMesh + "_" + bottomMat;
		state.bottomColorCode = "#000000";
		state.topCode = "TOP_" + topMesh + "_" + topMat;
		state.topColorCode = "#000000";
		state.shoesCode = "SHOES_" + shoesMesh + "_" + shoesMat;
		state.shoesColorCode = "#000000";
		state.faceCode = "FACE_AA_" + faceMat;
		state.faceColorCode = "#000000";
		state.skinCode = "SKIN_AA_001";
		state.skinColorCode = skinColor;
		state.nickName = "";

		/*
				state.hairCode = "HAIR_AD_001";
				state.hairColorCode = "#653625";
				state.bottomCode = "BOTTOM_AE_002";
				state.bottomColorCode = "#000000";
				state.topCode = "TOP_AG_002";
				state.topColorCode = "#000000";
				state.shoesCode = "SHOES_AD_002";
				state.shoesColorCode = "#000000";
				state.faceCode = "FACE_AA_001" ;
				state.faceColorCode = "#000000";
				state.skinCode = "SKIN_AA_001";
				state.skinColorCode = "#F0C8B4";
				state.nickName = "";
		*/
		//SetRandomizeNickName();
	}

	public string HairColorCodeSetter(string targetName)
	{
		string[] code = targetName.Split('_');
		string colorCode = "#000000";
		if (code[0].ToUpper() == "HAIR")
		{
			string[] ColorCode = {
				"#434343", // 0
                "#A43D3D",
				"#FFAE3D",
				"#0F6C3F",
				"#2B5B7B",
				"#D1A4E9",
				"#653625",
				"#B3CFCF", // 7

                "#FAE7D6", // 8
                "#F7DAC6",
				"#F5D5C2",
				"#F1D1B3",
				"#F0C8B4",
				"#EEC2AC",
				"#E7B49B",
				"#DBA58A",
				"#A17766",
				"#845C4E",
				"#614C39",
				"#443521",
				"#D13319",
				"#298A3A",
				"#4DB199",
				"#23759E", // 23
                "#4F2B18",
				"#463C33", // 25
                "#5E4834",
				"#81715E",
				"#675446",
				"#80593A", // 29
                "#603D2B"
			};
			switch (code[1])
			{
				case "AA":
					switch (code[2])
					{
						case "001":
							colorCode = ColorCode[25];
							break;
						case "002":
							colorCode = ColorCode[29];
							break;
						case "003":
							colorCode = ColorCode[24];
							break;
						case "004":
							colorCode = ColorCode[27];
							break;
					}
					break;
				case "AB":
					colorCode = ColorCode[24];
					break;
				case "AC":
					colorCode = ColorCode[25];
					break;
				case "AD":
					colorCode = ColorCode[26];
					break;
				case "AE":
					colorCode = ColorCode[27];
					break;
				case "AF":
					colorCode = ColorCode[28];
					break;
				case "AG":
					colorCode = ColorCode[29];
					break;
				case "AK":
					colorCode = ColorCode[30];
					break;
			}
		}

		return colorCode;
	}

	void OnDrawGizmosSelected()
	{
		var meshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		Vector3 before = meshrenderer.bones[0].position;
		for (int i = 0; i < meshrenderer.bones.Length; i++)
		{
			Gizmos.DrawLine(meshrenderer.bones[i].position, before);
			UnityEditor.Handles.Label(meshrenderer.bones[i].transform.position, i.ToString());
			before = meshrenderer.bones[i].position;
		}
	}
#endif

	#endregion

	public void CharactorPartsChange(string code, string color)
	{
		string[] splitCodes = code.Split('_');
		if( string.IsNullOrEmpty(splitCodes[0])  || string.IsNullOrEmpty(splitCodes[1])|| string.IsNullOrEmpty(splitCodes[2]))
			Debug.Log("code : " + code);
		else
			CharactorPartsChange(splitCodes[0].ToUpper(), splitCodes[1].ToUpper(), splitCodes[2], color.ToUpper());
	}

	public void CharacterSetting(AvatarState avatarState, bool local = false)
	{
		state = avatarState;
		if (nickName != null)
		{
			nickName.text = state.nickName;
			Debug.Log("nickname set as : " + state.nickName);
		}

		StartCoroutine(WaitUntilContainerAndThenSetting(local));
	}

	private IEnumerator WaitUntilContainerAndThenSetting(bool local = false)
	{
		GameObject container = GameObject.Find("MeshMatContainer");
		while (container == null)
		{
			yield return new WaitForSeconds(0.5f);
			container = GameObject.Find("MeshMatContainer");
		}

		// while (!(PlayerData.myPlayerinfo.state != null && skinnedMeshRendererList.Length > 0))
		// {
		// 	yield return new WaitForSeconds(0.5f);
		// }

		CharacterSetting(local);
		yield return null;
	}

	private void CharacterSetting(bool local = false)
	{
		if (state != null)
		{
			CharactorPartsChange(state.faceCode, state.faceColorCode);
			CharactorPartsChange(state.hairCode, state.hairColorCode);
			CharactorPartsChange(state.skinCode, state.skinColorCode);
			CharactorPartsChange(state.topCode, state.topColorCode);
			CharactorPartsChange(state.bottomCode, state.bottomColorCode);
			CharactorPartsChange(state.shoesCode, state.shoesColorCode);
			if (transform.Find("NickName") != null)
				SetPlayerNickName(state.nickName);
			if(local)
				ServerManager.Instance.AdjustNewestCustom();
		}
		else
		{
			Debug.LogError("Charactor state Null");
		}
	}
	
	private void SetPlayerNickName(string nickname)
	{
		//if (transform.tag != "Player") return;
		transform.Find("NickName").GetComponent<TextMeshPro>().text = nickname;
	}

	private void CharactorPartsChange(string partsName, string meshName, string matName,
		string matColor = "#000000")
	{
		//Debug.Log("CharactorPartsChange " + partsName + "_" + meshName + "_" + matName + " " + matColor);
		switch (partsName)
		{
			case "SKIN":
				UpdateMaterial("BOTTOM", partsName + "_" + meshName + "_" + matColor, 1);
				UpdateMaterial("TOP", partsName + "_" + meshName + "_" + matColor, 1);
				UpdateMaterial("FACE", partsName + "_FACE_" + meshName + "_" + matColor, 1);
				state.skinCode = partsName + "_" + meshName + "_" + matName;
				state.skinColorCode = matColor;
				break;
			case "HAIR":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				//if (meshName == "AA") // AA의 경우 모자와 머리카락 메쉬 위치가 바뀌어 있음
				//{
				//	UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matColor, 1);
				//	UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName);
				//}
				//else
				//{
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matColor);
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName, 1);
				//}
				state.hairCode = partsName + "_" + meshName + "_" + matName;
				state.hairColorCode = matColor;
				break;

			case "FACE":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + "001"); // 기본 얼굴
				UpdateMaterial(partsName,
					"SKIN_" + partsName + "_" + meshName + "_#" + ColorUtility.ToHtmlStringRGB(Array
						.Find<SkinnedMeshRenderer>(originalSkinnedMeshRenderers, c => c.name == partsName)
						.materials[1].color), 1); // 피부색 수정
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName, 2); // 문양 추가
				if (matName == "001") // 기본얼굴의 경우 문양 삭제
				{
					UpdateMaterial(partsName, "delete", 2);
				}

				CharactorPartsChange("EYE", meshName, "001");
				state.faceCode = partsName + "_" + meshName + "_" + matName;
				state.faceColorCode = matColor;
				break;

			case "EYE":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				UpdateMaterial(partsName, "EYE" + "_" + meshName + "_" + matName);
				break;
			case "BOTTOM":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName);
				state.bottomCode = partsName + "_" + meshName + "_" + matName;
				state.bottomColorCode = matColor;
				break;
			case "TOP":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName);
				state.topCode = partsName + "_" + meshName + "_" + matName;
				state.topColorCode = matColor;
				break;
			case "SHOES":
				UpdateMeshRenderer(partsName, partsName + "_" + meshName);
				UpdateMaterial(partsName, partsName + "_" + meshName + "_" + matName);
				state.shoesCode = partsName + "_" + meshName + "_" + matName;
				state.shoesColorCode = matColor;
				break;
			default:
				Debug.Log("Unidentified parts name - " + partsName);
				break;
		}
	}

	public void UpdateMeshRenderer(string targetName, string newMeshName)
	{
		//Debug.Log(targetName + " " + newMeshName);
		SkinnedMeshRenderer targetMeshRenderer =
			Array.Find<SkinnedMeshRenderer>(originalSkinnedMeshRenderers, c => c.name == targetName);
		SkinnedMeshRenderer newMeshRenderer =
			Array.Find<SkinnedMeshRenderer>(skinnedMeshRendererList, c => c.name == newMeshName);
		// update mesh
		targetMeshRenderer.sharedMesh = newMeshRenderer.sharedMesh;

		Transform[] childrens = transform.GetComponentsInChildren<Transform>(true);

		// sort bones.
		Transform[] bones = new Transform[newMeshRenderer.bones.Length];
		for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++)
		{
			bones[boneOrder] =
				Array.Find<Transform>(childrens, c => c.name == newMeshRenderer.bones[boneOrder].name);
		}

		targetMeshRenderer.bones = bones;
	}

	//private void ResizeMaterials(SkinnedMeshRenderer targetMeshRenderer, Material newMaterial, int materialNum)
	//   {
	//	if (newMaterial == null) // 입력된 마테리얼 값이 없는 경우
	//	{
	//		Material[] temp = targetMeshRenderer.materials;
	//		//if (temp.Length <= 1) // 메테리얼이 1개만 있는 경우 예외처ㄹ
	//		//	return false;
	//		Array.Resize(ref temp, temp.Length - 1);
	//		targetMeshRenderer.materials = temp;
	//		return;
	//	}

	//	if (targetMeshRenderer.materials.Length <= materialNum) // 현재 마테리얼의 개수가 입력예정인 순서값보다 낮아서 할당이 불가능할때
	//	{
	//		Material[] temp = targetMeshRenderer.materials;
	//		Array.Resize(ref temp, temp.Length + 1);
	//		temp.SetValue(null, temp.Length - 1);
	//		targetMeshRenderer.materials = temp;
	//	}

	//	//return false;
	//}

	private void UpdateMaterial(string targetName, string newMeshName, int materialNum = 0)
	{
		//Debug.Log(targetName + " " + newMeshName + " " + materialNum);
		SkinnedMeshRenderer targetMeshRenderer =
			Array.Find<SkinnedMeshRenderer>(originalSkinnedMeshRenderers, c => c.name == targetName);
		Material newMaterial = Array.Find<Material>(materialList, c => c.name == newMeshName);

		if (targetMeshRenderer.materials.Length <= materialNum) // 현재 메쉬에 마테리얼의 개수가 입력예정인 순서값보다 낮아서 할당이 불가능할때
		{
			Material[] temp = targetMeshRenderer.materials;
			Array.Resize(ref temp, temp.Length + 1);
			temp.SetValue(null, temp.Length - 1);
			targetMeshRenderer.materials = temp;
		}

		if (newMaterial == null) // 입력된 마테리얼 값이 없는 경우
		{
			Material[] temp = targetMeshRenderer.materials;
			//if (temp.Length <= 1) // 메테리얼이 1개만 있는 경우 예외처ㄹ
			//	return false;
			Array.Resize(ref temp, temp.Length - 1);
			targetMeshRenderer.materials = temp;
			return;
		}

		Material[] tempMat = targetMeshRenderer.materials;
		tempMat[materialNum] = newMaterial;
		targetMeshRenderer.materials = tempMat;
		//ResizeMaterials(targetMeshRenderer, newMaterial, materialNum);
	}

	[PunRPC]
	private void SetNewCustom(string newstatedata, int nid)
	{
		if (GetComponent<PhotonView>().InstantiationId == nid)
		{
			// Debug.Log("InstantiationId" + GetComponent<PhotonView>().InstantiationId + " // "+ nid + " // " + ProcessManager.Instance.player.GetComponent<PhotonView>().InstantiationId);
			// 	AvatarState newstate = JsonUtility.FromJson<AvatarState>(newstatedata);
			// 	ProcessManager.Instance.state.Copy(newstate);
			// 	//ProcessManager.Instance.player.
			// 	GetComponent<CharctorMeshAndMaterialController>()
			// 		.CharacterSetting(ProcessManager.Instance.state);
			Debug.Log("rpc recv nid : " + nid);
			AvatarState newstate = JsonUtility.FromJson<AvatarState>(newstatedata);
			GetComponent<CharctorMeshAndMaterialController>()
				.CharacterSetting(newstate);
		}
	}
}
