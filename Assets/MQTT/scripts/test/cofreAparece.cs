using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class cofreAparece : MonoBehaviour {
	public GameObject cofre;
	private MqttClient client;
	
	public string topic;
	// Use this for initialization
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "cofre1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
	}
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		
		if(topic.Equals("COFRE 1 ON")){

			
			UnityMainThreadDispatcher.Instance().Enqueue(() => CofreMostrar1 (true) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		}
		
		if(topic.Equals("COFRE 1 OFF")){
			UnityMainThreadDispatcher.Instance().Enqueue(() => CofreMostrar1 (false) );
			
			Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		} 
	} 
	
	void CofreMostrar1 (bool entrada) {
		if(entrada==true){
			cofre.SetActive(true); // false to hide, true to show
		}else{
			if(entrada==false){
				cofre.SetActive(false); // false to hide, true to show
			}
		}	
		
	}
	
	/*
	void OnTriggerEnter () {
		Rigidbody RigidCofre;
		RigidCofre = Instantiate(cofre, triggerCofre.position, triggerCofre.rotation)as Rigidbody;
	}
	*/
	
	
	
	// Update is called once per frame
	void Update () {



	}
}


