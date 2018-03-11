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

[RequireComponent(typeof(AudioSource))]
public class vaca : MonoBehaviour {
	GameObject leche;
	GameObject caca;
	GameObject comida;
	
	GameObject vaca1;
	
	GameObject vacaDentro;
	
	
	public AudioClip defecasfx;
	public AudioClip lechesfx;
	public AudioClip moscasfx;
	public AudioClip comidasfx;
	public AudioClip escanerfx;
	public AudioClip vacafx;
	public AudioClip retirarfx;
    
	public AudioSource audio;
	
	private MqttClient client;

	public string topic;
	public string topicNuevo;
	
	
	
	
	void Start () {
	
	 
	 
		audio = GetComponent<AudioSource>();
		
		// create client instance 
		client = new MqttClient(IPAddress.Parse("192.168.0.15"),1883 , false , null ); 
		
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Publish("controlVoz", System.Text.Encoding.UTF8.GetBytes(""), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "controlVoz" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		
		
		leche=GameObject.Find("cubo");
		caca=GameObject.Find("regalito");
		comida=GameObject.Find("com");
	
		vaca1=GameObject.Find("vaca1");
		vacaDentro=GameObject.Find("vacaDentro");
	
		leche.GetComponent<Renderer>().enabled = false;
		caca.GetComponent<Renderer>().enabled = false;
		comida.GetComponent<Renderer>().enabled = false;
	
		vaca1.GetComponent<Renderer>().enabled = true;
		vacaDentro.GetComponent<Renderer>().enabled = false;
	

		
		GameObject.Find("relajar").GetComponent<Button>().enabled = true;
		GameObject.Find("relajar").GetComponent<Image>().enabled = true;
		GameObject.Find("relajar2").GetComponent<Button>().enabled = false;
		GameObject.Find("relajar2").GetComponent<Image>().enabled = false;
			
		GameObject.Find("contenleche").GetComponent<Button>().enabled = false;
		GameObject.Find("contenleche").GetComponent<Image>().enabled = false;
		GameObject.Find("contencomer").GetComponent<Button>().enabled = false;
		GameObject.Find("contencomer").GetComponent<Image>().enabled = false;
		GameObject.Find("contendefeca").GetComponent<Button>().enabled = false;
		GameObject.Find("contendefeca").GetComponent<Image>().enabled = false;
    }
	public void moscasdesap () {
		GameObject.Find("moscas").GetComponent<Renderer>().enabled = true;
		GameObject.Find("moscas2").GetComponent<Renderer>().enabled = true;
		

					 
					
					 
					
					 
					 
	}	
	

	public void moooo () {
		if(!audio.isPlaying)
		{	 
	
			audio.PlayOneShot(vacafx, 0.7F);	 
		}
	}	
	
	
	void Update () {
		
	 if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
         //Application.Quit(); // Quits the game
		   SceneManager.LoadScene ("MENU");
     }
	 
			if(topicNuevo.Equals("defecar")){
				
				onClickDefecar();
				
			
			}
			
			
			
			
			if(topicNuevo.Equals("soplido")){
				
				GameObject.Find("moscas").GetComponent<Renderer>().enabled = false;
				GameObject.Find("moscas2").GetComponent<Renderer>().enabled = false;
				
				
				audio.PlayOneShot(moscasfx, 0.2F);
				
				 
				Invoke("moscasdesap", 8);
				
				Invoke("moooo", 6);

			}
			
			
			
			
			
			if(topicNuevo.Equals("limpiar")){
				
				onClickContentDefca();
			}
			
			if(topicNuevo.Equals("estudiar")){
				
				onClickRelajar();
			}
			
			if(topicNuevo.Equals("volver")){
				
				onClickRelajar();
			}
			
			
			
			if(topicNuevo.Equals("comer")){
				
				onClickContentComer();
			}
			
			if(topicNuevo.Equals("dar de comer")){
				
				onClickComer();
			}
			
			
			if(topicNuevo.Equals("ordeñar")){
				
				onClickLeche();
			}
			
			if(topicNuevo.Equals("retirar")){
				
				onClickContentLeche();
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
		
		
	}
	
	// Use this for initialization
    public void onClickComer()
    {
		Debug.Log("COMIENDO");
		comida.GetComponent<Renderer>().enabled = true;
		GameObject.Find("contencomer").GetComponent<Button>().enabled = true;
		GameObject.Find("contencomer").GetComponent<Image>().enabled = true;
		
		GameObject.Find("comer").GetComponent<Button>().enabled = false;
		GameObject.Find("comer").GetComponent<Image>().enabled = false;
		
		
			if(!audio.isPlaying)
		{	 
			audio.PlayOneShot(retirarfx, 0.7F);
		}
		
		
		
    }

   	public void onClickDefecar()
    {
		Debug.Log("DEFECAR");
		caca.GetComponent<Renderer>().enabled = true;
		GameObject.Find("contendefeca").GetComponent<Button>().enabled = true;
		GameObject.Find("contendefeca").GetComponent<Image>().enabled = true;
		
		GameObject.Find("defecar").GetComponent<Button>().enabled = false;
		GameObject.Find("defecar").GetComponent<Image>().enabled = false;

		
			
			if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(defecasfx,  0.7F);
		}
		
		
    }
	
