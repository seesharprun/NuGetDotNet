## Setup

### Get the Code

For this project, you'll download code and complete tasks on your local computer.

- **GitHub**: [seesharprun/NuGetDotNet](https://github.com/seesharprun/NuGetDotNet)

### Verify Local Environment

Let's make sure your environment is ready before we continue with this project. You will use the following commands in your console at the root folder of the project.

#### Verify .NET SDK

Run the following command to verify that a version of .NET greater than 3.1 is installed.

```
dotnet --version
```

This command should return the version number for your current .NET installation.

#### Verify Project

Run the following command to build your project for the first time.

```
dotnet build
```

This command may take a little while as it will need to install packages from NuGet and then perform your initial build.

#### Viewing your work

At any time, you can run the following command to run your project.

```
dotnet run
```

Use this command as you make changes throughout the project.

## Installing packages from NuGet

The ``dotnet add package <package-name>`` command is used to add packages to your .NET console application. For this project, we will add the [Colorful.Console](https://www.nuget.org/packages/Colorful.Console/) package from NuGet.

[//]: # (task_id: @nuget)
### Add Colorful.Console Package from NuGet

Import the **Colorful.Console** package from NuGet into your .NET project.

## Aliasing the classes in the NuGet library

The ``using <alias> = <Namespace>.<static-class>;`` syntax is used to create an alias to a static class in your project. You can read more about this special syntax on [Microsoft Docs](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/using-directive).

The **Colorful.Console** library ships with a special [Console](http://colorfulconsole.com/) static class. Instead of referencing this special class by always typing out ``Colorful.Console.<method-name>``, we would like to shorten this to ``Console.<method-name>``.

> ❗ Heads-up: All of the remaining work for this project will be done in the **Program.cs** file. Don't forget to use the ``dotnet run`` command to test your application at each step.

[//]: # (task_id: @li)
### Add a using block with an alias

Add a new using block to the file with the **Program** class that uses an alias.

[//]: # (task_id: @li)
### Change the using block to use an alias for "Console"

Update your using block to use the identifier ``Console`` as its alias.

[//]: # (task_id: @li)
### Change the using block to alias Colorful.Console

Update the using block to alias the ``Colorful.Console`` static class.

## Using the NuGet library

Now that you have imported and referenced the **Colorful.Console** NuGet package, it is time to use it in your project. You will now update the application's code to use the special ASCII art-printing features of the library.

[//]: # (task_id: @li)
### Use the Console.WriteAscii method 

Replace the ``Console.WriteLine`` method with ``Console.WriteAscii``.

[//]: # (task_id: @li)
### Update the greeting to say "Hello, Pluralsight!"

Update the greeting from ``"Hello World"`` to ``"Hello, Pluralsight!"``