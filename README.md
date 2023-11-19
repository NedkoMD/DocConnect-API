# DocConnect API - overview

DocConnect offers a seamless user experience with hassle-free registration and login functionalities for both users and specialists. Users enjoy comprehensive profile management features, including the ability to edit profiles, customize notifications, cancel or schedule appointments, and toggle multi-factor authentication for added security.

Effortlessly find the right healthcare specialist through our intuitive search feature, allowing users to filter based on specialty, country, and name. Say goodbye to traditional appointment scheduling with our online booking system, eliminating the need for time-consuming phone calls.

Stay in the loop with instant notifications and appointment reminders, ensuring that crucial medical appointments are never overlooked. DocConnect prioritizes the security and privacy of your medical information, implementing robust measures in alignment with HIPAA regulations.

Administrators wield powerful tools to manage the application efficiently. Grant specialist access, and easily add, delete, or update information such as countries, cities, specialties, and more. DocConnect is not just an application; it's a comprehensive solution designed to enhance the entire healthcare journey for users and specialists alike.

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
