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