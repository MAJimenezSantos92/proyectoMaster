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


public class menu : MonoBehaviour {
	private MqttClient client;
	
	
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("menuAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

	}
	
	void Update () {
		
	 if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
         Application.Quit(); // Quits the game
		  
     }
	}
	
	// Use this for initialization
    public void onClick1()
    {
        SceneManager.LoadScene ("pruebaFebrero");
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("juegoAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    }
	
		// Use this for initialization
    public void onClick2()
    {
        SceneManager.LoadScene ("museo");
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("museoAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    }
	
		// Use this for initialization
    public void onClick3()
    {
        SceneManager.LoadScene ("vaca");
		client.Publish("museo/androidCall", System.Text.Encoding.UTF8.GetBytes("vacaAndroid"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    }
	
}

