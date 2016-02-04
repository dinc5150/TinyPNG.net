namespace TinyPngDotNet.Models
{
    public class S3UploadData
    {
        public string service { get; set; }
        public string aws_access_key_id { get; set; }
        public string aws_secret_access_key { get; set; }
        public string region { get; set; }
        public string path { get; set; }
    }
}
