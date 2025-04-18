using UnityEngine;
using System.Collections;
     [ExecuteInEditMode]
public class SuspensionTraction : MonoBehaviour {

	public Transform susScal_1A;
	public Transform susScal_2A;
	public Transform susScale_1B;
	public Transform susScale_2B;
	public Transform susScale_1C;
	public Transform susScale_2C;
	public Transform susScale_1D;
	public Transform susScale_2D;
	public Transform susScale_1F;
	public Transform susScale_2F;
	public Transform susScale_1G;
	public Transform susScale_2G;
	public Transform susScale_1E;
	public Transform susScale_2E;
	public Transform susScale_1Q;
	public Transform susScale_2Q;
	public Transform susScale_1W;
	public Transform susScale_2W;
	public Transform susScale_1R;
	public Transform susScale_2R;
	public Transform susScale_1T;
	public Transform susScale_2T;
	public Transform susScale_1Y;
	public Transform susScale_2Y;
	public Transform susScale_1V;
	public Transform susScale_2V;
	public Transform susScale_1N;
	public Transform susScale_2N;
	public Transform susScale_1M;
	public Transform susScale_2M;
	public Transform susScale_1H;
	public Transform susScale_2H;
	public Transform sus_1A_SP;
	public Transform sus_2A_SP;
	public Transform sus_1B_SP;
	public Transform sus_2B_SP;
	public Transform sus_1C_SP;
	public Transform sus_2C_SP;
	public Transform sus_1D_SP;
	public Transform sus_2D_SP;
	public Transform sus_1E_SP;
	public Transform sus_2E_SP;
	public Transform sus_1F_SP;
	public Transform sus_2F_SP;
	public Transform sus_1G_SP;
	public Transform sus_2G_SP;
	public Transform sus_1Q_SP;
	public Transform sus_2Q_SP;

	private float originalLengthleft1;
	private float originalLengthleft2;
	private float originalLengthleft3;
	private float originalLengthleft4;
	private float originalLengthleft5;
	private float originalLengthleft6;
	private float originalLengthleft7;
	private float originalLengthleft8;
	private float originalLengthleft9;
	private float originalLengthleft10;
	private float originalLengthleft11;
	private float originalLengthleft12;
	private float originalLengthleft13;
	private float originalLengthleft14;
	private float originalLengthleft15;
	private float originalLengthleft16;




