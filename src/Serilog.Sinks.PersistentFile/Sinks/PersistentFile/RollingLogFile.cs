/*
 * Source files of NuGet package called 'Serilog.Sinks.PersistentFile' found at https://www.nuget.org/packages/Serilog.Sinks.PersistentFile/.
 * Github page can be found at https://github.com/dfacto-lab/serilog-sinks-file.
 * It is a fork of Serilog.Sinks.File with Github page https://github.com/serilog/serilog-sinks-file/ which can be found as a NuGet package at https://www.nuget.org/packages/Serilog.Sinks.File/.
 * It has been grabbed not as package but as pure source code because it only supports .NET 8.0 but because of Kubernates and multi pods we cannot use that version.
 * Therefore this was translated by a tool to .NET 6.0 source code which isn't officially supported.
 * Once we can support .NET 8.0 or if the NuGet package supports lower versions, we will migrate back over to the use of the NuGet package.
 */
// Copyright 2013-2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Serilog.Sinks.PersistentFile
{
    class RollingLogFile
    {
        public RollingLogFile(string filename, DateTime? dateTime, int? sequenceNumber)
        {
            Filename = filename;
            DateTime = dateTime;
            SequenceNumber = sequenceNumber;
        }

        public string Filename { get; }

        public DateTime? DateTime { get; }

        public int? SequenceNumber { get; }
    }
}
