# PiPhotoBooth

![Image of the app]('Resources/app.png' "PiPhotoBooth")

This is a simple Photo Booth Application written in C# with 
[Avalonia UI](https://github.com/AvaloniaUI/Avalonia) using the 
[GPhoto2](http://www.gphoto.org/) library.

## Getting started

### Prerequistes
[GPhoto2](http://www.gphoto.org/) and 
[.Net 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 
must be installed on the System.

Set the correct Path on the **PhotoDirectory** Settings in the appesettings.json.
All Photos will be stored at this path.

### Run the app

```
cd PiPhotoBooth.Presentation
dotnet run
```