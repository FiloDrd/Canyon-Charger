using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBSkin : MonoBehaviour
{
	public Color[] memColors;
	public Color startColor;
	public Color endColor;

	public Material horseMat;

	public float speed = 5.0f;
	float startTime;
	



	private void Awake()
	{
		startTime = Time.time;
		startColor = memColors[0];
		endColor = memColors[1];
	}

	private void Update()
	{
		if(startColor == memColors[0])
        {
			changeColor();
		}
		if(horseMat.color == endColor)
        {
			startColor = memColors[1];
			endColor = memColors[2];
			startTime = Time.time;
		}
		if (startColor == memColors[1])
		{
			changeColor();
		}
		if (horseMat.color == endColor)
		{
			startColor = memColors[2];
			endColor = memColors[3];
			startTime = Time.time;
		}
		if (startColor == memColors[2])
		{
			changeColor();
		}
		if (horseMat.color == endColor)
		{
			startColor = memColors[3];
			endColor = memColors[4];
			startTime = Time.time;
		}
		if (startColor == memColors[3])
		{
			changeColor();
		}
		if (horseMat.color == endColor)
		{
			startColor = memColors[4];
			endColor = memColors[5];
			startTime = Time.time;
		}
		if (startColor == memColors[4])
		{
			changeColor();
		}
		if (horseMat.color == endColor)
		{
			startColor = memColors[5];
			endColor = memColors[0];
			startTime = Time.time;
		}
		if (startColor == memColors[5])
		{
			changeColor();
		}
		if (horseMat.color == endColor)
		{
			startColor = memColors[0];
			endColor = memColors[1];
			startTime = Time.time;
		}




	}
	
	private void changeColor()
	{
        
			float t = (Time.time - startTime) * speed;
			horseMat.color = Color.Lerp(startColor, endColor, t);
		
			
		
	}

	

	

	
}
