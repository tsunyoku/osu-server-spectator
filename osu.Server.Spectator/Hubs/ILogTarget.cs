// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Microsoft.Extensions.Logging;

namespace osu.Server.Spectator.Hubs
{
    internal interface ILogTarget
    {
        void Log(string message, LogLevel logLevel = LogLevel.Information);

        void Error(string message, Exception exception);
    }
}
