﻿using TrueCraft.Core;
using YamlDotNet.Serialization;

namespace TrueCraft
{
    public class ServerConfiguration : Configuration
    {
        public const int ServerPortDefault = 25565;
        public const bool QueryDefault = true;
        public const int QueryPortDefault = 25566;
        public const bool EnableLightingDefault = true;
        public const bool EnableEventLoadingDefault = true;
        public const bool SinglePlayerDefault = false;

        public class DebugConfiguration
        {
            public class ProfilerConfiguration
            {
                public ProfilerConfiguration()
                {
                    Buckets = "";
                }

                [YamlMember(Alias = "buckets")]
                public string Buckets { get; set; }

                [YamlMember(Alias = "lag")]
                public bool Lag { get; set; }
            }

            public DebugConfiguration()
            {
                DeleteWorldOnStartup = false;
                DeletePlayersOnStartup = false;
                Profiler = new ProfilerConfiguration();
            }

            [YamlMember(Alias = "deleteWorldOnStartup")]
            public bool DeleteWorldOnStartup { get; set; }

            [YamlMember(Alias = "deletePlayersOnStartup")]
            public bool DeletePlayersOnStartup { get; set; }

            [YamlMember(Alias = "profiler")]
            public ProfilerConfiguration Profiler { get; set; }
        }

        public ServerConfiguration()
        {
            MOTD = "Welcome to TrueCraft!";
            Debug = new DebugConfiguration();
            ServerPort = ServerPortDefault;
            ServerAddress = "0.0.0.0";
            WorldSaveInterval = 30;
            Singleplayer = SinglePlayerDefault;
            Query = QueryDefault;
            QueryPort = QueryPortDefault;
            EnableLighting = EnableLightingDefault;
            EnableEventLoading = EnableEventLoadingDefault;
            DisabledEvents = new string[0];
        }

        [YamlMember(Alias = "motd")]
        public string MOTD { get; set; }

        [YamlMember(Alias = "bind-port")]
        public int ServerPort {get; set; }

        [YamlMember(Alias = "bind-endpoint")]
        public string ServerAddress { get; set; }

        [YamlMember(Alias = "debug")]
        public DebugConfiguration Debug { get; set; }

        [YamlMember(Alias = "save-interval")]
        public int WorldSaveInterval { get; set; }

        [YamlIgnore]
        public bool Singleplayer { get; set; }

        [YamlMember(Alias = "query-enabled")]
        public bool Query { get; set; }

        [YamlMember(Alias = "query-port")]
        public int QueryPort { get; set; }

        [YamlMember(Alias = "enable-lighting")]
        public bool EnableLighting { get; set; }
        
        [YamlMember(Alias = "enable-event-loading")]
        public bool EnableEventLoading { get; set; }
        
        [YamlMember(Alias = "disable-events")]
        public string[] DisabledEvents { get; set; }
    }
}