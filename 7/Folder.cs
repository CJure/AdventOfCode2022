using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode7
{
    internal class Folder
    {
        string name;
        Folder parentFolder;
        int filesSize = 0;
        Dictionary<string, Folder> subFolders = new Dictionary<string, Folder>();

        public Folder(string name, Folder parentFolder)
        {
            this.name = name;
            this.parentFolder = parentFolder;
        }

        public void AddSize(int size)
        {
            this.filesSize += size;
        }

        public void AddSubFolder(Folder folder)
        {
           // Console.WriteLine("Adding subFolder {0} to folder{1}", folder.GetName(), name);
            subFolders.Add(folder.name, folder);
        }

        public int GetTotalSize()
        {
            int totalSize = filesSize;
            Folder tempFolder;
            foreach (KeyValuePair<string, Folder> subFolder in subFolders)
            {
                tempFolder = subFolder.Value;
                totalSize += tempFolder.GetTotalSize();
            }
            Console.WriteLine("Total size of {0} = {1}", GetPath(), totalSize);
            return totalSize;
        }

        internal string GetName()
        {
            return name;
        }

        internal Folder GetParentFolder()
        {
            return parentFolder;
        }

        public string GetPath()
        {
            if(name != Program.baseFolderName)
            {
                return parentFolder.GetPath() + name + "/";
            }
            else
            {
                return GetName() + "/";
            }
        }
    }
}
