namespace student_permit_system.PL.Helper
{
    public class DocumentConf
    {
        public static string DocumentUpload(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0 || string.IsNullOrEmpty(folderName))
            {
                // Handle the case where the file is null, has no content, or folderName is empty
                return null;
            }

            // Construct the folder path
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            // Generate a unique file name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(folderpath, fileName);

            // Copy the file to the specified path
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return fileName;
        }

        public static void DcoumentDelete(string fileName, string folderName)
        {
            if (fileName is not null && !string.IsNullOrEmpty(folderName))
            {
                // Construct the file path
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

                // Check if the file exists, if so, delete it
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
