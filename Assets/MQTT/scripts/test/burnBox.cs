using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnBox : MonoBehaviour {

	public float burntime;
	public float time;
	public GameObject go;
	public Color endcolor = Color.black;
	private Color startColor;

	private Color delta;
	public float speed = 0.5f;
	public bool isBurn;
	
	

	
	public GameObject humoNegro;
	public GameObject fuegoNegro;
	float curre = 1.0f;
	// Use this for initialization
	void Start () {
	 startColor = go.GetComponent<Renderer>().material.color;
	 burntime=25.0f;
	 time=0.0f;
	 delta = startColor/burntime;
	 isBurn=false;
	 //Debug.Log(delta.ToString());
	 
	}

	
	void OnCollisionEnter (Collision col) {
		if(isBurn==false){
		if(col.gameObject.name == "lava1_rock") {
			Debug.Log("estoy dentro");
			isBurn=true;
			startColor = go.GetComponent<Renderer>().material.color;
			burntime=25.0f;
			time=0.0f;
			delta = startColor/burntime;
			//Destroy(col.gameObject); 
			//client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		} /* else{
			if(topic.Equals("RFID1 ON")){
				client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			}
		} */
		}
	} 
	
	
// Update is called once per frame
	void Update () {
		if(isBurn==true){
			humoNegro.SetActive(true); 
			fuegoNegro.SetActive(true); 
			if(curre < 0.15f){
					isBurn=false;
					Destroy(go,1.0f);
					}
			
			if(burntime<23.79){
				
				Debug.Log("acaba el quemando");
				
				Vector3 originalScale = new Vector3(1.0f, 1.0f, 1.0f);
				Vector3 destinationScale = new Vector3(1.0f, curre, 1.0f);
		
				
					go.gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, 1.0f);
					curre=curre-0.025f;
					Debug.Log(curre);
				
				
				//Destroy(go);
				
			}
			
			if ( time < 1.25f) {
				Debug.Log("tiempo quemando");
				 if( burntime > 0.0f){
					 Debug.Log("burntime cambiando material");
					 burntime -= Time.deltaTime;
					 time += Time.deltaTime;
					 go.GetComponent<Renderer>().material.color -= delta;
					 
					 Debug.Log(go.GetComponent<Renderer>().material.color.ToString());
					 Debug.Log(time);
					 Debug.Log(burntime);
				 }
			}
			
		}
			
	}
}