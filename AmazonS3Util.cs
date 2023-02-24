using Amazon.Runtime;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using System.Configuration;

namespace S3DownloadFile
{
    public class AmazonS3Util
    {
        // Add your AWS credentials here
        const string accessKey = "";
        const string secretKey = "";
        // Bucket Name where the file located
        const string bucketName = "";
        // Add Prefix If any ( prefix + "/")
        const string prefix = "prefix/";
        // Optional: If you have list of files and you want to get the latest 
        List<string> s3ObjectListVersion = new List<string>();
        // S3Client to establish the connection
        AmazonS3Client S3Client = null;
        // Temp Value to compare S3 file version
        public string s3Version = "1.0";

        public void GetFoldersNamesFromS3()
        {
            // Connect with s3
            try
            {
                var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);
                var awsConfig = new AmazonS3Config();
                // Change region if needed
                awsConfig.RegionEndpoint = RegionEndpoint.EUNorth1;
                S3Client = new AmazonS3Client(awsCredentials, awsConfig);
                var r = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    Prefix = prefix,
                    Delimiter = "/",
                    MaxKeys = 100
                };

                ListObjectsV2Response responseObject = S3Client.ListObjectsV2(r);

                responseObject.CommonPrefixes.ForEach((o) =>
                {
                    s3ObjectListVersion.Add(o);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetLatestSetupFileVersion()
        {
            // This code is needed if you want to retrieve the version via MetaData
            /*var utility = new TransferUtility(S3Client);
            string s3Version = "0.0";
            int fileIndex = 0;
            for (int i = 0; i < response.S3Objects.Count - 1; i++)
            {
                if (response.S3Objects[i].Key.EndsWith(".msi"))
                {
                    var metaResponse = await S3Client.GetObjectMetadataAsync("centific-version-checker", response.S3Objects[i].Key);
                    MetadataCollection mc = metaResponse.Metadata;
                    string metaVersion = mc["x-amz-meta-version"];
                    if (s3Version.CompareTo(metaVersion) <= 0)
                    {
                        s3Version = metaVersion;
                        fileIndex = i;
                    }
                }
                else
                {
                    continue;
                }

            }
            */

            string version = "0.0";
            // Get Latest version number
            foreach (var o in s3ObjectListVersion)
            {
                version = Regex.Replace(o, "[^0-9_.]+", "", RegexOptions.Compiled);
                if (version.CompareTo(s3Version) > 0)
                    s3Version = version;
            }
            return s3Version;
        }

        public async Task DownloadTheNewSetupFile(string s3Version)
        {
            try
            {
                var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);
                var awsConfig = new AmazonS3Config();
                // Change region if needed
                awsConfig.RegionEndpoint = RegionEndpoint.EUNorth1;
                var S3Client = new AmazonS3Client(awsCredentials, awsConfig);
                var r = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    Prefix = prefix + "v" + s3Version + "/" + ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location).AppSettings.Settings["VersionStability"].Value + "/",
                    MaxKeys = 100
                };
                var response = await S3Client.ListObjectsV2Async(r, CancellationToken.None);
                var utility = new TransferUtility(S3Client);

                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location).AppSettings.Settings["MainFolder"].Value;
                utility.Download($"{downloadPath}\\{response.S3Objects.Where(x => x.Key.Contains(s3Version)).ToList()[0].Key}", bucketName, response.S3Objects.Where(x => x.Key.Contains(s3Version)).ToList()[0].Key);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
