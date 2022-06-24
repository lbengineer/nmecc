[日本語はこちら](/howto_ja.md)


# Operation explanation

## Introduction
This page explains the basic usage of nMECC.  
Let's take the data rewriting of the public domain NES software "Love Story" as an example.

## Preparation
Download the Famicom emulator "[Nestopia v1.40](http://nestopia.sourceforge.net/)" and the ROM image "[LoveStory](https://pdroms.de/files/nintendo-nintendoentertainmentsystem-nes-famicom-fc/love-story)" of the software.  
Be sure to use v1.40 as the version of Nestopia.

After downloading, start Nestopia, load Love Story and start the game.

## Memory display
First, display the memory of Love Story.
### Process loading
After starting nMECC, press the process selection button on the upper left of the screen to display the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175544748-0dd94e05-f5c4-4207-afb9-7872ebf919c1.png)

If you have Nestopia running, you will find "nestopia" in the list, select it and press the select button.
![無題](https://user-images.githubusercontent.com/47295136/175546073-f482533e-4149-4903-842e-0b02a252b62f.png)

If the information of nestopia is displayed in the Info tab on the left side of the screen, the process has been loaded successfully.
![無題](https://user-images.githubusercontent.com/47295136/175546276-255574f6-da7c-4680-ad1f-d7624d8ac489.png)

### Download virtual region
In order to display the memory of the game, it is necessary to set the virtual region for each emulator.  
The settings of Nestopia v1.40 can be downloaded, so download the settings here.

Select the Region tab on the left side of the screen and press the Virtual Region Settings button at the top to display the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175546471-0aa3b4fe-98f2-45b3-8f8d-bd34db7c85c4.png)

Press the download button at the top right of the dialog to display another dialog.
![無題](https://user-images.githubusercontent.com/47295136/175546721-c7c50d17-b2bc-42f0-93bf-3db59f8df9ce.png)

You need to enter a list of files to download here, but you can download with the default settings.  
Click the Download button at the bottom left, and when the download is complete, press the OK button to close the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175546948-8f0cb630-440f-4057-a528-c87c010d3653.png)

If you can see the Nestopia settings in the list, press the Close button to close the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175547174-b6852754-1f69-49c3-8156-dfe5feb24e3b.png)

### Virtual region selection
If you can download the virtual region settings, the virtual region for Nestopia will appear in the list on the left side of the screen.  
If you select this, the memory of the game will be displayed in the memory editor in the center of the screen.
![無題](https://user-images.githubusercontent.com/47295136/175547495-f91cd281-d1a8-4aed-a5d9-d2b2080d3bcf.png)

If the contents of the memory are displayed in the memory editor, you can rewrite the memory of the game using the memory editor or code.

## Data rewriting
Now, let's rewrite the game data.

### About search
In the explanation here, the data is rewritten using the code already prepared.  
In nMECC, it is possible to rewrite the data by searching the memory value and rewriting the value, but the method is not explained here.

### Code download
The data will be rewritten using the code for LoveStory, so download the code first.

Select the Code tab at the bottom of the screen and press the Edit Code List button on the right to display the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175547690-ca100af4-84d2-42be-b4be-58e8f93bbfb4.png)

Press the download button at the top right of the dialog to display another dialog.
![無題](https://user-images.githubusercontent.com/47295136/175547837-f2376f18-44f9-4a61-bed0-7316c24ce0bd.png)

The download method is the same as the procedure for downloading a virtual region, so the explanation is omitted. After downloading, press the OK button to close the dialog.

If you can see Love Story in the list, press the Close button to close the dialog.
![無題](https://user-images.githubusercontent.com/47295136/175547972-eeeac68f-0296-48c8-bc0d-425848095d15.png)

### Check the code
When the download is complete, select Love Story from the code group list at the bottom of the screen.  
Select a code group to display a list of codes in the code list.
![無題](https://user-images.githubusercontent.com/47295136/175548185-f6368695-6087-43ca-bfc1-a8b91222c837.png)

### Invincible
Now you are ready to rewrite your data.  
Let's try rewriting the data to make it invincible.

Enable Invincible check from the code list and press the Execute button on the top right.
![無題](https://user-images.githubusercontent.com/47295136/175548529-01823c1d-a9cc-48cc-b3bd-e4224906b549.png)

The game character is now invincible without any damage.  
Play Love Story on Nestopia to make sure you don't take any damage from your enemies.
![無題](https://user-images.githubusercontent.com/47295136/175548864-738dc112-4f6a-42ed-a2c7-eb917ea22be4.png)

## Conclusion
Here, we have described the procedure for rewriting data using code.

This time, You downloaded the virtual region setting and the code, but by adding the URL of the download files, you can download the settings and code prepared by other people.  
You can increase the number of compatible emulators by increasing the virtual region settings.  
Also, by increasing the code, you can rewrite the data of various games.
