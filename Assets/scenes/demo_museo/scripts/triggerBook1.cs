using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;

public class triggerBook1 : MonoBehaviour {
	private MqttClient client;
	public string topic;
	public Material[] material;
	Renderer rend;
	public GameObject obj;
	public string topicNuevo;
	
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "museo/book1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		client.Publish("museo/book1", System.Text.Encoding.UTF8.GetBytes("book1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//obj.GetComponent<Renderer>().material = Book1;
		//obj.GetComponent<Renderer>().material = mat1;
		rend = obj.GetComponent<Renderer>();
		rend.enabled= true;
		rend.sharedMaterial = material[0];
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        
		
	}
	
	void Update () {
		
		
		 if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
         //Application.Quit(); // Quits the game
		   SceneManager.LoadScene ("MENU");
     }
	 
	 
		if(topicNuevo.Equals("book2")){
			rend.sharedMaterial = material[1];
			Debug.Log("CAMBIO A LIBRO 2");

		}else if(topicNuevo.Equals("book1")){
			rend.sharedMaterial = material[0];
			Debug.Log("CAMBIO A LIBRO 1");
		}else if(topicNuevo.Equals("book3")){
			rend.sharedMaterial = material[2];
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
		Debug.Log("Entro a Subscripcion");
		topicNuevo=topic;
		} 

		
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
	


	void OnTriggerEnter(Collider c){
         if(c.gameObject.name == "FPSController"){ 
             Debug.Log ("Player triggered");
		 client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("bookAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
         }else{ 
             Debug.Log ("Something else triggered");
			 }
    }
	
	
	
}