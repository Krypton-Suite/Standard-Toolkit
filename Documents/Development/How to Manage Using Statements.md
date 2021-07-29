# Krypton Developer Documentation - How to Manage Using Statements

As part of C# 10 & .NET 6, it is now possible to contain all `using` statements in one central file. The benefits to doing this is that it frees up system resources, and avoids a spider web of `using` declarations.

As a result, **all** projects contain a `GlobalDeclarations` file in a directory named ***globals***. If you need to add a new `using` statement, please add it to the aforementioned file, while removing the unneeded statements from the source code file. 