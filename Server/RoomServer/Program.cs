﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Linq;
using Service.Core;
using RoomServer.Services;
using System.Threading;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Service.HttpMisc;

namespace RoomServer
{
    class Program
    {

        static void Main(string[] args)
        {
            string key = "SomeConnectionKey";
            int port = 50000;
            uint mapId = 1;
            ushort playerNumber = 100;
            int gsPort = 9030;
            string hash = "";
            string logServerUrl = "";
            if (args.Length > 0)
            {
                if (Array.IndexOf(args, "-key") > -1) key = args[Array.IndexOf(args, "-key") + 1];
                if (Array.IndexOf(args, "-port") > -1) port = Convert.ToInt32(args[Array.IndexOf(args, "-port") + 1]);
                if (Array.IndexOf(args, "-mapId") > -1) mapId = Convert.ToUInt32(args[Array.IndexOf(args, "-mapId") + 1]);
                if (Array.IndexOf(args, "-playernumber") > -1) playerNumber = Convert.ToUInt16(args[Array.IndexOf(args, "-playernumber") + 1]);
                if (Array.IndexOf(args, "-gsPort") > -1) gsPort = Convert.ToInt32(args[Array.IndexOf(args, "-gsPort") + 1]);
                if (Array.IndexOf(args, "-hash") > -1) hash = args[Array.IndexOf(args, "-hash") + 1];
                if (Array.IndexOf(args, "-logserverurl") > -1) logServerUrl = args[Array.IndexOf(args, "-logserverurl") + 1];
            }
            var logger = new ConsoleLogger.LoggerImpl.Logger("ROOMSERVER:"+port, logServerUrl);
            logger.EnableConsoleOutput = false;
            Services.RoomApplication.Logger = logger;
            logger.Log("LogServer Address: " + logServerUrl);
            Services.RoomApplication room = new Services.RoomApplication(key);
            room.StartServer(port);
            ServerDll.Service.Modules.Service.GetModule<RoomServer.Modules.BattleModule>().InitStartup(mapId,playerNumber, gsPort,hash);

            //ConsoleLogger.CommandParser commandParser = new ConsoleLogger.CommandParser(logger);
            //commandParser.AddCommand(new ConsoleLogger.CommandAction("-exit", cmdParams => { AppQuit(); return ConsoleLogger.CommandExecuteRet.Break; }, "Exit app."))
            //    .ReadCommandLine();

            while (true)
                System.Threading.Thread.Sleep(1000);
        }


        
        static void AppQuit()
        {
            
        }

    }
}