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
     * Complete the 'staircase' function below.
     *
     * The function accepts INTEGER n as parameter.
     */

    public static void staircase(int n)
    {
        string line="";
        for(int i =1 ;i<=n;i++)
        {
            for(int j=0;j<n-i ;j++){
                line=line+" ";
            }
            
            for(int k=0 ;k<i;k++){
                line=line+"#";
            }
            Console.WriteLine(line);
            line="";
        }
        
    }
 

}
 

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Result.staircase(n);
    }
}
