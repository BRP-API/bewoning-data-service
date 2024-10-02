/*
 * Source files of NuGet package called 'Serilog.Sinks.PersistentFile' found at https://www.nuget.org/packages/Serilog.Sinks.PersistentFile/.
 * Github page can be found at https://github.com/dfacto-lab/serilog-sinks-file.
 * It is a fork of Serilog.Sinks.File with Github page https://github.com/serilog/serilog-sinks-file/ which can be found as a NuGet package at https://www.nuget.org/packages/Serilog.Sinks.File/.
 * It has been grabbed not as package but as pure source code because it only supports .NET 8.0 but because of Kubernates and multi pods we cannot use that version.
 * Therefore this was translated by a tool to .NET 6.0 source code which isn't officially supported.
 * Once we can support .NET 8.0 or if the NuGet package supports lower versions, we will migrate back over to the use of the NuGet package.
 */
// Copyright 2013-2016 Serilog Contributors
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Serilog.Sinks.PersistentFile
{
    class PathRoller
    {
        const string PeriodMatchGroup = "period";
        const string SequenceNumberMatchGroup = "sequence";

        readonly string _directory;
        readonly string _filenamePrefix;
        readonly string _filenameSuffix;
        readonly Regex _filenameMatcher;

        readonly PersistentFileRollingInterval _interval;
        readonly string _periodFormat;

        public PathRoller(string path, PersistentFileRollingInterval interval)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            _interval = interval;
            _periodFormat = interval.GetFormat();

            var pathDirectory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(pathDirectory))
                pathDirectory = Directory.GetCurrentDirectory();

            _directory = Path.GetFullPath(pathDirectory);
            _filenamePrefix = Path.GetFileNameWithoutExtension(path);
            _filenameSuffix = Path.GetExtension(path);
            _filenameMatcher = new Regex(
                "^" +
                Regex.Escape(_filenamePrefix) +
                "(?<" + PeriodMatchGroup + ">\\d{" + _periodFormat.Length + "}){0,1}" +
                "(?<" + SequenceNumberMatchGroup + ">_[0-9]{3,}){0,1}" +
                Regex.Escape(_filenameSuffix) +
                "$");

            DirectorySearchPattern = $"{_filenamePrefix}*{_filenameSuffix}";
        }

        public string LogFileDirectory => _directory;

        public string DirectorySearchPattern { get; }

        public void GetLogFilePath(DateTime date, int? sequenceNumber, out string path)
        {
            var currentCheckpoint = GetCurrentCheckpoint(date);

            var tok = currentCheckpoint?.ToString(_periodFormat, CultureInfo.InvariantCulture) ?? "";

            if (currentCheckpoint == null && sequenceNumber == null)
                sequenceNumber = 1;

            if (sequenceNumber != null)
                tok += "_" + sequenceNumber.Value.ToString("000", CultureInfo.InvariantCulture);

            path = Path.Combine(_directory, _filenamePrefix + tok + _filenameSuffix);
        }

        public void GetLogFilePath(out string path)
        {
            path = Path.Combine(_directory, _filenamePrefix + _filenameSuffix);
        }

        public IEnumerable<RollingLogFile> SelectMatches(IEnumerable<string> filenames)
        {
            foreach (var filename in filenames)
            {
                var match = _filenameMatcher.Match(filename);
                if (!match.Success)
                    continue;

                int? inc = null;
                var incGroup = match.Groups[SequenceNumberMatchGroup];
                if (incGroup.Captures.Count != 0)
                {
                    var incPart = incGroup.Captures[0].Value.Substring(1);
                    inc = int.Parse(incPart, CultureInfo.InvariantCulture);
                }

                DateTime? period = null;
                var periodGroup = match.Groups[PeriodMatchGroup];
                if (periodGroup.Captures.Count != 0)
                {
                    var dateTimePart = periodGroup.Captures[0].Value;
                    if (DateTime.TryParseExact(
                        dateTimePart,
                        _periodFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var dateTime))
                    {
                        period = dateTime;
                    }
                }

                yield return new RollingLogFile(filename, period, inc);
            }
        }

        public DateTime? GetCurrentCheckpoint(DateTime instant) => _interval.GetCurrentCheckpoint(instant);

        public DateTime? GetNextCheckpoint(DateTime instant) => _interval.GetNextCheckpoint(instant);
    }
}
