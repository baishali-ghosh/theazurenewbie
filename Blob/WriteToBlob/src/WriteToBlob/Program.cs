using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace WriteToBlob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            String result = "";
            result = WriteBlob();
            //Console.WriteLine(result);
        }
        public static string WriteBlob()
        {
            string accountName = "ENTER_YOUR_STORAGE_ACCOUNT_NAME";
            string accountKey = "YOUR_STORAGE_ACCOUNT_KEY";
	    string response = "";
            try
            {

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = client.GetContainerReference("CONTAINER_NAME");
                sampleContainer.CreateIfNotExists();

                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference("NAME_TO_GIVE_TO_UPLOADED_IMAGE");
                using (Stream file = System.IO.File.OpenRead("LOCAL_PATH_OF_FILE_TO_UPLOAD"))
                {
                    blob.UploadFromStream(file);
                }
		response = "File uploaded successfully";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
		response = "An error occurred";            
            }
	    return response;
            
        }
    }
}
