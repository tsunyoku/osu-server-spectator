// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using osu.Game.Scoring;
using osu.Game.Scoring.Legacy;

namespace osu.Server.Spectator.Storage
{
    public class ReplayServerScoreStorage : IScoreStorage
    {
        private readonly ILogger logger;

        public ReplayServerScoreStorage(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger(nameof(S3ScoreStorage));
        }

        public async Task WriteAsync(Score score)
        {
            using var outStream = new MemoryStream();

            new LegacyScoreEncoder(score, null).Encode(outStream, true);

            outStream.Seek(0, SeekOrigin.Begin);

            logger.LogInformation($"Uploading replay for score {score.ScoreInfo.OnlineID}");

            var httpClient = new HttpClient(new HttpClientHandler());

            var form = new MultipartFormDataContent();
            form.Add(new ByteArrayContent(outStream.ToArray()), "replayFile", $"{score.ScoreInfo.OnlineID}.osr");

            await httpClient.PutAsync($"{AppSettings.ReplayServerDomain}/replays/{score.ScoreInfo.OnlineID}", form);
        }
    }
}
