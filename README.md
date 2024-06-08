# Dynamo

![Dynamo Logo](../Assets/Images/Dynamo.gif)

## Overview

Dynamo is a cross-platform application for streaming anime from external sources like Gogoanime and Animepahe. It uses media players like MPV and VLC to provide a seamless viewing experience.

## Features

- Stream anime from various sources
- Support for multiple media players (MPV, VLC)
- Cross-platform: Windows, macOS, Linux
- User-friendly interface
- Regular updates and improvements

## Screenshots

![Dynamo Screenshot](../Assets/Images/ScreenShots/Screenshot%20(2).png)
![Dynamo Screenshot](../Assets/Images/ScreenShots/Screenshot%20(3).png)

## Installation

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (not needed for .exe users)
- [Git](https://git-scm.com/)
- [MPV](https://mpv.io/installation/) or [VLC](https://www.videolan.org/vlc/)

### Steps

#### Using Pre-built Executable (Recommended for Users)

1. Download the latest `.exe` file from the [releases page](https://github.com/Eugenenoble2005/Dynamo/releases).
2. Run the executable and follow the on-screen instructions.

#### Building from Source (For Developers)

1. Clone the repository:
   ```
   git clone https://github.com/Eugenenoble2005/Dynamo.git
   cd Dynamo
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```
   
3. Build the project:
   ```
   dotnet build
   ```

4. Run the application:
   ```
   dotnet run --project path/to/Dynamo.csproj
   ```
### Usage
1. Open the application.
2. Select a source (e.g., Gogoanime, Animepahe).
3. Browse or search for your favorite anime.
4. Choose a media player (MPV or VLC) and start streaming.

### Contributing
We welcome contributions from the community! Here’s how you can get involved:

1. Fork the repository.
2. Create a new branch (git checkout -b feature-branch).
3. Make your changes.
4. Commit your changes (git commit -m 'Add some feature').
5. Push to the branch (git push origin feature-branch).
6. Open a pull request.

