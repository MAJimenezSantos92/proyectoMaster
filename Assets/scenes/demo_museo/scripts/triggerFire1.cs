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

public class triggerFire1 : MonoBehaviour {
	private MqttClient client;
	public string topic;
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "museo/fire1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		client.Publish("museo/fire1", System.Text.Encoding.UTF8.GetBytes("fire1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

	}
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		
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
		 client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("fireAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
         }else{ 
             Debug.Log ("Something else triggered");
			 }
    }
	
	
	
}