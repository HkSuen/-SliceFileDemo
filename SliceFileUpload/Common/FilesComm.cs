using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SliceFileUpload.Common
{
    public class FilesComm
    {
        /// <summary>
        /// 获取指定文件的已上传数量
        /// </summary>
        /// <returns></returns>
        public static int GetChunkCount(string Path)
        {
            var dirInfo = new System.IO.DirectoryInfo(Path);
            int totalFile = 0;
            totalFile += dirInfo.GetFiles().Length;
            //foreach (System.IO.DirectoryInfo subdir in dirInfo.GetDirectories())
            //{
            //    totalFile += GetChunkCount(subdir);
            //}
            return totalFile;
        }

        /// <summary>
        /// 文件分块保存
        /// </summary>
        public static async void ChunkUpload(Stream stream,string path)
        {
            try
            {
                // Stream convert to byte
                byte[] fileBuf = new byte[stream.Length];
                stream.Read(fileBuf, 0, fileBuf.Length);
                //set current stream for begin start;
                stream.Seek(0, SeekOrigin.Begin);
                if (!File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(fileBuf, 0, fileBuf.Length);
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("写入文件出错：消息={0},堆栈={1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        public static async Task<bool> MergeFiles(string[] fileName,string outFileName)
        {
            int b;
            int n = fileName.Length;
            FileStream[] fileIn = new FileStream[n];
            using (FileStream fileOut = new FileStream(outFileName, FileMode.Create))
            {
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        fileIn[i] = new FileStream(fileName[i], FileMode.Open);
                        while ((b = fileIn[i].ReadByte()) != -1)
                            fileOut.WriteByte((byte)b);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        fileIn[i].Close();
                    }

                }
            }
            return true;
        }
    }
}
