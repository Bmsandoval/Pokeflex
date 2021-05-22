Create new unit tests:

# Not sure why you'd have multiple test projects, but you can
$ dotnet new xunit -o Tests
$ dotnet add ./Tests/PokeflexTests.csproj reference ./App/Pokeflex.csproj






# Create ram disk 512mb ramdisk (512*2048=1048576)
$ diskutil erasevolume HFS+ 'RAM Disk' `hdiutil attach -nomount ram://1048576`
```
Started erase on disk2
Unmounting disk
Erasing
Initialized /dev/rdisk2 as a 512 MB case-insensitive HFS Plus volume
Mounting disk
Finished erase on disk2 (RAM Disk)
```
# symlink bin folder to the ramdisk
$ mkdir -p /Volumes/RAM\ Disk/Pokeflex/bin
$ rm -rf bin
$ ln -s /Volumes/RAM\ Disk/Pokeflex/bin /Users/bryansandoval/projects/C#/Pokeflex/App





# Unit Test Current Time Estimates
* [docker] baseline (runs no tests) ~2:36
* [docker] running all tests once ~2:47
* [native] running all tests once ~9s



# Dev Env Setup
Note these steps will not allow you to run as sudo unless you target the right dotnet binary

- Create the install path \
```
$ mkdir -p ~/dotnet
```

- Download dotnet 5. The following is for Arm32 (raspberry pi) \
```
$ wget https://download.visualstudio.microsoft.com/download/pr/55547694-fe7e-43f3-bf58-33ef9bb7ee85/5d8b57df472b96e6f38988041751ba2e/dotnet-sdk-5.0.203-linux-arm.tar.gz \
-O dotnet-sdk-arm.tar.gz
```

- Unpack the download
```
$ sudo tar zxf dotnet-sdk-arm.tar.gz -C $HOME/dotnet
```

- Remove the original download
```
$ rm dotnet-sdk-arm.tar.gz
```

- Link the download in your .bashrc
```
$ echo "
export DOTNET_ROOT=$HOME/dotnet
export PATH=\$PATH:$HOME/dotnet" \
>> ~/.bashrc
```

- And update your environment
```
$ source ~/.bashrc
```