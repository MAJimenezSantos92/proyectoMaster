using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class mqttTest : MonoBehaviour {
	private MqttClient client;
	// Use this for initialization
	void Start () {
		// create client instance 
		//client = new MqttClient(IPAddress.Parse("143.185.118.233"),8080 , false , null ); 
		//client = new MqttClient("a25r8ylqy203h8.iot.us-west-2.amazonaws.com",8883 , false , null ); 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		//client.Subscribe(new string[] { "hello/world" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "ledsPrueba1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "tempPrueba1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 

		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
	} 

	void OnGUI(){
		
		// Make a background box
        GUI.Box(new Rect(10,10,300,210), "MENU LEDS");
		
		if ( GUI.Button (new Rect (20,40,120,20), "Enable Red LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("RED ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
		
		if ( GUI.Button (new Rect (180,40,120,20), "Disable Red LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("RED OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		
		}
		
		if ( GUI.Button (new Rect (20,70,120,20), "Enable Yellow LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("YELLOW ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
		
		if ( GUI.Button (new Rect (180,70,120,20), "Disable Yellow LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("YELLOW OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
		
		if ( GUI.Button (new Rect (20,100,120,20), "Enable Green LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("GREEN ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
			
		if ( GUI.Button (new Rect (180,100,120,20), "Disable Green LED")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("GREEN OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
		
		
		
		if ( GUI.Button (new Rect (20,130,120,20), "Open The Door")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("PUERTA ON"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
			
		if ( GUI.Button (new Rect (180,130,120,20), "Close The Door")) {
			Debug.Log("sending...");
			client.Publish("ledsPrueba1", System.Text.Encoding.UTF8.GetBytes("PUERTA OFF"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
		
		
	}
	// Update is called once per frame
	void Update () {



	}
}
