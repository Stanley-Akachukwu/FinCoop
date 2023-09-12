﻿using NUlid;
using System.Security.Cryptography;

namespace AP.ChevronCoop.Commons
{
    public static partial class SequentialGuid
    {
        #region Static Fields
        /// <summary>
        /// Provides cryptographically strong random data for GUID creation.
        /// https://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database
        /// </summary>
        //private static readonly RNGCryptoServiceProvider randomGenerator = new RNGCryptoServiceProvider();
        private static readonly RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();
        #endregion

        /// <summary>
        /// Returns a new GUID value which is sequentially ordered when formatted as
        /// a string, a byte array, or ordered by the least significant six bytes of the
        /// Data4 block, as specified by <paramref name="guidType" />.
        /// </summary>
        /// <param name="guidType">
        /// Specifies the type of sequential GUID (i.e. whether sequential as a string,
        /// as a byte array, or according to the Data4 block.  This can affect
        /// performance under various database types; see below.
        /// </param>
        /// <returns>
        /// A <see cref="Guid" /> structure whose value is created by replacing
        /// certain randomly-generated bytes with a sequential timestamp.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method creates a new GUID value which combines a random component
        /// with the current timestamp, also known as a COMB.  The general concept
        /// is outlined in Jimmy Nilsson's article "The Cost of GUIDs as Primary Keys",
        /// and involves replacing either the least significant or most significant
        /// six bytes of the GUID with the current timestamp.  This reduces the
        /// random component of the GUID from 16 bytes to 10 bytes, but this is
        /// still sufficient to prevent a collision under most real-world circumstances.
        /// </para>
        /// <para>
        /// The purpose of sequential GUIDs is not to promote the use of GUIDs as
        /// sortable entities.  In fact, GUIDs generated very close together may
        /// have the same timestamp and are not guaranteed to be sequentially ordered
        /// at all.  The intent is to increase performance when doing repeated
        /// inserts into database tables that have a clustered index on a GUID
        /// column, so that later entries do not have to be inserted into the middle
        /// of the table, but can simply be appended to the end.
        /// </para>
        /// <para>
        /// According to experiments, Microsoft SQL Server sorts GUID values using
        /// the least significant six bytes of the Data4 block; therefore, GUIDs being
        /// generated for use with SQL Server should pass a <paramref name="guidType" /> 
        /// value of <c>SequentialAtEnd</c>.  GUIDs generated for most other database
        /// types should be passed a <paramref name="guidType" /> value of
        /// <c>SequentialAsString</c> or <c>SequentialAsByteArray</c>.
        /// </para>
        /// <para>
        /// Various standards already define a time-based UUID; however, the
        /// format specified by these standards splits the timestamp into
        /// several components, limiting its usefulness as a sequential ID.
        /// Additionally, the format used for such UUIDs is not compatible
        /// with the GUID ordering on Microsoft SQL Server.
        /// </para>
        /// </remarks>
        public static Guid Create(SequentialGuidType guidType)
        {
            // We start with 16 bytes of cryptographically strong random data.
            byte[] randomBytes = new byte[10];
            //randomGenerator.GetBytes(randomBytes);
            randomGenerator.GetBytes(randomBytes);

            // An alternate method: use a normally-created GUID to get our initial
            // random data:
            // byte[] randomBytes = Guid.NewGuid().ToByteArray();
            // This is faster than using RNGCryptoServiceProvider, but I don't
            // recommend it because the .NET Framework makes no guarantee of the
            // randomness of GUID data, and future versions (or different
            // implementations like Mono) might use a different method.

            // Now we have the random basis for our GUID.  Next, we need to
            // create the six-byte block which will be our timestamp.

            // We start with the number of milliseconds that have elapsed since
            // DateTime.MinValue.  This will form the timestamp.  There's no use
            // being more specific than milliseconds, since DateTime.Now has
            // limited resolution.

            // Using millisecond resolution for our 48-bit timestamp gives us
            // about 5900 years before the timestamp overflows and cycles.
            // Hopefully this should be sufficient for most purposes. :)
            long timestamp = DateTime.UtcNow.Ticks / 10000L;

            // Then get the bytes
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            // Since we're converting from an Int64, we have to reverse on
            // little-endian systems.
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:

                    // For string and byte-array version, we copy the timestamp first, followed
                    // by the random data.
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to compensate for the fact
                    // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                    // respectively.  That means that it switches the order on little-endian
                    // systems.  So again, we have to reverse.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:

                    // For sequential-at-the-end versions, we copy the random data first,
                    // followed by the timestamp.
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;


                case SequentialGuidType.SequentialAsUlid:
                    var ulid = Ulid.NewUlid();
                    guidBytes = ulid.ToByteArray();

                    break;

                case SequentialGuidType.Timestamp:
                    byte[] guidArray = Guid.NewGuid().ToByteArray();

                    DateTime baseDate = new DateTime(1900, 1, 1);
                    DateTime now = DateTime.UtcNow;

                    // Get the days and milliseconds which will be used to build the byte string 
                    TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
                    TimeSpan msecs = now.TimeOfDay;

                    // Convert to a byte array 
                    // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
                    byte[] daysArray = BitConverter.GetBytes(days.Days);
                    byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

                    // Reverse the bytes to match SQL Servers ordering 
                    Array.Reverse(daysArray);
                    Array.Reverse(msecsArray);

                    // Copy the bytes into the guid 
                    Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
                    Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

                    guidBytes = guidArray;

                    break;
            }

            return new Guid(guidBytes);
        }



    }
}