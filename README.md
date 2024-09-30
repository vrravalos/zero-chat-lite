# Zero Chat Lite v1.0 💬

By Victor Rafael Ramaldes Avalos

## Building the Application

To create a single executable application:

1. Go to the ZeroChatLite project folder (which contains ZeroChatLite.csproj)
2. Run the following command (for RELEASE mode and X64 architecture):

```
dotnet publish -r win-x64 -c Release --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true
```

## Running the Application

The application will start on port 33000 by default. To specify a different port, add the port number as a parameter:

```
ZeroChatLite.exe 35000
```

## Code Design Choices and Patterns

- Publisher/Subscriber: internally in every client and server
- Router/Dealer: for socket communication (from ZeroMQ)
- Command pattern (adapted): to create commands for communication
- Factory method: for creating chat messages
- Visitor (adapted): for message validation
- Dependency injection into some classes
- Single responsibility 
- DRY (Don't Repeat Yourself)
- KISS (Keep It Simple, Stupid)

## Libraries Used

- NetMQ: native C# port of lightweight messaging library ZeroMQ
- Utf8Json: zero mapping JSON serializer

## Icon Source

Chat icon from: [icon-icons.com](https://icon-icons.com/icon/Chat-conversation-message/81504)

## Future Improvements

Any room for improvement, refactoring, other ideas?  
Sure there is! Maybe for the 2.0 release? ;-)
