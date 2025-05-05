using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'timeConversion' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string timeConversion(string s)
    {
        int len=s.Length;
        string lastPart=s.Substring(len-2,2);
        string firstPart=s.Substring(0,2);
        
        
        if (lastPart=="AM" && firstPart=="12"){
            string res="00"+s.Substring(2,6);
            return res;
        }
        else if(lastPart=="AM"){
            return s.Substring(0,8);
        }
        else if(lastPart=="PM"&&firstPart=="12")
        {
            string res=s.Substring(0,8);
            return res;
        }
        else
        {
            int fp=Convert.ToInt32(firstPart);
            int convertTime=fp+12;
            string res= Convert.ToString(convertTime)+s.Substring(2,6);
            return res;
        }
        
        // Console.WriteLine(firstPart);
        // return firstPart;
        
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.timeConversion(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
