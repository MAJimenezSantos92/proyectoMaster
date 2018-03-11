using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class  rfidPuerta : MonoBehaviour {
	private MqttClient client;
	public bool puertaMover;
	public string topic;
	public Transform soporte_02;
		
	// Use this for initialization
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "tarjeta1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Publish("tarjeta1", System.Text.Encoding.UTF8.GetBytes("1426849450"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		
	}
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		
		if(topic.Equals("1437043627")){
			UnityMainThreadDispatcher.Instance().Enqueue(() => girarCofre (true) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
			//client.Publish("tarjeta1", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

		}
		
		if(topic.Equals("1426849450")){
			UnityMainThreadDispatcher.Instance().Enqueue(() => girarCofre (false) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
			//client.Publish("tarjeta1", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		} 
		
	} 
	
	void girarCofre (bool entrada) {
		if(entrada==true){
			puertaMover=true;
			
		}else{
			if(entrada==false){
				puertaMover=false;
			}
		}	
		
	}
	
	// Update is called once per frame
	void Update () {
  if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
         //Application.Quit(); // Quits the game
		   SceneManager.LoadScene ("MENU");
     }
        if(puertaMover){
            var newRot = Quaternion.RotateTowards(soporte_02.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
            soporte_02.rotation = newRot;
        }else{
			if(!puertaMover){
				var newRot = Quaternion.RotateTowards(soporte_02.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 250);
				soporte_02.rotation = newRot;
        	}
		}
	}
}


