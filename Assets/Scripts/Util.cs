using System;
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

    public static T RandomSelection<T>(this IEnumerable<T> enumerable){
        return RandomSelection(enumerable, t => 1);
    }

    public static T RandomSelection<T>(this IEnumerable<T> enumerable, Func<T, int> weightFunc)
    {
        int totalWeight = 0; // this stores sum of weights of all elements before current
        T selected = default(T); // currently selected element
        foreach (var data in enumerable)
        {
            int weight = weightFunc(data); // weight of current element
            int value = random.Next(totalWeight + weight); // random value
            if (value >= totalWeight) // probability of this is weight/(totalWeight+weight)
                selected = data; // it is the probability of discarding last selected element and selecting current one instead
            totalWeight += weight; // increase weight sum
        }

		if (selected == null){

		}

        return selected; // when iterations end, selected is some element of sequence. 
    }

    public static List<T> RandomSelections<T>(this IEnumerable<T> enumerable, int count){
        List<T> temp = enumerable.ToList();
        List<T> listToReturn = new List<T>();
        if (temp.Count == 0){
            return listToReturn;
        }
        while(count > 0){
            T item = RandomSelection(temp, t => 1);
            listToReturn.Add(item);
            temp.Remove(item);
            if (temp.Count == 0){
                temp = enumerable.ToList();
            }
            count--;
        }
        return listToReturn;
    }
}
