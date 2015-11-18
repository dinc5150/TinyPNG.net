  _____ _             ____  _   _  ____                  _   
 |_   _(_)_ __  _   _|  _ \| \ | |/ ___|      _ __   ___| |_ 
   | | | | '_ \| | | | |_) |  \| | |  _      | '_ \ / _ \ __|
   | | | | | | | |_| |  __/| |\  | |_| |  _  | | | |  __/ |_ 
   |_| |_|_| |_|\__, |_|   |_| \_|\____| (_) |_| |_|\___|\__|
                |___/                                        

A 3rd Party .NET Library for Shrinking Images via the TinyPNG API by digitalmomentum.com.au

Installation
==========================================================

Download the latest Binary and place it in your /bin folder

Tiny Png API
==========================================================

You will need at least a free API key from Tiny Png which can be created at https://tinypng.com/developers

Usage
==========================================================

The following code shows how to shrink an API

----------------------------------------------------------------------------------------

TinyPngDotNet.TinyPng myPng = new TinyPngDotNet.TinyPng("YOUR_TINY_PNG_API_KEY");

var response = myPng.Shrink("c:\temp\Original.png");

int OutputSize = response.output.size;
string DownloadUrl = response.output.url;
double Ratio = response.output.ratio;

myPng.DownloadShrinkedFile("c:\temp\Compressed.png"));
----------------------------------------------------------------------------------------

API Functions not yet Added
==========================================================

This Library only has the basic function generating a compressed image from Tiny PNG. There are other API functions that have not yet been added. Please feel free to help me extend this library and make it better!

Functions not yet supported:
 - Resizing
 - Uploading to Amazon S3