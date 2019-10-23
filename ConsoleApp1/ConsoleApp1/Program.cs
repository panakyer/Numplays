using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        enum PA
        {
            NONE = -1,
            BLANK,//""
            PLUS,//"+"
            MINUS,//"-"
            MAX
        };

        //int[] nums  = {0,1,2,3,4,5,6,7,8,9 };
        //PA[]  paras = new PA[10];
        //Int64 result = 0;
        const Int64 INT64_MAX = 9223372036854775807;

        static void Main(string[] args)
        {
            FileInfo     readFileInfo = new FileInfo("./txt/para.txt");
            StreamReader str1         = readFileInfo.OpenText();

            //1行目：パラメータ読み込む
            string   nextLine = str1.ReadLine();
            string[] tmpStrs  = nextLine.Split(',');
            Int64[]  nums     = new Int64[tmpStrs.Length];

            for (int i =0;i<tmpStrs.Length; i++)
            {
                if (tmpStrs[i] != null && tmpStrs[i] != "")
                {
                    nums[i] = Int64.Parse(tmpStrs[i]);
                }
                else
                {
                    nums[i] = 0;
                }
            }

            //2行目：ターゲット読み込む
            nextLine     = str1.ReadLine();
            Int64 target = (nextLine != null && nextLine != "") ? (Math.Abs(Int64.Parse(nextLine)) < INT64_MAX? Int64.Parse(nextLine) : 0): 0;

            str1.Close();

            //演算子
            PA[] paras = new PA[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                paras[i] = PA.NONE;
            }

            //書き出しテキストを設定
            FileInfo writeFileInfo = new FileInfo("./txt/result.txt");
            StreamWriter sw1       = writeFileInfo.CreateText();

            //for (int i=0;i< nums.Length;i++)
            //{
            //    for (int j = 0; j < paras.Length; j++)
            //    {
            //        for (PA tmpPa = PA.BLANK; tmpPa < PA.MAX; tmpPa++)
            //        {
            //            paras[j] = tmpPa;
            //            if (isParasMakeTarget(nums, paras, target))
            //            {
            //                sw1.WriteLine(makeWriteLine(nums, paras));
            //            }
            //        }
            //    }

            //}
            tryMakeParas(paras.Length, nums, target, sw1, ref paras);

            sw1.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="paras"></param>
        /// <param name="target"></param>
        private static void tryMakeParas(int lastTurns, Int64[] nums,Int64 target,StreamWriter sw1,ref PA[] paras)
        {
            if (lastTurns > 0)
            {
                for (PA tmpPa = PA.BLANK; tmpPa < PA.MAX; tmpPa++)
                {
                    paras[paras.Length - lastTurns] = tmpPa;
                    tryMakeParas(lastTurns - 1, nums, target, sw1, ref paras);
                    if (isParasMakeTarget(nums, paras, target))
                    {
                        sw1.WriteLine(makeWriteLine(nums, paras));
                    }
                }
            }

        }

        /// <summary>
        /// 現在の演算子列を使えばターゲットにたどり着けるかな
        /// </summary>
        /// <param name="nums">数字列</param>
        /// <param name="paras">演算子</param>
        /// <returns></returns>
        private static bool isParasMakeTarget(Int64[] nums, PA[] paras, Int64 target)
        {
            Int64 result = 0;
            if (nums.Length > 0 && paras.Length > 0)
            {
                Int64 nextNum = 0;
                for (int i = nums.Length - 1; i >= 0 ; i --)
                {
                    if ((paras[i] == PA.BLANK || paras[i] == PA.NONE) && i!= 0)
                    {
                        nextNum = makeNumsToOne(nums[i - 1], nextNum);
                    }
                    else if (paras[i] == PA.PLUS || paras[i] == PA.MINUS || i == 0)
                    {
                        if (nextNum == 0)
                        {
                            result += (paras[i] == PA.MINUS ? -1 : 1) * nums[i];
                        }
                        else
                        {
                            result += (paras[i] == PA.MINUS ? -1 : 1) * nextNum;
                        }
                        nextNum = 0;
                    }
                    else
                    {
                        nextNum = 0;
                    }

                }
            }
            return result == target;
        }

        /// <summary>
        /// 2つの数字を一つに
        /// todo:桁数が長すぎになる時の処理
        /// </summary>
        /// <param name="num1">数字列を左から見れば前の数字</param>
        /// <param name="num2">数字列を左から見れば前の数字</param>
        /// <returns></returns>
        private static Int64 makeNumsToOne(Int64 num1,Int64 num2)
        {
            Int64 result = 0;
            result = num2 + num1 * Convert.ToInt64(Math.Pow(10, num2.ToString().Length));
            return result;
        }


        /// <summary>
        /// 出力stringを作る
        /// </summary>
        /// <param name="nums">数字列</param>
        /// <param name="paras">演算子</param>
        /// <returns></returns>
        private static string makeWriteLine(Int64[] nums,PA[] paras)
        {
            string result = "";
            if (nums.Length >0 && paras.Length > 0)
            {
                for (int i=0;i<nums.Length;i++)
                {
                    if (paras[i] == PA.BLANK || (i == 0 && paras[i] == PA.PLUS))
                    {
                        result += nums[i].ToString();
                    }
                    else if (paras[i] == PA.PLUS)
                    {
                        result += "+" + nums[i].ToString();
                    }
                    else if (paras[i] == PA.MINUS)
                    {
                        result += "-" + nums[i].ToString();
                    }
                }
            }
            return result;
        }
    }
}
