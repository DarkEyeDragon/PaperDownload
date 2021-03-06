# PaperDownload

[![AppVeyor](https://img.shields.io/appveyor/ci/darkeyedragon/paperdownload.svg)](https://travis-ci.com/darkeyedragon/paperdownload)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/darkeyedragon/paperdownload.svg)](https://ci.appveyor.com/project/DarkEyeDragon/paperdownload/build/tests)

Download Minecraft Paper servers with ease.
This is a stand alone project and is not related to [PaperMC](https://papermc.io/) in any way.

#### What is PaperDownload
PaperDownload is a lightweight Windows application that lets you download any [PaperMC](https://papermc.io/) project.
This includes [Paper](https://github.com/PaperMC/Paper), [Waterfall](https://github.com/PaperMC/Waterfall) and [Travertine](https://github.com/PaperMC/Travertine).

#### Does this work with spigot?
No. Spigot has no api for download managing so it would make things a bit harder.
Paper is compatible with Spigot. So your plugins will run fine on it. There really isn't any downside of using Paper.

#### How to use
1. Launch the application.
2. Click the 'browse...' button and select your minecraft server directory.
3. Select your Project, MC version and optionally your build. If no build is provided it will download the latest one.
4. Press the download button and grab a cookie while it completes.*

**NOTE:** This **WILL** override your jar files. So if you download the latest it will override the previous latest. Given it has the same name.
So don't download this while your server is running.

#### The format is the following: 
`Project-MinecraftVersion_build` Example: `Paper-1.13.2_358`
If no build version is set it will replace `build` with `latest`.

#### How does it work
This tool uses Paper's [Downloads API](https://paper.readthedocs.io/en/stable/site/api.html). It processes all versions from there.


#### Can I contribute
Yes you can. Feel free to fork and make PR's!

# Download 
You can download the latest version from the [releases](https://github.com/DarkEyeDragon/PaperDownload/releases) page.

#### Screenshots
![screenshot 1](https://github.com/DarkEyeDragon/PaperDownload/raw/master/Screenshots/PaperDownloader_1.png "Screenshot 1")
![screenshot 2](https://github.com/DarkEyeDragon/PaperDownload/raw/master/Screenshots/PaperDownloader_2.png "Screenshot 2")
![screenshot 3](https://github.com/DarkEyeDragon/PaperDownload/raw/master/Screenshots/PaperDownloader_3.png "Screenshot 3")
![screenshot 4](https://github.com/DarkEyeDragon/PaperDownload/raw/master/Screenshots/PaperDownloader_4.png "Screenshot 4")
