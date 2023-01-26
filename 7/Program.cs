using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode7
{
    internal class Program
    {
        static Folder currentFolder;
        static IDictionary<string, Folder> allFolders = new Dictionary<string, Folder>();
        static ArrayList smallFolders = new ArrayList();
        public static string baseFolderName = "baseFolder";
        static int totalUsedSpace;
        static Folder baseFolder;


        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\files.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            currentFolder = new Folder(baseFolderName, null);
            allFolders.Add(currentFolder.GetPath(), currentFolder); ;
            baseFolder = currentFolder;
            foreach (string line in lines)
            {
                ParseLine(line);
            }
            GetSmallFoldersSumSize();
            totalUsedSpace = baseFolder.GetTotalSize();
            Console.WriteLine("used space = {0}", totalUsedSpace);
            GetDeleteFolderSize();
        }

        private static void ParseLine(string line)
        {
            string[] data = line.Split(' ');
            string firstParam = data[0];
            string secondParam = data[1];
            if (Char.IsNumber(firstParam[0])) AddFileSizeToCurrentFolder(int.Parse(firstParam)); //add file size
            else if (firstParam == "dir")  //moved in folder
            {
                
                if (!allFolders.ContainsKey(secondParam))
                {
                    Folder newSubFolder = CreateNewFolder(secondParam);
                    currentFolder.AddSubFolder(newSubFolder);
                }
            }
            else if(firstParam == "$" && secondParam == "cd")
            {
                string thirdParam = data[2];
                if(thirdParam == "..") // move to parent folder
                {
                    if(currentFolder.GetName() != baseFolderName) currentFolder = currentFolder.GetParentFolder();
                }
                else if(thirdParam == "/") //move to base folder
                {
                    currentFolder = allFolders[baseFolderName + "/"];
                }
                else // move folder up
                {
                    String path = currentFolder.GetPath() + thirdParam + "/";
                    currentFolder = allFolders[path];
                }
            }
        }

        private static Folder CreateNewFolder(string secondParam)
        {
            Folder newFolder = new Folder(secondParam, currentFolder);
            //Console.WriteLine("Adding {0} to folders", newFolder.GetPath());
            allFolders.Add(newFolder.GetPath(), newFolder);
            return newFolder;
        }

        private static void AddFileSizeToCurrentFolder(int sizeOfFile)
        {
            currentFolder.AddSize(sizeOfFile);
        }

        public static void AddSmallFolder(Folder folder)
        {
            smallFolders.Add(folder);
        }

        private static void GetSmallFoldersSumSize()
        {
            int sumSmallFoldersSize = 0;
            Folder tempFolder;
            int tempFolderSize;
            foreach (KeyValuePair<string, Folder> entry in allFolders)
            {
                tempFolder = entry.Value;
                tempFolderSize = tempFolder.GetTotalSize();
                if (tempFolderSize < 100000)
                {
                    sumSmallFoldersSize += tempFolderSize;
                    //Console.WriteLine("folder {0} is small, size = {1}", tempFolder.GetPath(), tempFolderSize);
                }
            }
            //Console.WriteLine("sum small size = {0}", sumSmallFoldersSize);
        }

        private static void GetDeleteFolderSize()
        {
            Folder tempFolder;
            int tempFolderSize;
            int folderSizeToDelete = int.MaxValue;

            int neededSpace = (70000000 - 30000000 - totalUsedSpace) * - 1;
            foreach (KeyValuePair<string, Folder> entry in allFolders)
            {
                tempFolder = entry.Value;
                tempFolderSize = tempFolder.GetTotalSize();
                if (tempFolderSize > neededSpace && tempFolderSize < folderSizeToDelete)
                {
                    folderSizeToDelete = tempFolderSize;
                    Console.WriteLine("folder {0} can be deleted, size = {1}", tempFolder.GetPath(), tempFolderSize);
                }
            }
            Console.WriteLine("deleted size = {0}", folderSizeToDelete);
        }
    }
}
