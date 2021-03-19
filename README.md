[![NuGet](https://img.shields.io/nuget/v/ChoiByteBuffer)](https://www.nuget.org/packages/ChoiByteBuffer/)
[![NuGet](https://img.shields.io/nuget/dt/ChoiByteBuffer)](https://www.nuget.org/packages/ChoiByteBuffer/)

ChoiByteBuffer
================

A buffer class that stores byte-type data elements for the .NET platform.

## Usage ##

### Generating binary file using file streams ###

```c#
string dataFile = Directory.GetCurrentDirectory() + @"\..\..\..\sample.data";

// Reading binary file using file stream
using (var stream = new FileStream(dataFile, FileMode.Open, FileAccess.Read, FileShare.None))
using (var buffer = new StreamByteBuffer(stream))
{
    // get string type
    var data1 = buffer.Get<string>(10);
    // get int type
    var data2 = buffer.Get<int>();
    // get long type
    var data3 = buffer.Get<long>();
    // get float type
    var data4 = buffer.Get<float>();
    // get bytes
    var data5 = buffer.Get(2);
    // get single byte
    var data6 = buffer.Get();
    // get bytes
    var data7 = buffer.Get(2);
}
```

### Reading binary file using file stream ###

```c#
string dataFile = Directory.GetCurrentDirectory() + @"\..\..\..\sample.data";

// Generating binary file using file streams
using (var stream = new FileStream(dataFile, FileMode.Create, FileAccess.Write, FileShare.None))
using (var buffer = new StreamByteBuffer(stream))
{
    // input string type data
    buffer.Put("string data", 10)
        // input int type
        .Put(1)
        // input long type
        .Put(1L)
        // input float type
        .Put(1F)
        // input null bytes
        .PutNull(2)
        // input single byte
        .Put((byte)0x00)
        // input bytes
        .Put(new byte[] { 0x00, 0x01 });
}
```

### Generating binary file ###

```c#
string dataFile = Directory.GetCurrentDirectory() + @"\..\..\..\sample.data";

// Generating binary file
using (var buffer = new InMemoryByteBuffer())
{
    // input string type data
    buffer.Put("string data", 10)
        // input int type
        .Put(1)
        // input long type
        .Put(1L)
        // input float type
        .Put(1F)
        // input null bytes
        .PutNull(2)
        // input single byte
        .Put((byte)0x00)
        // input bytes
        .Put(new byte[] { 0x00, 0x01 });

    File.WriteAllBytes(dataFile, buffer.ToArray());
}
```

### Reading binary files ###

```c#
string dataFile = Directory.GetCurrentDirectory() + @"\..\..\..\sample.data";

// Reading binary files
using (var buffer = new InMemoryByteBuffer(File.ReadAllBytes(dataFile)))
{
    // get string type
    var data1 = buffer.Get<string>(10);
    // get int type
    var data2 = buffer.Get<int>();
    // get long type
    var data3 = buffer.Get<long>();
    // get float type
    var data4 = buffer.Get<float>();
    // get bytes
    var data5 = buffer.Get(2);
    // get single byte
    var data6 = buffer.Get();
    // get bytes
    var data7 = buffer.Get(2);
}
```
