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
     * Complete the 'getTotalX' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER_ARRAY b
     */

    public static int getTotalX(List<int> a, List<int> b)
    {
        List<int> multiples=new List<int>();
        List<int> factors=new List<int>();
        
        int lenA=a.Count;
        int lenB=b.Count;
        int k=1;
        for(int i=a[lenA-1];i<=b[0];i++){
            k=1;
            for(int j=0;j<lenA;j++){
                if(i%a[j]!=0){
                    k=0;
                    break;
                }
                k=1;
            }
            if(k==1){
                 multiples.Add(i);
                 Console.WriteLine(i);
            }
        }
        int l=1;
        int res=0;
        for(int i=0;i<multiples.Count;i++){
            l=1;
            for(int j=0;j<lenB;j++){
                if(b[j]%multiples[i]!=0){
                    l=0;
                    break;
                }
                
            }
            if(l==1){
                res++;
            }
        }
        
        return res;
         
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> brr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(brrTemp => Convert.ToInt32(brrTemp)).ToList();

        int total = Result.getTotalX(arr, brr);

        textWriter.WriteLine(total);

        textWriter.Flush();
        textWriter.Close();
    }
}
