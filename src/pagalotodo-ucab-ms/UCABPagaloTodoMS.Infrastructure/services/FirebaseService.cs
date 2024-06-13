using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace UCABPagaloTodoMS.Infrastructure.Services
{
    public class Firebase
    {
        private readonly GoogleCredential credential;
        private readonly StorageClient storageClient;
        private readonly string Bucket = "pagalotodoucab-bae82.appspot.com";

        public Firebase()
        {
            GoogleCredential credential = GoogleCredential.GetApplicationDefault();
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // Si la instancia actual no es nula, eliminarla
            if (app != null)
            {
                app.Delete();
            }

            app = FirebaseApp.Create(new AppOptions()
            {
                Credential = credential
            });
            storageClient = StorageClient.Create();
        }

        public async Task<string> ReadFileContentsAsync(string objectName)
        {
            using (var stream = new MemoryStream())
            {
                try
                {
                    await storageClient.DownloadObjectAsync(Bucket, objectName, stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async static void UploadFile(IFormFile file)
        {
            var credential = GoogleCredential.GetApplicationDefault();
            string bucketName = "pagalotodoucab-bae82.appspot.com";
            StorageClient _storageClient = StorageClient.Create();
            var objectName = file.FileName;
            var contentType = file.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await _storageClient.UploadObjectAsync(bucketName, objectName, contentType, memoryStream);
            }
        }
    }
}
