using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        const Int64 INT64_MAX = 9223372036854775807;

        //欲張り法でやってみたが
        //実際は間違ってる
        //修正待ち
        static void Main(string[] args)
        {
            FileInfo     readFileInfo = new FileInfo("./txt/para.txt");
            StreamReader str1         = readFileInfo.OpenText();

            //1行目：パラメータ読み込む
            string   nextLine = str1.ReadLine();
            string[] tmpStrs  = nextLine.Split(',');

            str1.Close();

            var strList = new List<string>(tmpStrs);
            int listCount = strList.Count;
            string resultStr = "";
            for (int i=0;i<listCount;i++)
            {
                resultStr += GetMaxNum(ref strList);
            }

            //書き出しテキストを設定
            FileInfo writeFileInfo = new FileInfo("./txt/result.txt");
            StreamWriter sw1 = writeFileInfo.CreateText();
            sw1.WriteLine(resultStr);
            sw1.Close();
        }

        /// <summary>
        /// 文字列から頭文字が最も大きなものを探す
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        private static string GetMaxNum(ref List<string> strList)
        {
            if (strList.Count > 0)
            {
                string tmpStr = strList[0];

                if (strList.Count == 1)
                {
                    strList.Remove(tmpStr);
                    return tmpStr;
                }

                for (int i=1;i<strList.Count;i++)
                {
                    tmpStr = compareStringForInitialNum(tmpStr,strList[i],0);
                }

                strList.Remove(tmpStr);
                return tmpStr;
            }

            return "";
        }

        /// <summary>
        /// 文字列から頭文字が最も大きなものを探す
        /// 同じなら次の桁を比べる
        /// が、問題はここです、修正待ち
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string compareStringForInitialNum(string str1,string str2,int index)
        {
            if (str1.Length > 0 && str2.Length > 0)
            {
                if (index > str1.Length - 1)
                {
                    return str1;
                }
                else if (index > str2.Length - 1)
                {
                    return str2;
                }
                else if (str1[index] > str2[index])
                {
                    return str1;
                }
                else if (str1[index] < str2[index])
                {
                    return str2;
                }
                else if(str1[index] == str2[index])
                {
                    compareStringForInitialNum(str1, str2, index + 1);
                }
            }
            return "";
        }
    }
}
