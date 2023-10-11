# DocConnect API - How to run

## Build
```bash
 - 'dotnet build -c Release'

 - 'dotnet publish -c Release --no-build -o:$PUBLISH_FOLDER'
```

# DocConnect API - How to run locally

## Prerequisites

Before getting started, you need to ensure you have the .NET SDK 7.0 installed on your machine. If you don't have it already, you can download and install it from the official website: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

## Clone the Project

Open your terminal or command prompt and run the following command to clone the project:
```bash
  git clone https://github.com/NedkoMD/DocConnect-API
```

## Go to the main branch

Navigate to the cloned repository's directory:
```bash
  cd back-end
```

## Checkout development branch

Switch to the development branch using the following command:
```bash
  git checkout dev
```

## Go to the project directory

Now, move to the DocConnect API project directory:
```bash
  cd DocConnect
```

## Build the app

Build the project using the dotnet build command:
```bash
  dotnet build
```

## Install dependencies

Restore the project dependencies using the following command:
```bash
  dotnet restore
```

## Start the app

Finally, start the DocConnect API application:
```bash
  dotnet run --project DocConnect.Presentation.API   
```
