using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class videoplay : MonoBehaviour {

 public MovieTexture movie1;
 public MovieTexture movie2;
 public MovieTexture movie3;
 
 // Use this for initialization
 void Start () {
	GetComponent<RawImage>().texture = movie1 as MovieTexture;


	movie1.loop = true;

	movie1.Play();
 }

     
}﻿



