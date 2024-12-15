## Prerequisites

- SDK [.NET 8](https://dotnet.microsoft.com/es-es/download/dotnet/8.0).
- Port 5000 Available
- git [2.33.0](https://git-scm.com/downloads) or higher.
- [Docker](https://www.docker.com/) **Note**: If you do not have installed is highly recommended to see the steps to correctly install docker on your machine with [Linux](https://docs.docker.com/desktop/install/linux-install/), [Windows](https://docs.docker.com/desktop/install/windows-install/) or [MacOs](https://docs.docker.com/desktop/install/mac-install/).
- [Cubi12 Legacy](https://github.com/Dizkm8/cubi12-api) **Note**: If you have already installed this project, it is not necessary to install it again.

## Getting Started

Follow these steps to get the project up and running on your local machine:

1. Install [Cubi12 Legacy](https://github.com/Dizkm8/cubi12-api).    

2. Clone the repository to your local machine.
     ```bash
     git clone https://github.com/Dariusss12/api-gateway
     ```

3. Navigate to the root folder.
     ```bash
     cd api-gateway
     ```
       
4. Install project dependencies using dotnet sdk.
     ```bash
     dotnet restore
     ```
5. Run project.
      ```bash
      dotnet run
      ```

This will start the development server, and you can access the app in your web browser by visiting http://localhost:5001.

## Use

You can use [Postman](https://www.postman.com/) to use the API.
