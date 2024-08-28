# PiPhotoBooth

![Image of the app](Resources/app.png "PiPhotoBooth")

This is a simple Photo Booth Application written in C# with 
[Avalonia UI](https://github.com/AvaloniaUI/Avalonia) using the 
[GPhoto2](http://www.gphoto.org/) library.

## Getting started

### Prerequistes
[.Net 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 
must be installed on the System.

To use a connected camery, [GPhoto2](http://www.gphoto.org/) must be
installed on the system. 

### Run the app

```
cd PiPhotoBooth.Presentation
dotnet run
```

### Deploy the app

For Linux/Raspberry Pi

```
dotnet publish -c Release -r linux-arm --self-contained -o ./publish
``` 

### Use the app

On the first star the Settings Window will open automatically.
You have to set the Directory in which the photos will be stored.  
It is possible to select a Faker Service, when no Camery is connected or 
GPhoto is not installed on the System. The Faker will Download random
images from [LoremPicsum](https://picsum.photos/) when a photo is taken.

