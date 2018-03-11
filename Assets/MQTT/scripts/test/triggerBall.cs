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

public class triggerBall : MonoBehaviour {
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
		client.Subscribe(new string[] { "bolaRFID1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

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


	void OnCollisionEnter (Collision col) {
		if(col.gameObject.name == "plataforma") {
			//Destroy(col.gameObject); 
			client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		} /* else{
			if(topic.Equals("RFID1 ON")){
				client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			}
		} */
	}
	
 	void OnCollisionExit(Collision col) {
		if(col.gameObject.name == "plataforma") {
			
       client.Publish("bolaRFID1", System.Text.Encoding.UTF8.GetBytes("RFID1 OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	   }
    } 
	
}