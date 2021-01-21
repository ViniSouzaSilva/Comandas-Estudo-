using AmbiStore.Shared.EFCore.Models;   
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace AmbiStore.Shared.Libraries
{
    public static class Static
    {
        public static string GetMD5Hash(string text)
        {
            using var md5Hash = new HMACMD5(Encoding.UTF8.GetBytes("KPTa"));
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();

        }

        //public static int PropertyOne { get; set; }
        public static FUNCIONARIO FUN_LOGADO { get; set; }

        public static string GetSerialHexNumberFromExecDisk()
        {
            //uint uintSerialNum, uintDummy1, uintDummy2;
            GetVolumeInformation(Path.GetPathRoot(Environment.CurrentDirectory), null, 0, out uint uintSerialNum, out _, out _, null, 0);
            return uintSerialNum.ToString("X");
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetVolumeInformation(string rootPathName,
                                               StringBuilder volumeNameBuffer,
                                               int volumeNameSize,
                                               out uint volumeSerialNumber,
                                               out uint maximumComponentLength,
                                               out uint fileSystemFlags,
                                               StringBuilder fileSystemNameBuffer,
                                               int nFileSystemNameSize);
    }
}