	public void onClickLeche()
    {
		Debug.Log("LECHE");
		leche.GetComponent<Renderer>().enabled = true;
		GameObject.Find("contenleche").GetComponent<Button>().enabled = true;
		GameObject.Find("contenleche").GetComponent<Image>().enabled = true;
		
		GameObject.Find("leche").GetComponent<Button>().enabled = false;
		GameObject.Find("leche").GetComponent<Image>().enabled = false;
	
		
		
			if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(lechesfx,  0.7F);
		}
    }
	
	
	public void onClickRelajar1()
    {
		Debug.Log("RELAJAR");
		
		
		vaca1.GetComponent<Renderer>().enabled = false;
		vacaDentro.GetComponent<Renderer>().enabled = true;
		

			
		GameObject.Find("relajar").GetComponent<Button>().enabled = false;
		GameObject.Find("relajar").GetComponent<Image>().enabled = false;
		GameObject.Find("relajar2").GetComponent<Button>().enabled = true;
		GameObject.Find("relajar2").GetComponent<Image>().enabled = true;
		
			if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(escanerfx,  0.7F);
		}
		
		
		
    }
	
	
		public void onClickRelajar2()
    {
		Debug.Log("RELAJAR");
		
		
		
			vaca1.GetComponent<Renderer>().enabled = true;
			vacaDentro.GetComponent<Renderer>().enabled = false;
		
			GameObject.Find("relajar").GetComponent<Button>().enabled = true;
		GameObject.Find("relajar").GetComponent<Image>().enabled = true;
		GameObject.Find("relajar2").GetComponent<Button>().enabled = false;
		GameObject.Find("relajar2").GetComponent<Image>().enabled = false;
			
		if(!audio.isPlaying)
		{	 
			audio.PlayOneShot(retirarfx,  0.7F);
		}
		
    }
	
	
	
	
	public void onClickRelajar()
    {
		Debug.Log("RELAJAR");
		
		if(topicNuevo.Equals("estudiar")){
			vaca1.GetComponent<Renderer>().enabled = false;
			vacaDentro.GetComponent<Renderer>().enabled = true;
					audio.PlayOneShot(escanerfx,  0.7F);
			
		}
		
		if(topicNuevo.Equals("volver")){
			vaca1.GetComponent<Renderer>().enabled = true;
			vacaDentro.GetComponent<Renderer>().enabled = false;
			
				
				
				audio.PlayOneShot(retirarfx,  0.7F);
	
		
		
		}
    }
	
	
	
	public void onClickContentLeche()
    {
		Debug.Log("CONTENT LECHE");
		leche.GetComponent<Renderer>().enabled = false;
		GameObject.Find("contenleche").GetComponent<Button>().enabled = false;
		GameObject.Find("contenleche").GetComponent<Image>().enabled = false;
		
		GameObject.Find("leche").GetComponent<Button>().enabled = true;
		GameObject.Find("leche").GetComponent<Image>().enabled = true;
		
		
				if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(retirarfx,  0.7F);
		}
		
    }
	
	public void onClickContentComer()
    {
		Debug.Log("CONTENT COMER");
		comida.GetComponent<Renderer>().enabled = false;
		GameObject.Find("contencomer").GetComponent<Button>().enabled = false;
		GameObject.Find("contencomer").GetComponent<Image>().enabled = false;
		
		GameObject.Find("comer").GetComponent<Button>().enabled = true;
		GameObject.Find("comer").GetComponent<Image>().enabled = true;
		
		
				if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(comidasfx,  0.7F);
		}
		
    }
	
	public void onClickContentDefca()
    {
		Debug.Log("CONTENT COMER");
		caca.GetComponent<Renderer>().enabled = false;
		GameObject.Find("contendefeca").GetComponent<Button>().enabled = false;
		GameObject.Find("contendefeca").GetComponent<Image>().enabled = false;
		
		GameObject.Find("defecar").GetComponent<Button>().enabled = true;
		GameObject.Find("defecar").GetComponent<Image>().enabled = true;

		
				if(!audio.isPlaying)
		{	 
				audio.PlayOneShot(retirarfx,  0.7F);
		}
		
    }
}

