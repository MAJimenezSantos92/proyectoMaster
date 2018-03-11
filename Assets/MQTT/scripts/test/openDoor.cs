using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class openDoor : MonoBehaviour {
 
    public string curPassword;
    public string input;
    public bool onTrigger;
    public bool doorOpen;
	public bool doorClose;
	public bool isClose;
    public bool keypadScreen;	
    public Transform soporte_01;

    public AudioClip[] soundToPlay;
    private AudioSource audio;
	
	void Start()
    {
        audio = GetComponent<AudioSource>();
		curPassword="2017";
    }

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }
 
    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
		doorClose=true;
		
		Invoke ("closePuerta", 9);
    }

	void closePuerta()
    {
        isClose = true;
		doorOpen = false;
    }
	
    void Update()
    {
        if(input == curPassword)
        {
            doorOpen = true;
			doorClose = true;
			isClose = false;
        }
 
        if(doorOpen)
        {
            var newRot = Quaternion.RotateTowards(soporte_01.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
            soporte_01.rotation = newRot;
			
        }
		
		if(isClose)
		{
            var newRot = Quaternion.RotateTowards(soporte_01.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 250);
            soporte_01.rotation = newRot;
		}

    }
 
	void RandomAudio()
    {
        if (audio.isPlaying){
            return;
                }
        audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audio.Play();

    }
	
    void OnGUI()
    {
        if(!doorOpen)
        {
            if(onTrigger)
            {
			
                GUI.Box(new Rect( (Screen.width / 2) - 100, Screen.height / 2, 200, 25), "Pulsa 'E' para introducir el código");
 
                if(Input.GetKeyDown(KeyCode.E))
                {
                    keypadScreen = true;
                    onTrigger = false;
                }
            }
 
            if(keypadScreen)
            {
                GUI.Box(new Rect((Screen.width / 2)- 100, (Screen.height / 2)-350, 320, 455), "");
                GUI.Box(new Rect((Screen.width / 2)- 100 +5, (Screen.height / 2)-350+5, 310, 25), input);
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100 +5, (Screen.height / 2)-350+35, 100, 100), "1"))
                {
                    input = input + "1";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+110, (Screen.height / 2)-350+35, 100, 100), "2"))
                {
                    input = input + "2";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+215, (Screen.height / 2)-350+35, 100, 100), "3"))
                {
                    input = input + "3";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+5, (Screen.height / 2)-350+140, 100, 100), "4"))
                {
                    input = input + "4";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+110, (Screen.height / 2)-350+140, 100, 100), "5"))
                {
                    input = input + "5";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+215, (Screen.height / 2)-350+140, 100, 100), "6"))
                {
                    input = input + "6";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+5, (Screen.height / 2)-350+245, 100, 100), "7"))
                {
                    input = input + "7";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+110, (Screen.height / 2)-350+245, 100, 100), "8"))
                {
                    input = input + "8";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+215, (Screen.height / 2)-350+245, 100, 100), "9"))
                {
                    input = input + "9";
					RandomAudio();
                }
 
                if(GUI.Button(new Rect((Screen.width / 2)- 100+110, (Screen.height / 2)-350+350, 100, 100), "0"))
                {
                    input = input + "0";
					RandomAudio();
                }
            }
        }
    }
}