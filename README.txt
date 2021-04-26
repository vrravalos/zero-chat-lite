== Zero Chat Lite v1.0 ==

By Victor Rafael Ramaldes Avalos

To create a single executable app:
Go to ZeroChatLite project folder (containing ZeroChatLite.csproj) and run the following command (for RELEASE mode and X64 architecture):
dotnet publish -r win-x64 -c Release --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true

The app start at the port 33000, to specify a different port just add the number as parameter, like in the command:
ZeroChatLite.exe 35000

Some code design decisions and patterns:
- Publisher/Subscriber: internally in each client and server
- Router/Dealer: for the socket communication (from ZeroMQ)
- Command pattern (adapted): to create commands for the communication
- Factory method: to create chat messages
- Visitor (adapted): for validation of messages
- Dependency injection in some classes
- Single responsibility 
- DRY
- KISS

Used libraries:
NetMQ: native C# port of the lightweight messaging library ZeroMQ
Utf8Json:  zero allocation JSON Serializer

Ico from:
https://icon-icons.com/icon/Chat-conversation-message/81504

Is there room for improvement, refactoring, other ideas?
Sure! Maybe for the 2.0 version? ;-)




