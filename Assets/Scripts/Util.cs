﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public static class Util
{
    private static System.Random _random;
    public static System.Random random{
        get{
            if (_random == null){
                _random = new System.Random();
            }
            return _random;
        }
    }
    public static float GetNormalDistFloat(float mean, float stdDev){
		double u1 = 1.0-random.NextDouble(); //uniform(0,1] random doubles
		double u2 = 1.0-random.NextDouble();
		double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
		double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

		return (float)randNormal;
	}

	public static float GetNormalDistFloat(float mean, float stdDev, float min, float max){
		float norm = GetNormalDistFloat(mean, stdDev);
		norm = Mathf.Max(min, norm);
		norm = Mathf.Min(max, norm);

		return norm;
	}

    public static Quaternion RandomYEuler(){
        int angle = random.Next(0,360);
        return Quaternion.Euler(0,angle,0);
    }
}