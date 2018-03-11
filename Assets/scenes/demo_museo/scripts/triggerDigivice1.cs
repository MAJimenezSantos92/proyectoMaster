using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;

public class triggerDigivice1 : MonoBehaviour {
	private MqttClient client;
	public string topic;
	
	public Material[] material;
	Renderer rend2;
	public GameObject obj2;
	public string topicNuevo;
	
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "museo/digivice1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		client.Publish("museo/digivice1", System.Text.Encoding.UTF8.GetBytes("digivice1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

		rend2 = obj2.GetComponent<Renderer>();
		rend2.enabled= true;
		rend2.sharedMaterial = material[0];
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		
	}
	
	void Update () {
		
		if(topicNuevo.Equals("digivice1")){
			rend2.sharedMaterial = material[0];
			Debug.Log("CAMBIO A LIBRO 2");
		}else if(topicNuevo.Equals("digivice2")){
			rend2.sharedMaterial = material[1];
			Debug.Log("CAMBIO A LIBRO 1");
		}else if(topicNuevo.Equals("digivice3")){
			rend2.sharedMaterial = material[2];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice4")){
			rend2.sharedMaterial = material[3];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice5")){
			rend2.sharedMaterial = material[4];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice6")){
			rend2.sharedMaterial = material[5];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice7")){
			rend2.sharedMaterial = material[6];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice8")){
			rend2.sharedMaterial = material[7];
			Debug.Log("CAMBIO A LIBRO 3");
		}else if(topicNuevo.Equals("digivice9")){
			rend2.sharedMaterial = material[8];
			Debug.Log("CAMBIO A LIBRO 3");
		}		
		
		topicNuevo="";
	}
	
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		topicNuevo=topic;
/* 		if(topic.Equals("RFID1 ON")){
			if(posMat!=0){
				posMat=posMat-1;
			} 
			UnityMainThreadDispatcher.Instance().Enqueue(() => cambiarMaterial (posMat) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
			
			//client.Publish("tarjeta1", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

		}
		
		if(topic.Equals("NEXT")){
			if(posMat<8){
				posMat=posMat+1;
			} 
			UnityMainThreadDispatcher.Instance().Enqueue(() => cambiarMaterial (posMat) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
			//client.Publish("tarjeta1", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		}  */
	} 



	void OnTriggerEnter(Collider c){
         if(c.gameObject.name == "FPSController"){ 
             Debug.Log ("Player triggered");
		 client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("digiviceAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
         }else{ 
             Debug.Log ("Something else triggered");
			 }
    }
	
	
	
}