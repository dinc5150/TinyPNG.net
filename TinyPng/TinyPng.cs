using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;
using TinyPngDotNet.Models;

namespace TinyPngDotNet {
    public class TinyPng {
        /// <summary>
        /// You can get an API Key from https://tinypng.com/developers
        /// </summary>
        private string apiKey = null;

        /// <summary>
        /// The URL to the TinyPNG Library
        /// </summary>
        public string ApiUrl = "https://api.tinypng.com/";

        private WebClient TinyPngConnection = new WebClient();

        public ShrinkResponse LastUploadResponse = new ShrinkResponse();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApiKey">You can get an API key from https://tinypng.com/developers</param>
        public TinyPng(string ApiKey) {
            apiKey = ApiKey;

            string _auth = string.Format("{0}:{1}", "api", apiKey);
            string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
            string _cred = string.Format("{0} {1}", "Basic", _enc);
            TinyPngConnection.Headers[HttpRequestHeader.Authorization] = _cred;
        }


        /// <summary>
        /// Uploads the File to Tiny PNG for Compresstion
        /// </summary>
        /// <param name="SourceFilename">Full path and filename to image</param>
        /// <returns></returns>
        public ShrinkResponse Shrink(string SourceFilename) {
            byte[] sourceFileBytes = File.ReadAllBytes(SourceFilename);
            return Shrink(sourceFileBytes);
        }


        /// <summary>
        /// Uploads the File to Tiny PNG for Compresstion
        /// </summary>
        /// <param name="SourceFile">Byte Array of an image file</param>
        /// <returns></returns>
        public ShrinkResponse Shrink(byte[] SourceFile) {
            byte[] response = TinyPngConnection.UploadData(ApiUrl + "shrink", SourceFile);
            string retVal = TinyPngConnection.Encoding.GetString(response);

            LastUploadResponse = Json.Decode<ShrinkResponse>(retVal);

            return LastUploadResponse;
        }

        /// <summary>
        /// Uploads the File to Tiny PNG for Compresstion and then downloads the compressed file and overwites the original file
        /// </summary>
        /// <param name="Filename">Full file path and name to the source and destination file</param>
        /// <returns></returns>
        public ShrinkResponse ShrinkAndDownload(string Filename) {
            ShrinkResponse retVal = Shrink(Filename);
            DownloadShrinkedFile(Filename);
            return retVal;
        }

        /// <summary>
        /// Uploads the File to Tiny PNG for Compresstion and then downloads the compressed file
        /// </summary>
        /// <param name="SourceFilename">Full file path and name to the source File</param>
        /// <param name="OutputFilename">Full file path and name to the the destination file to create / update</param>
        /// <returns></returns>
        public ShrinkResponse ShrinkAndDownload(string SourceFilename, string OutputFilename) {
            ShrinkResponse retVal = Shrink(SourceFilename);
            DownloadShrinkedFile(OutputFilename);
            return retVal;
        }
        /// <summary>
        /// Uploads the File to Tiny PNG for Compresstion and then downloads the compressed file
        /// </summary>
        /// <param name="SourceFile">Byte Array of an image file</param>
        /// <param name="OutputFilename">Full file path and name to the the destination file to create / update</param>
        /// <returns></returns>
        public ShrinkResponse ShrinkAndDownload(byte[] SourceFile, string OutputFilename) {
            ShrinkResponse retVal =  Shrink(SourceFile);
            DownloadShrinkedFile(OutputFilename);
            return retVal;
        }

        /// <summary>
        /// You must succesfully Upload a File before Calling this function
        /// </summary>
        /// <param name="OutputFilename">Full file path and name to the the destination file to create / update</param>
        /// <returns>true if succesfull or false if no file to download</returns>
        public bool DownloadShrinkedFile(string OutputFilename) {
            if (LastUploadResponse.error == null) {
                TinyPngConnection.DownloadFile(TinyPngConnection.ResponseHeaders["Location"], OutputFilename);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Send a compressed file to Amazon S3 storage
        /// </summary>
        /// <param name="data">object containing required params for connecting to S3</param>
        /// <returns>true if succesfull or false if failed</returns>
        public bool UpoadToS3(S3UploadData data)
        {
            if (LastUploadResponse.error == null)
            {
                TinyPngConnection.Headers.Add("Content-Type", "application/json");
                var retVal = TinyPngConnection.UploadString(TinyPngConnection.ResponseHeaders["Location"],
                    JsonConvert.SerializeObject(new {data = data}));

                //@TODO: check status code of request for errors

                return true;
            }
            return false;
        }

    }
}
