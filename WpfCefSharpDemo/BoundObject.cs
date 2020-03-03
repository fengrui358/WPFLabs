using System;
using System.IO;
using Newtonsoft.Json;

namespace WpfCefSharpDemo
{
    public class BoundObject
    {
        //属性在js中不行
        public string TestData = Guid.NewGuid().ToString();

        public int Add(int a, int b)
        {
            return a + b;
        }

        public string GetData()
        {
            return DateTime.Now.ToString();
        }

        public void WriteData(byte[] bytes)
        {
            
        }

        public void WriteStringData(string str, string str2)
        {
            var bytes = JsonConvert.DeserializeObject<byte[]>(str2);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");
            File.WriteAllBytes(filePath, bytes);
        }

        public byte[] GetBytes()
        {
            //读取测试文件，写入数据
            var path = @"D:\Users\rui\Desktop\nspclient_v1.2_final2.zip";
            var fileContens = File.ReadAllBytes(path);

            return fileContens;
        }
    }
}
