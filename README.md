# buet-share
P2P file sharing app


# Dependencies
.NET 8.0

# How to build
- It can built using dotnet cli. Open any command line app (like powershell) and navigate to the repository
- Run ``dotnet publish``. It will create a portable file to run this app.
- Navigate to publish folder ``cd .\src\Buet.Share.Core\bin\Release\net8.0\publish\``
- Run ``.\Buet.Share.Core.exe -s 127.0.0.1 1337`` for server.
- To run client, open another command line app and run ``.\Buet.Share.Core.exe -c 127.0.0.1 1337``

## Sample Run:
Server:
```
$ ./Buet.Share.Core.exe -s 127.0.0.1 1337
> Client connected from 127.0.0.1. Check client list with list command

> send 0 /another_location/myfile.pdf
>
```

Client:
```
$ ./Buet.Share.Core.exe -c 127.0.0.1 1337
Data of size 305664 recieved! Accept? y/n 
y
Enter path to save location: 
/home/faisal/myfile.pdf
```
