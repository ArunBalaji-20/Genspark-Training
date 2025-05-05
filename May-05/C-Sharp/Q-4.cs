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
     * Complete the 'birthdayCakeCandles' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY candles as parameter.
     */

    public static int birthdayCakeCandles(List<int> candles)
    {
        int n=candles.Count;
        
        int result=1;
        int largest=candles[0];
        for(int i=1;i<n;i++)
        {
            if(candles[i]>largest){
                largest=candles[i];
                result=1;
            }
            else if(candles[i]==largest){
                result++;
            }
            else{
                
            }
        }
        // candles.Sort();
        // int largest=candles[n-1];
        // for(int i=n-2;i>0;i--){
        //     if(largest==candles[i]){
        //         result++;
        //     }
        //     else{
        //         break;
        //     }
        // }
        Console.WriteLine(result);
        return result;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int candlesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> candles = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(candlesTemp => Convert.ToInt32(candlesTemp)).ToList();

        int result = Result.birthdayCakeCandles(candles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
