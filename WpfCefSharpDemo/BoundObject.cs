using System;
using System.IO;

namespace WpfCefSharpDemo
{
    public class BoundObject
    {
        //属性在js中不行
        public string TestData { get; set; } = Guid.NewGuid().ToString();

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

        }

        public byte[] GetBytes()
        {
            return new byte[]{1,2};
        }
    }
}
