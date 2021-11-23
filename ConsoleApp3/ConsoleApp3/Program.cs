using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        const Int64 INT64_MAX = 9223372036854775807;
        static void Main(string[] args)
        {
            FileInfo readFileInfo = new FileInfo("./txt/para.txt");
            StreamReader str1 = readFileInfo.OpenText();

            //第一行：读入球序列，按【，】划分开
            string nextLine = str1.ReadLine();
            string[] tmpStrs = nextLine.Split(',');
            Int64[] nums = new Int64[tmpStrs.Length];

            for (int i = 0; i < tmpStrs.Length; i++)
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

            //第二行：读入池子数
            nextLine = str1.ReadLine();
            Int64 target = (nextLine != null && nextLine != "") ? (Math.Abs(Int64.Parse(nextLine)) < INT64_MAX ? Int64.Parse(nextLine) : 1) : 1;
            str1.Close();





















            //结果输出文本
            FileInfo writeFileInfo = new FileInfo("./txt/result.txt");
            StreamWriter sw1 = writeFileInfo.CreateText();
            //tryMakeParas(paras.Length, nums, target, sw1, ref paras);
            sw1.Close();
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private float makeVariance(Int64[] nums)
        {
            if (nums.Length == 0)
            {
                return -1f;
            }

            int sum = 0;
            for (int i=0;i<nums.Length;i++)
            {
                sum += 0;
            }

            float aver = sum * 1.0f / nums.Length;

            float result = 0f;
            for (int i = 0; i < nums.Length; i++)
            {
                result += (aver - nums[i]) * (aver - nums[i]);
            }
            result = (float)(Math.Sqrt(result / nums.Length));

            return result;
        }

    }
}
