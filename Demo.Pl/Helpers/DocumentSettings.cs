using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.Pl.Helpers
{
    public static class DocumentSettings
    {
        // Upload 
        public static string UploadFile(IFormFile file, string FolderName)
        {

            // 1- Get Located Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            // 2- Get File Name and Make it Unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- Get File Path[Folder Path + FileName]
            string FilePath = Path.Combine(FolderPath, FileName);

            // 4- Sava File as Streams
            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);

            // 5- Return File Name
            return FileName;
        }


        // Delete 

        public static void DeleteFile(string FileName, string FolderName)
        {

            // 1- Get File Path
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);

            // 2- Check File is Exists of Not
            if (File.Exists(FilePath))
            {
                // If File Exists Remove It
                File.Delete(FilePath);
            }
        }
    }
}
