
	
using UnityEngine;
using System.Collections;

public class characterButtonGirl : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[9];
	public AnimationClip[] AniList  = new AnimationClip[4];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject Villarger_A_Girl_prefab;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 70, 40),"Idle")){
		 frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Idle");
	  }
	    if (GUI.Button(new Rect(90, 20, 70, 40),"Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Walk");
	  }
		  if (GUI.Button(new Rect(160, 20, 70, 40),"L_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_L_Walk");
	  }
		  if (GUI.Button(new Rect(230, 20, 70, 40),"R_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_R_Walk");
	  }
		  if (GUI.Button(new Rect(300, 20, 70, 40),"B_Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_B_Walk");
	  }
	     if (GUI.Button(new Rect(370, 20, 70, 40),"Talk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Talk");
	  } 
		 if (GUI.Button(new Rect(440, 20, 70, 40),"Talk01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Talk01");
	  } 
		if (GUI.Button(new Rect(510, 20, 70, 40),"Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Run");
	  }  
		if (GUI.Button(new Rect(580, 20, 70, 40),"L_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_L_Run");
	  }  
		if (GUI.Button(new Rect(650, 20, 70, 40),"R_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_R_Run");
	  }  
		if (GUI.Button(new Rect(720, 20, 70, 40),"B_Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_B_Run");
	 }  
			if (GUI.Button(new Rect(790, 20, 70, 40),"Jump")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Jump");
	  } 
		if (GUI.Button(new Rect(860, 20, 70, 40),"Draw Blade")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("BG_DrawBlade");
	  } 
		if (GUI.Button(new Rect(20, 65, 70, 40),"Put Blade")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("BG_PutBlade");
	  } 
		if (GUI.Button(new Rect(90, 65, 70, 40),"AtkStandy")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_AttackStandy");
	  } 
		if (GUI.Button(new Rect(160, 65, 70, 40),"Attack00")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Attack00");
	  } 
		if (GUI.Button(new Rect(230, 65, 70, 40),"Attack01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Attack01");
			
		}	
		if (GUI.Button(new Rect(300, 65, 70, 40),"Block")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Block");
	  } 
		
			if (GUI.Button(new Rect(370, 65, 70, 40),"BlockAttack")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_BlockAttack");
	  } 
			if (GUI.Button(new Rect(440, 65, 70, 40),"Combo1")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Combo1");
	  }
				if (GUI.Button(new Rect(510, 65, 70, 40),"Combo1_1")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("BG_Combo1_1");
	  }
				if (GUI.Button(new Rect(580, 65, 70, 40),"Combo1_2")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.Play("BG_Combo1_2");
	  }
				if (GUI.Button(new Rect(650, 65, 70, 40),"Combo1_3")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("BG_Combo1_3");
	  }
		if (GUI.Button(new Rect(720, 65, 70, 40),"kick")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Kick");
	  }
			if (GUI.Button(new Rect(790, 65, 70, 40),"Skill")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Skill");
	  }
			if (GUI.Button(new Rect(860, 65, 70, 40),"M_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_M_Avoid");
	  }
			if (GUI.Button(new Rect(20, 110, 70, 40),"L_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_L_Avoid");
	  }
			if (GUI.Button(new Rect(90, 110, 70, 40),"R_Avoid")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_R_Avoid");
	  }
			if (GUI.Button(new Rect(160, 110, 70, 40),"Buff")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Buff");
	  }
		if (GUI.Button(new Rect(230, 110, 70, 40),"Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Run01");
	  }
		if (GUI.Button(new Rect(300, 110, 70, 40),"RunAttack")){
		  frog.animation.wrapMode= WrapMode.Once;
		  	frog.animation.CrossFade("BG_RunAttack");
	  }
		if (GUI.Button(new Rect(370, 110, 70, 40),"L_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_L_Run01");
	  }
		if (GUI.Button(new Rect(440, 110, 70, 40),"R_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_R_Run01");
	  }
		if (GUI.Button(new Rect(510, 110, 70, 40),"B_Run01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_B_Run01");
	  }
		
		if (GUI.Button(new Rect(580, 110, 70, 40),"Jump01")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Jump01");
	  }
		if (GUI.Button(new Rect(650, 110, 70, 40),"PickUp")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Pickup");
	  }
		
			if (GUI.Button(new Rect(720, 110, 70, 40),"Damage")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Damage");
	  }
			if (GUI.Button(new Rect(790, 110, 70, 40),"Death")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_Death");
	  }
		if (GUI.Button(new Rect(860, 110, 70, 40),"GangnamStyle")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("BG_GangnamStyle");
	  }
	
		
		/////////////////////////////////////////////////////////////////////
		
		if (GUI.Button(new Rect(20, 700, 120, 40),"Blad_Warrior")){
	       Application.LoadLevel(0);
	 }
		if (GUI.Button(new Rect(150, 700, 70, 40),"Base")){
	       Application.LoadLevel(0);
	 }  
		if (GUI.Button(new Rect(220, 700, 70, 40),"T01")){
	       Application.LoadLevel(1);
	 } 
		if (GUI.Button(new Rect(290, 700, 70, 40),"T02")){
	       Application.LoadLevel(2);
	 }  
		if (GUI.Button(new Rect(360, 700, 70, 40),"T03")){
	       Application.LoadLevel(3);
	 }  
		if (GUI.Button(new Rect(430, 700, 70, 40),"T04")){
	       Application.LoadLevel(4);
	 }  
		if (GUI.Button(new Rect(500, 700, 70, 40),"T05")){
	       Application.LoadLevel(5);
	 }  
		if (GUI.Button(new Rect(570, 700, 70, 40),"T06")){
	       Application.LoadLevel(6);
	 }  
		if (GUI.Button(new Rect(640, 700, 70, 40),"T07")){
	       Application.LoadLevel(7);
	 }  
		
	 ///////////////////////////////////////////////////////////////////////////////
		
		if (GUI.Button(new Rect(20, 740, 120, 40),"Blad_Girl")){
	       Application.LoadLevel(8);
	 }
			if (GUI.Button(new Rect(150, 740, 70, 40),"Base")){
	       Application.LoadLevel(8);
	 }  
		if (GUI.Button(new Rect(220, 740, 70, 40),"T01")){
	       Application.LoadLevel(9);
	 } 
		if (GUI.Button(new Rect(290, 740, 70, 40),"T02")){
	       Application.LoadLevel(10);
	 }  
		if (GUI.Button(new Rect(360, 740, 70, 40),"T03")){
	       Application.LoadLevel(11);
	 }  
		if (GUI.Button(new Rect(430, 740, 70, 40),"T04")){
	       Application.LoadLevel(12);
	 }  
		if (GUI.Button(new Rect(500, 740, 70, 40),"T05")){
	       Application.LoadLevel(13);
	 }  
		if (GUI.Button(new Rect(570, 740, 70, 40),"T06")){
	       Application.LoadLevel(14);
	 }  
		if (GUI.Button(new Rect(640, 740, 70, 40),"T07")){
	       Application.LoadLevel(15);
	 }  
	
	//////////////////////////////////////////////////////////////////////////////
			if (GUI.Button(new Rect(20, 520, 120, 40),"V  1.4")){
	            frog.animation.wrapMode= WrapMode.Loop;
		     	frog.animation.CrossFade("BG_Chibi_Idle");
	 } 	
		
		if (GUI.Button(new Rect(20, 560, 120, 40),"Equipment")){
	       Application.LoadLevel(0);
	 } 
	
		
		if (GUI.Button(new Rect(20, 600, 120, 40),"Costume")){
	       Application.LoadLevel(16);
	 } 
			if (GUI.Button(new Rect(20, 640, 120, 40),"Chibi")){
	       Application.LoadLevel(20);
	 } 
		
			//	if (GUI.Button(new Rect(640, 540, 140, 40),"Ver 2.0")){
		// frog.animation.wrapMode= WrapMode.Loop;
		 // 	frog.animation.CrossFade("BW_Idle");
	  //}
		
		
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
