# S3-Download-File
This is a brief code using C# to download a file from AWS S3.

"AmazonS3Util.cs" includes 3 main functions:

1) Check the names of folders in case you name your folders in S3 according to the version number.
2) Return the latest version string.
3) Download the file.

Steps to make the code works for you:

1) Add your AWS credentials.
2) Add Bucket Name and Prefix.
3) Change "MainFolder" and 'VersionStability" in the "App.config" if needed.
4) Change the folder structure in S3 to match the code or both to match your need wherever needed. The default is:

"s3://[Bucket Name]/[Prefix]/v+[s3Version]/[VersionStability]/[The file that you want to download]"
