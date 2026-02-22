using System;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;

namespace MongoInit.Data;

public static class ObjectIdGenerator
{
    public static ObjectId GenerateId(string seed, long timeUnixTimeSeconds)
    {
        var objectIdBytes = new byte[12];

        // 1. Get current Unix Timestamp (4 bytes)
        var timeBytes = BitConverter.GetBytes((uint)timeUnixTimeSeconds);

        // ObjectId expects Big-Endian for the timestamp
        if (BitConverter.IsLittleEndian) Array.Reverse(timeBytes);

        // Copy timestamp to first 4 bytes
        Array.Copy(timeBytes, 0, objectIdBytes, 0, 4);

        // 2. Fill the remaining 8 bytes with a hash of your seed
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        Array.Copy(hash, 0, objectIdBytes, 4, 8);

        return new ObjectId(objectIdBytes);
    }
}
