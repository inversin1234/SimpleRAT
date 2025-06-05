# SimpleRAT

SimpleRAT is a minimal remote access tool written in C# using Windows Forms. It contains two projects:

- **RAT_Server** – a server application that listens for incoming clients, logs connections and provides a remote desktop view.
- **RAT_Client** – a lightweight client that connects to a configured server and responds to commands such as screenshot requests.

This code base is intended for educational purposes only.

## Features

- Build custom clients with the integrated *Builder* form.
- Configure the listening port via the *Escucha Avanzada* window.
- View connected clients and open a remote desktop window.
- Clients send their PC name when connecting and can transmit screenshots when requested.

## Requirements

- Windows with **.NET Framework 4.7.2** or later.
- Visual Studio (or MSBuild) to compile the solution.

## Building

1. Open `RemoteAccessTool.sln` in Visual Studio.
2. Build the `RAT_Server` project.
3. Use the server's Builder form to generate a client executable specifying the server IP and port.

## Usage

1. Run **RAT_Server.exe** and choose a port to listen on (port `5000` by default).
2. Click **Builder** to create a client with the desired connection settings.
3. Execute the generated client on another machine. When it connects, its name appears in the client list.
4. Right‑click a client entry and select **Ver Escritorio Remoto** to view its screen.

## Directory Structure

```
RAT_Server   - Source code for the server (Windows Forms)
RAT_Client   - Basic client implementation
RemoteAccessTool.sln - Visual Studio solution file
```

## License

This project is released under the [MIT License](LICENSE).
