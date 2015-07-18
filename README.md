# PinnedArrays
C# helper methods to provide functionality for dealing with managed or unmanaged pinned arrays (useful for PInvoke).

*Minimal requirements: .NET Framework 4.5.1*

A dynamic class library that consists of classes and class factories for operating with struct arrays that are not moved in memory by the CLR and thus can be used in native methods (passed as parameters to P/Invoke methods). By utilizing the arrays from this library the overhead of copying the array on each P/Invoke call can be eliminated.

*Pinned arrays of reference types are not supported.*

The library provides ways of creating arrays on both managed CLR heap and unmanaged process heap (the classes **PinnedArrays.ManagedArray** and **PinnedArrays.UnmanagedArray** respectively). The above classes can also be constructed by factory classes **PinnedArrays.ManagedFactory** and **PinnedArrays.UnmanagedFactory**. The main factory class **PinnedArrays.PinnedArrayFactory** can be used to provide universal functionality for creating pinned arrays.

Examples of usage:

> Create unmanaged 'char' array with values 'a', 'b', 'c':

1. PinnedArrayFactory\<UnmanagedFactory\>.CreateArray\<char\>(3); //then populate the array with [] indexer
2. PinnedArrayFactory\<UnmanagedFactory\>.CreateArray('a', 'b', 'c'); //params constructor
3. PinnedArrayFactory\<UnmanagedFactory\>.GetFactory().Create\<char\>(3); //the same as 1.
4. PinnedArrayFactory\<UnmanagedFactory\>.GetFactory().Create('a', 'b', 'c'); //the same as 2.
5. The direct constructor of the 'PinnedArrays.UnmanagedArray' class can also be used.

> To create managed pinned array replace *UnmanagedFactory* with *ManagedFactory* in the examples above.

**PinnedArrays.ManagedFactory** has a method **ManagedArray\<T\> PinArray\<T\>(T[] array)** that can be used to allocate a handle for an existing managed array to prevent it from being moved by the GC.
> var array = PinnedArrayFactory\<ManagedFactory\>.GetFactory().PinArray(new []{'a', 'b', 'c'});

**PinnedArrays.UnmanagedArray** has a method **GetFromPointer\<T\>(IntPtr pointer, int length, bool registerForDispose)** that can be used to create new 'PinnedArrays.UnmanagedArray' object from the pointer to an existing array without allocating new memory.
> var initialArray = PinnedArrayFactory\<UnmanagedFactory\>.CreateArray('a', 'b', 'c');

> var arrayWithTheSamePointer = PinnedArrayFactory\<UnmanagedFactory\>.GetFactory().GetFromPointer\<char\>(initialArray.GetPtr(), initialArray.Length, false);

All the public library entities are provided with C# XML documentation comments.

***Please, inform me of any bugs you happen to encounter via email (shown on my profile page). Any feedback will be highly appreciated.***
