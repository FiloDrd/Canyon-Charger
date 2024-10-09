using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunzioniUtili : MonoBehaviour
{
    /// <summary> If the integer passed is 1 returns true, else it returns false </summary>
    public static bool IntToBool(int n) 
    {
        if (n == 1)
        {
            return true;
        }
        return false;
    }

    /// <summary> If the integer passed is true returns 1, else it returns 0 </summary>
    public static int BoolToInt(bool n)
    {
        if (n)
        {
            return 1;
        }else
        {
            return 0;
        }
        
    }

    /// <summary> Allows you to modify a string by passing the string, the character you want to add and the index of the character to delete</summary>
    public static string ModifyString(string stringa, char substitute, int index) 
    {
        char[] charArray = stringa.ToCharArray();
        charArray[index] = substitute;

        string tempString = "";


        for (int i = 0; i < charArray.Length; i++)
        {
            tempString = tempString + charArray[i].ToString();
        }

        return tempString;    
    }

    /// <summary> Returns a character of the string at the index postion</summary>
    public static char ReadString(string stringa,int index)
    {
        char[] charArray = stringa.ToCharArray();
        //print(charArray.Length + "    " + index );
        return charArray[index];
    }

    /// <summary> If the char you pass is equal to the trueValue it returns true, else it returns false</summary>
    public static bool CharToBool(char c, char trueValue)
    {
        if (c == trueValue)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    /// <summary> Turns the date into an integer by typing the format you want, you can pass as parameter: 
    /// day (returns the day);
    /// month (returns the month);
    /// year (returns the year);
    /// hour (returns the hour);
    /// minutes (returns the minute);
    /// seconds (returns the seconds);
    /// </summary>

    /// /// <param name="type">The format of date you want</param>
    /// <returns>The requested date as integer</returns>
    public static int GetDateValue(string type)
    {
        char[] date_in_array = System.DateTime.Now.ToString("dd/MM/yyyy/HH:mm:ss").ToCharArray();

        //print(System.DateTime.Now.ToString("dd/MM/yyyy/hh:mm:ss"));
        if (type == "day")
        {
            return int.Parse(System.DateTime.Now.ToString("dd"));
        } else if (type == "month")
        {
            return int.Parse(System.DateTime.Now.ToString("MM"));
        } else if (type == "year")
        {
            return int.Parse(System.DateTime.Now.ToString("yyyy"));
        } else if (type == "hour")
        {
            return int.Parse(System.DateTime.Now.ToString("HH"));
        }
        else if (type == "minutes")
        {
            return int.Parse(System.DateTime.Now.ToString("mm"));
        }
        else if (type == "seconds")
        {
            return int.Parse(System.DateTime.Now.ToString("ss"));
        }

        return 00;
    }
    /*public int[] pow;
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            print(i +": " + RandomNumberFromList(pow));
        }
        
    }*/

    public static int RandomNumberFromList(int[] Livelli)
    {
        
        List<int> Lista = new List<int>();

        int j = 0;
        for (int i = 0; i < Livelli.Length; i++)
        {
            if(Livelli[i] != 0)
            {
                Lista.Add(i);
                j++;
            }
        }

        //print("elementi: " + Lista.Count);
        int elementi = Lista.Count;
        /*for(int i = 0; i< Lista.Count; i++)
        {
            print(Lista[i]);
        }*/
        int index = Random.Range(0, elementi);
        return Lista[index];
    }
}
