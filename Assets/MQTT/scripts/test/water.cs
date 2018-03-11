using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;


public class water : MonoBehaviour {
	private MqttClient client;
	public float x = -1.87f;
	public float y = -0.221f;
	public float z = 22.72f;
	public string topic;
	public float valorY;
	// Use this for initialization
	void Start () {
		
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "nivelAgua1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
	}
	

	private float GetFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
	
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		
		valorY=GetFloat(topic, y);
		
		
	} 
	
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(x,valorY,z);
	}
}


