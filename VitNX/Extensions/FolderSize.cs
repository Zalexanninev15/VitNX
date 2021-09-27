using System.IO;

namespace VitNX.Extensions 
{  
    class FolderSize
    {  
        public static long Result(DirectoryInfo d) 
        { 
            long size = 0; 
            FileInfo[] fis = d.GetFiles(); 
            foreach (FileInfo fi in fis) 
            { size += fi.Length; } 
            DirectoryInfo[] dis = d.GetDirectories(); 
            foreach (DirectoryInfo di in dis) 
            { size += Result(di); } 
            return size; 
        } 
    } 
}