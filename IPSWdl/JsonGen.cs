#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace Utf8Json.Resolvers
{
    using System;
    using Utf8Json;

    public class GeneratedResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(6)
            {
                {typeof(global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Firmware>), 0 },
                {typeof(global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Device>), 1 },
                {typeof(global::IPSWdl.JsonReps.Device), 2 },
                {typeof(global::IPSWdl.JsonReps.Firmware), 3 },
                {typeof(global::IPSWdl.JsonReps.FirmwareListing), 4 },
                {typeof(global::IPSWdl.JsonReps.jank), 5 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::Utf8Json.Formatters.ListFormatter<global::IPSWdl.JsonReps.Firmware>();
                case 1: return new global::Utf8Json.Formatters.ListFormatter<global::IPSWdl.JsonReps.Device>();
                case 2: return new Utf8Json.Formatters.IPSWdl.JsonReps_DeviceFormatter();
                case 3: return new Utf8Json.Formatters.IPSWdl.JsonReps_FirmwareFormatter();
                case 4: return new Utf8Json.Formatters.IPSWdl.JsonReps_FirmwareListingFormatter();
                case 5: return new Utf8Json.Formatters.IPSWdl.JsonReps_jankFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Utf8Json.Formatters.IPSWdl
{
    using System;
    using Utf8Json;


    public sealed class JsonReps_DeviceFormatter : global::Utf8Json.IJsonFormatter<global::IPSWdl.JsonReps.Device>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public JsonReps_DeviceFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("identifier"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("platform"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("cpid"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("bdid"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("identifier"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("platform"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("cpid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("bdid"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::IPSWdl.JsonReps.Device value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.identifier);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.platform);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt32(value.cpid);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt32(value.bdid);
            
            writer.WriteEndObject();
        }

        public global::IPSWdl.JsonReps.Device Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __name__ = default(string);
            var __name__b__ = false;
            var __identifier__ = default(string);
            var __identifier__b__ = false;
            var __platform__ = default(string);
            var __platform__b__ = false;
            var __cpid__ = default(int);
            var __cpid__b__ = false;
            var __bdid__ = default(int);
            var __bdid__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    case 1:
                        __identifier__ = reader.ReadString();
                        __identifier__b__ = true;
                        break;
                    case 2:
                        __platform__ = reader.ReadString();
                        __platform__b__ = true;
                        break;
                    case 3:
                        __cpid__ = reader.ReadInt32();
                        __cpid__b__ = true;
                        break;
                    case 4:
                        __bdid__ = reader.ReadInt32();
                        __bdid__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::IPSWdl.JsonReps.Device();
            if(__name__b__) ____result.name = __name__;
            if(__identifier__b__) ____result.identifier = __identifier__;
            if(__platform__b__) ____result.platform = __platform__;
            if(__cpid__b__) ____result.cpid = __cpid__;
            if(__bdid__b__) ____result.bdid = __bdid__;

            return ____result;
        }
    }


    public sealed class JsonReps_FirmwareFormatter : global::Utf8Json.IJsonFormatter<global::IPSWdl.JsonReps.Firmware>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public JsonReps_FirmwareFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("identifier"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("version"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("buildid"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sha1sum"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("md5sum"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("filesize"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("url"), 6},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("uploaddate"), 7},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("identifier"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("version"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("buildid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("sha1sum"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("md5sum"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("filesize"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("url"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("uploaddate"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::IPSWdl.JsonReps.Firmware value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.identifier);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.version);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.buildid);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteString(value.sha1sum);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteString(value.md5sum);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteInt64(value.filesize);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteString(value.url);
            writer.WriteRaw(this.____stringByteKeys[7]);
            formatterResolver.GetFormatterWithVerify<global::System.DateTime>().Serialize(ref writer, value.uploaddate, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::IPSWdl.JsonReps.Firmware Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __identifier__ = default(string);
            var __identifier__b__ = false;
            var __version__ = default(string);
            var __version__b__ = false;
            var __buildid__ = default(string);
            var __buildid__b__ = false;
            var __sha1sum__ = default(string);
            var __sha1sum__b__ = false;
            var __md5sum__ = default(string);
            var __md5sum__b__ = false;
            var __filesize__ = default(long);
            var __filesize__b__ = false;
            var __url__ = default(string);
            var __url__b__ = false;
            var __uploaddate__ = default(global::System.DateTime);
            var __uploaddate__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __identifier__ = reader.ReadString();
                        __identifier__b__ = true;
                        break;
                    case 1:
                        __version__ = reader.ReadString();
                        __version__b__ = true;
                        break;
                    case 2:
                        __buildid__ = reader.ReadString();
                        __buildid__b__ = true;
                        break;
                    case 3:
                        __sha1sum__ = reader.ReadString();
                        __sha1sum__b__ = true;
                        break;
                    case 4:
                        __md5sum__ = reader.ReadString();
                        __md5sum__b__ = true;
                        break;
                    case 5:
                        __filesize__ = reader.ReadInt64();
                        __filesize__b__ = true;
                        break;
                    case 6:
                        __url__ = reader.ReadString();
                        __url__b__ = true;
                        break;
                    case 7:
                        __uploaddate__ = formatterResolver.GetFormatterWithVerify<global::System.DateTime>().Deserialize(ref reader, formatterResolver);
                        __uploaddate__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::IPSWdl.JsonReps.Firmware();
            if(__identifier__b__) ____result.identifier = __identifier__;
            if(__version__b__) ____result.version = __version__;
            if(__buildid__b__) ____result.buildid = __buildid__;
            if(__sha1sum__b__) ____result.sha1sum = __sha1sum__;
            if(__md5sum__b__) ____result.md5sum = __md5sum__;
            if(__filesize__b__) ____result.filesize = __filesize__;
            if(__url__b__) ____result.url = __url__;
            if(__uploaddate__b__) ____result.uploaddate = __uploaddate__;

            return ____result;
        }
    }


    public sealed class JsonReps_FirmwareListingFormatter : global::Utf8Json.IJsonFormatter<global::IPSWdl.JsonReps.FirmwareListing>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public JsonReps_FirmwareListingFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("identifier"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("boardconfig"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("platform"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("cpid"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("bdid"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("firmwares"), 6},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("identifier"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("boardconfig"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("platform"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("cpid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("bdid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("firmwares"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::IPSWdl.JsonReps.FirmwareListing value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.identifier);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.boardconfig);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteString(value.platform);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt32(value.cpid);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteInt32(value.bdid);
            writer.WriteRaw(this.____stringByteKeys[6]);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Firmware>>().Serialize(ref writer, value.firmwares, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::IPSWdl.JsonReps.FirmwareListing Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __name__ = default(string);
            var __name__b__ = false;
            var __identifier__ = default(string);
            var __identifier__b__ = false;
            var __boardconfig__ = default(string);
            var __boardconfig__b__ = false;
            var __platform__ = default(string);
            var __platform__b__ = false;
            var __cpid__ = default(int);
            var __cpid__b__ = false;
            var __bdid__ = default(int);
            var __bdid__b__ = false;
            var __firmwares__ = default(global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Firmware>);
            var __firmwares__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    case 1:
                        __identifier__ = reader.ReadString();
                        __identifier__b__ = true;
                        break;
                    case 2:
                        __boardconfig__ = reader.ReadString();
                        __boardconfig__b__ = true;
                        break;
                    case 3:
                        __platform__ = reader.ReadString();
                        __platform__b__ = true;
                        break;
                    case 4:
                        __cpid__ = reader.ReadInt32();
                        __cpid__b__ = true;
                        break;
                    case 5:
                        __bdid__ = reader.ReadInt32();
                        __bdid__b__ = true;
                        break;
                    case 6:
                        __firmwares__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Firmware>>().Deserialize(ref reader, formatterResolver);
                        __firmwares__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::IPSWdl.JsonReps.FirmwareListing();
            if(__name__b__) ____result.name = __name__;
            if(__identifier__b__) ____result.identifier = __identifier__;
            if(__boardconfig__b__) ____result.boardconfig = __boardconfig__;
            if(__platform__b__) ____result.platform = __platform__;
            if(__cpid__b__) ____result.cpid = __cpid__;
            if(__bdid__b__) ____result.bdid = __bdid__;
            if(__firmwares__b__) ____result.firmwares = __firmwares__;

            return ____result;
        }
    }


    public sealed class JsonReps_jankFormatter : global::Utf8Json.IJsonFormatter<global::IPSWdl.JsonReps.jank>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public JsonReps_jankFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("devices"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("devices"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::IPSWdl.JsonReps.jank value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Device>>().Serialize(ref writer, value.devices, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::IPSWdl.JsonReps.jank Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }
            

            var __devices__ = default(global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Device>);
            var __devices__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __devices__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::IPSWdl.JsonReps.Device>>().Deserialize(ref reader, formatterResolver);
                        __devices__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::IPSWdl.JsonReps.jank();
            if(__devices__b__) ____result.devices = __devices__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