	void Start()  {
		originalLengthleft1 = Vector3.Distance(susScal_1A.position, susScal_2A.position);
		originalLengthleft2 = Vector3.Distance(susScale_1B.position, susScale_2B.position);
		originalLengthleft3 = Vector3.Distance(susScale_1C.position, susScale_2C.position);
		originalLengthleft4 = Vector3.Distance(susScale_1D.position, susScale_2D.position);
		originalLengthleft5 = Vector3.Distance(susScale_1F.position, susScale_2F.position);
		originalLengthleft6 = Vector3.Distance(susScale_1G.position, susScale_2G.position);
		originalLengthleft7 = Vector3.Distance(susScale_1E.position, susScale_2E.position);
		originalLengthleft8 = Vector3.Distance(susScale_1Q.position, susScale_2Q.position);
		originalLengthleft9 = Vector3.Distance(susScale_1W.position, susScale_2W.position);
		originalLengthleft10 = Vector3.Distance(susScale_1R.position, susScale_2R.position);
		originalLengthleft11 = Vector3.Distance(susScale_1T.position, susScale_2T.position);
		originalLengthleft12 = Vector3.Distance(susScale_1Y.position, susScale_2Y.position);
		originalLengthleft13 = Vector3.Distance(sus_1A_SP.position, sus_2A_SP.position);
		originalLengthleft14 = Vector3.Distance(sus_1B_SP.position, sus_2B_SP.position);
		originalLengthleft15 = Vector3.Distance(sus_1C_SP.position, sus_2C_SP.position);
		originalLengthleft16 = Vector3.Distance(sus_1D_SP.position, sus_2D_SP.position);
	
	
	
	}
	void Update (){

		float newScale1 = Vector3.Distance (susScal_1A.position, susScal_2A.position) / originalLengthleft1;
		susScal_1A.localScale = new Vector3 (1f, 1f, newScale1);
		float newScale2 = Vector3.Distance (susScale_1B.position, susScale_2B.position)/ originalLengthleft2;
		susScale_1B.localScale = new Vector3 (1f, 1f, newScale2);
		float newScale3 = Vector3.Distance (susScale_1C.position, susScale_2C.position)/ originalLengthleft3;
		susScale_1C.localScale = new Vector3 (1f, 1f, newScale3);
		float newScale4 = Vector3.Distance (susScale_1D.position, susScale_2D.position)/ originalLengthleft4;
		susScale_1D.localScale = new Vector3 (1f, 1f, newScale4);
		float newScale5 = Vector3.Distance (susScale_1F.position, susScale_2F.position)/ originalLengthleft5;
		susScale_1F.localScale = new Vector3 (1f, 1f, newScale5);
		float newScale6 = Vector3.Distance (susScale_1G.position, susScale_2G.position)/ originalLengthleft6;
		susScale_1G.localScale = new Vector3 (1f, 1f, newScale6);
		float newScale7 = Vector3.Distance (susScale_1E.position, susScale_2E.position)/ originalLengthleft7;
		susScale_1E.localScale = new Vector3 (1f, 1f, newScale7);
		float newScale8 = Vector3.Distance (susScale_1Q.position, susScale_2Q.position)/ originalLengthleft8;
		susScale_1Q.localScale = new Vector3 (1f, 1f, newScale8);
		float newScale9 = Vector3.Distance (susScale_1W.position, susScale_2W.position)/ originalLengthleft9;
		susScale_1W.localScale = new Vector3 (1f, 1f, newScale9);
		float newScale10 = Vector3.Distance (susScale_1R.position, susScale_2R.position)/ originalLengthleft10;
		susScale_1R.localScale = new Vector3 (1f, 1f, newScale10);
		float newScale11 = Vector3.Distance (susScale_1T.position, susScale_2T.position)/ originalLengthleft11;
		susScale_1T.localScale = new Vector3 (1f, 1f, newScale11);
		float newScale12 = Vector3.Distance (susScale_1Y.position, susScale_2Y.position)/ originalLengthleft12;
		susScale_1Y.localScale = new Vector3 (1f, 1f, newScale12);
		float newScale13 = Vector3.Distance (sus_1A_SP.position, sus_2A_SP.position)/ originalLengthleft13;
		sus_1A_SP.localScale = new Vector3 (1f, 1f, newScale13);
		float newScale14 = Vector3.Distance (sus_1B_SP.position, sus_2B_SP.position)/ originalLengthleft14;
		sus_1B_SP.localScale = new Vector3 (1f, 1f, newScale14);
		float newScale15 = Vector3.Distance (sus_1C_SP.position, sus_2C_SP.position)/ originalLengthleft15;
		sus_1C_SP.localScale = new Vector3 (1f, 1f, newScale15);
		float newScale16 = Vector3.Distance (sus_1D_SP.position, sus_2D_SP.position)/ originalLengthleft16;
		sus_1D_SP.localScale = new Vector3 (1f, 1f, newScale16);
	
	
	
	
	}
      

void LateUpdate (){

	if (susScal_1A != null && susScal_2A != null) 
	{
		susScal_1A.LookAt (susScal_2A.position, susScal_1A.up);
		susScal_2A.LookAt (susScal_1A.position, susScal_2A.up);
	}
	if (susScale_1B != null && susScale_2B != null) 
	{
		susScale_1B.LookAt (susScale_2B.position, susScale_1B.up);
		susScale_2B.LookAt (susScale_1B.position, susScale_2B.up);
	}
	if (susScale_1C != null && susScale_2C != null) 
	{
		susScale_1C.LookAt (susScale_2C.position, susScale_1C.up);
		susScale_2C.LookAt (susScale_1C.position, susScale_2C.up);
	}
	if (susScale_1D != null && susScale_2D != null) 
	{
		susScale_1D.LookAt (susScale_2D.position, susScale_1D.up);
		susScale_2D.LookAt (susScale_1D.position, susScale_2D.up);
	}
	if (susScale_1F != null && susScale_2F != null) 
	{
		susScale_1F.LookAt (susScale_2F.position, susScale_1F.up);
		susScale_2F.LookAt (susScale_1F.position, susScale_2F.up);
	}
	if (susScale_1G != null && susScale_2G != null) 
	{
		susScale_1G.LookAt (susScale_2G.position, susScale_1G.up);
		susScale_2G.LookAt (susScale_1G.position, susScale_2G.up);
	}
	if (susScale_1E != null && susScale_2E != null) 
	{
		susScale_1E.LookAt (susScale_2E.position, susScale_1E.up);
		susScale_2E.LookAt (susScale_1E.position, susScale_2E.up);
	}
	if (susScale_1Q != null && susScale_2Q != null) 
	{
		susScale_1Q.LookAt (susScale_2Q.position, susScale_1Q.up);
		susScale_2Q.LookAt (susScale_1Q.position, susScale_2Q.up);
	}
	if (susScale_1R != null && susScale_2R != null) 
	{
		susScale_1R.LookAt (susScale_2R.position, susScale_1R.up);
		susScale_2R.LookAt (susScale_1R.position, susScale_2R.up);
	}
	if (susScale_1T != null && susScale_2T != null) 
	{
		susScale_1T.LookAt (susScale_2T.position, susScale_1T.up);
		susScale_2T.LookAt (susScale_1T.position, susScale_2T.up);
	}
	if (susScale_1W != null && susScale_2W != null) 
	{
		susScale_1W.LookAt (susScale_2W.position, susScale_1W.up);
		susScale_2W.LookAt (susScale_1W.position, susScale_2W.up);
	}
	if (susScale_1Y != null && susScale_2Y != null) 
	{
		susScale_1Y.LookAt (susScale_2Y.position, susScale_1Y.up);
		susScale_2Y.LookAt (susScale_1Y.position, susScale_2Y.up);
	}
	if (susScale_1V != null && susScale_2V != null) 
	{
		susScale_1V.LookAt (susScale_2V.position, susScale_1V.up);
		susScale_2V.LookAt (susScale_1V.position, susScale_2V.up);
	}
	if (susScale_1N != null && susScale_2N != null) 
	{
		susScale_1N.LookAt (susScale_2N.position, susScale_1N.up);
		susScale_2N.LookAt (susScale_1N.position, susScale_2N.up);
	}
	if (susScale_1M != null && susScale_2M != null)
	{
		susScale_1M.LookAt (susScale_2M.position, susScale_1M.up);
		susScale_2M.LookAt (susScale_1M.position, susScale_2M.up);
	}
	if (susScale_1H != null && susScale_2H != null) 
	{
		susScale_1H.LookAt (susScale_2H.position, susScale_1H.up);
		susScale_2H.LookAt (susScale_1H.position, susScale_2H.up);
	} 
		if (sus_1A_SP != null && sus_2A_SP != null) 
		{
			sus_1A_SP.LookAt (sus_2A_SP.position, sus_1A_SP.up);
			sus_2A_SP.LookAt (sus_1A_SP.position, sus_2A_SP.up);
		}
		if (sus_1B_SP != null && sus_2B_SP != null) 
		{
			sus_1B_SP.LookAt (sus_2B_SP.position, sus_1B_SP.up);
			sus_2B_SP.LookAt (sus_1B_SP.position, sus_2B_SP.up);
		}
		if (sus_1C_SP != null && sus_2C_SP != null) 
		{
			sus_1C_SP.LookAt (sus_2C_SP.position, sus_1C_SP.up);
			sus_2C_SP.LookAt (sus_1C_SP.position, sus_2C_SP.up);
		}
		if (sus_1D_SP != null && sus_2D_SP != null) 
		{
			sus_1D_SP.LookAt (sus_2D_SP.position, sus_1D_SP.up);
			sus_2D_SP.LookAt (sus_1D_SP.position, sus_2D_SP.up);
		}
		if (sus_1E_SP != null && sus_2E_SP != null) 
		{
			sus_1E_SP.LookAt (sus_2E_SP.position, sus_1E_SP.up);
			sus_2E_SP.LookAt (sus_1E_SP.position, sus_2E_SP.up);
		}
		if (sus_1F_SP != null && sus_2F_SP != null) 
		{
			sus_1F_SP.LookAt (sus_2F_SP.position, sus_1F_SP.up);
			sus_2F_SP.LookAt (sus_1F_SP.position, sus_2F_SP.up);
		}
		if (sus_1G_SP != null && sus_2G_SP != null) 
		{
			sus_1G_SP.LookAt (sus_2G_SP.position, sus_1G_SP.up);
			sus_2G_SP.LookAt (sus_1G_SP.position, sus_2G_SP.up);
		}
		if (sus_1Q_SP != null && sus_2Q_SP != null) 
		{
			sus_1Q_SP.LookAt (sus_2Q_SP.position, sus_1Q_SP.up);
			sus_2Q_SP.LookAt (sus_1Q_SP.position, sus_2Q_SP.up);
		}
	
	}
}