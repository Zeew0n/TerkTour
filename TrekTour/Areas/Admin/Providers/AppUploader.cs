using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TrekTour.Areas.Admin.Providers
{
    public class AppUploader
    {
        public static bool DeleteAllFileFromDirectory(string diretoryPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(diretoryPath);
                FileInfo[] rgFiles = di.GetFiles();
                foreach (FileInfo fi in rgFiles)
                {
                    FileInfo TheFile = new FileInfo(fi.FullName);
                    if (TheFile.Exists)
                    {
                        File.Delete(fi.FullName);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool DeleteFileByName(string directoryPath, string FileName)
        {
            try
            {
                //string[] filePaths = Directory.GetFiles(@"D:\workflow\WorkFlow3\WorkFlow\Content\Uploaded\Document", FileName);                
                string[] filePaths = Directory.GetFiles(directoryPath, FileName);
                foreach (string file in filePaths)
                {
                    FileInfo TheFile = new FileInfo(file);
                    if (TheFile.Exists)
                    {
                        File.Delete(file);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CreateDirectory(string directoryName, string parentDirectoryPath)
        {
            try
            {
                string newDirectory = parentDirectoryPath + "\\" + directoryName;
                if (Directory.Exists(newDirectory))
                {
                    return false;
                }

                DirectoryInfo di = Directory.CreateDirectory(newDirectory);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DeleteDirectory(string diretoryPath)
        {
            try
            {
                if (Directory.Exists(diretoryPath))
                {
                    Directory.Delete(diretoryPath, true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsDirectoryExist(string directoryName, string parentDirectoryPath)
        {
            string newDirectory = parentDirectoryPath + "\\" + directoryName;
            if (Directory.Exists(newDirectory))
            {
                return true;
            }
            return false;
        }

        public static bool IsFileExist(string directoryPath, string FileName)
        {
            string newDirectory = directoryPath + "\\" + FileName;
            if (File.Exists(newDirectory))
            {
                return true;
            }
            return false;
        }

        /* public static bool UploadFile(HttpPostedFileBase UploadedFile, string UploadDirPath = "~/Content/Uploaded/")
         {
             string UploadedFileName = UploadFileMaster(UploadedFile, UploadDirPath, UploadedFile.FileName);
             if (UploadedFileName != "")
             {
                 return UploadedFileName;
             }
             return true;

         }
         */

        public static string UploadDocumentAndRename(HttpPostedFileBase UploadedFile, ContentPathMode mode, string UploadDirPath = "~/Upload/Package")
        {
            string UploadedFileName = "";
            DateTime dt = DateTime.Now;
            string fileNamePrefix = "";
            if (UploadedFile.ContentLength > 0)
            {
                fileNamePrefix = Guid.NewGuid().ToString("D");
                var fileName = fileNamePrefix + Path.GetExtension(UploadedFile.FileName);
                UploadedFileName = UploadFileMaster(UploadedFile, mode, UploadDirPath, fileName);
            }
            return UploadedFileName;
        }

        public static string UploadFileAndRename(HttpPostedFileBase UploadedFile, ContentPathMode mode, string UploadDirPath = "~/Upload/Package",
            bool RenameInGuidFormat = true)
        {

            string UploadedFileName = "";
            DateTime dt = DateTime.Now;
            string fileNamePrefix = "";
            if (UploadedFile.ContentLength > 0)
            {
                if (RenameInGuidFormat)
                {

                    fileNamePrefix = Guid.NewGuid().ToString() + "__";
                }
                else
                {
                    fileNamePrefix = string.Format("{0:dd-MM-yyyy-HH.mm.ss}", dt) + "__";

                }
                var fileName = fileNamePrefix + Path.GetFileName(UploadedFile.FileName);
                UploadedFileName = UploadFileMaster(UploadedFile, mode, UploadDirPath, fileName);
            }
            return UploadedFileName;
        }

        private static string UploadFileMaster(HttpPostedFileBase UploadedFile, ContentPathMode mode, string UploadDirPath, string FileRenameToNewName = "")
        {
            string UploadedFileName = "";
            if (UploadedFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(UploadedFile.FileName);
                UploadedFileName = (FileRenameToNewName == "" ? fileName : FileRenameToNewName);
                string path = string.Empty; ;
                if (mode.ToString() == "relative")
                {
                    path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(UploadDirPath), UploadedFileName);
                }
                else
                {
                    path = Path.Combine(UploadDirPath, UploadedFileName);

                }
                UploadedFile.SaveAs(path);

            }
            return UploadedFileName;

        }

        public enum ContentPathMode
        {
            relative,
            absolute,

        }

        public static void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
             }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(NewFile);
        }
    }
}