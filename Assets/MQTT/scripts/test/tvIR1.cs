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
[RequireComponent (typeof(AudioSource))]

public class tvIR1 : MonoBehaviour {
	private MqttClient client;
	public string topic;
	public int posMat=0;	
	public MovieTexture movie0;
	public MovieTexture movie1;
	public MovieTexture movie2;
	public MovieTexture movie3;
	public MovieTexture movie4;
	
	public MovieTexture movie5;
	public MovieTexture movie6;
	public MovieTexture movie7;
	
	private AudioSource audio1;
	//private AudioSource audio1;
	//private AudioSource audio2;
	//private AudioSource audio3;
	//private AudioSource audio4;

	// Use this for initialization
	void Start () {
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		//client = new MqttClient("unfathomablebarrier.servegame.com",1883 , false , null ); 
		
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "tvIR1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		client.Publish("tvIR1", System.Text.Encoding.UTF8.GetBytes("PREV"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		GetComponent<RawImage>().texture = movie0 as MovieTexture;
		movie0.loop = true;
		movie0.Play();
	}
	
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Console.WriteLine("message="+e.Message.ToString());
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		topic=System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log(topic);
		
		if(topic.Equals("PREV")){
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
		} 
	} 
	
	void cambiarMaterial (int posMat2) {
		if(posMat2==0){
			GetComponent<RawImage>().texture = movie0 as MovieTexture;
			
			movie0.loop = true;
			movie0.Play();	
			movie1.Pause();
			movie2.Pause();
			movie3.Pause();
			movie4.Pause();
			movie5.Pause();
			movie6.Pause();
			movie7.Pause();
			
			audio1.Pause();
			//audio2.Pause();
			//audio3.Pause();
			//audio4.Pause();
			
			
		}
		
		if(posMat2==1){
			GetComponent<RawImage>().texture = movie1 as MovieTexture;
			movie1.loop = true;
			audio1 = GetComponent<AudioSource>();
			audio1.clip = movie1.audioClip;
			movie1.Play();	
			audio1.Play();
			
			movie0.Pause();
			movie2.Pause();
			movie3.Pause();
			movie4.Pause();
			movie5.Pause();
			movie6.Pause();
			movie7.Pause();
		}
		
		if(posMat2==2){
			GetComponent<RawImage>().texture = movie2 as MovieTexture;
			movie2.loop = true;
			movie2.Play();	
			audio1.Pause();
			
			movie0.Pause();
			movie1.Pause();
			movie3.Pause();
			movie4.Pause();
			movie5.Pause();
			movie6.Pause();
			movie7.Pause();
			
		}
		
		if(posMat2==3){
			GetComponent<RawImage>().texture = movie3 as MovieTexture;
			movie3.loop = true;
			movie3.Play();	
			movie0.Pause();
			movie1.Pause();
			movie2.Pause();
			movie4.Pause();
			movie5.Pause();
			movie6.Pause();
			movie7.Pause();
		}
		
		if(posMat2==4){
			GetComponent<RawImage>().texture = movie4 as MovieTexture;
			movie4.loop = true;
			movie4.Play();	
			
			movie0.Pause();
			movie1.Pause();
			movie2.Pause();
			movie3.Pause();
			movie5.Pause();
			movie6.Pause();
			movie7.Pause();
			
		}
		
		if(posMat2==5){
			GetComponent<RawImage>().texture = movie5 as MovieTexture;
			movie5.loop = true;
			movie5.Play();	
			
			movie0.Pause();
			movie1.Pause();
			movie2.Pause();
			movie3.Pause();
			movie4.Pause();
			movie6.Pause();
			movie7.Pause();
			
		}
		
		if(posMat2==6){
			GetComponent<RawImage>().texture = movie6 as MovieTexture;
			movie6.loop = true;
			movie6.Play();	
			
			movie0.Pause();
			movie1.Pause();
			movie2.Pause();
			movie3.Pause();
			movie4.Pause();
			movie5.Pause();
			movie7.Pause();
			
		}
		
		if(posMat2==7){
			GetComponent<RawImage>().texture = movie7 as MovieTexture;
			movie7.loop = true;
			movie7.Play();	
			
			movie0.Pause();
			movie1.Pause();
			movie2.Pause();
			movie3.Pause();
			movie4.Pause();
			movie5.Pause();
			movie6.Pause();
			
		}
	}
}


