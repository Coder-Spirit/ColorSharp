ColorSharp's Contributing Guidelines
====================================

Contributing with Code
----------------------

In first place, if you want to contribute code to ColorSharp, you are going to be able to compile the project.

### Prerequisites to compile ColorSharp

#### MS Windows

* Microsoft .NET >= 4.0 or Mono >= 3.2 .
* MS Visual Studio >= 2013, or MonoDevelop >= 5.5 .

#### Linux

It's prefered to use the last version of Mono (currently 3.12). To install it on your system follow the instructions of this page:

 * http://www.mono-project.com/docs/getting-started/install/linux/

In Ubuntu you have to install the monodevelop package after following the previous instructions:

```bash
apt-get install monodevelop
```

### Some comments about NuGet package generation

Currently, [Litipk](https://github.com/Litipk) owns the private key to sign the NuGet packages sent to nuget.org, but you can generate your own DLLs using defered signature (which is, in fact, the default project's setting).

### Testing

It's desirable that every new feature arrives with it's own unit tests. At this point, the unit testing coverage on ColorSharp is pretty low, but we're working on it, and new code without tests will harm this effort.

Contributing with Documentation
-------------------------------

This has to be discussed, because there isn't yet a high volumen of documentation and there are many possibilities. Feel free to suggest us any workpath that you think it's convenient.

Contributing with Bug Reports
-----------------------------

A very important part of every software project is the bugs tracking and solving process. You can send us bug reports through the [tracker](https://github.com/Litipk/ColorSharp/issues).

It's desirable that every bug report explains the bug in a way that the developers can easily reproduce it. The following points are very helpful in order to achieve reproducibility:
* Example code.
* ColorSharp's version.
* Chosen Runtime: It's MS .NET? or it's Mono?
* .NET version (are you using .NET 4.0, 4.5, 4.5.1..?). If you are using Mono it's important to note that a given Mono version can run as different .NET versions.

Contributing with New Ideas
---------------------------

You can use the github [tracker](https://github.com/Litipk/ColorSharp/issues) to send us new ideas, even if those ideas aren't bug reports. In such case, adding something like "SUGGESTION: ...", "IMPROVEMENT: ..." to the report bug it's a good idea.

You can also discuss your ideas with us in our [Gitter chat room](https://gitter.im/Litipk/ColorSharp).
