// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading.Tasks;

namespace osu.Server.Spectator.Hubs.Spectator
{
    /// <summary>
    /// Allows hub clients to receive notifications about the completion of processing of a score.
    /// </summary>
    public interface IScoreProcessedSubscriber
    {
        /// <summary>
        /// Registers a hub client for future notifications about the completion of processing of a score.
        /// </summary>
        /// <param name="receiverConnectionId">The ID of the connection that should receive the notifications.</param>
        /// <param name="userId">The ID of the user who set the score.</param>
        /// <param name="scoreId">The ID of the score which is being processed.</param>
        Task RegisterForSingleScoreAsync(string receiverConnectionId, int userId, long scoreId);

        /// <summary>
        /// Registers a hub client for future notifications about incoming scores in a given <paramref name="roomId"/>.
        /// </summary>
        Task RegisterForMultiplayerRoomAsync(int userId, long roomId);

        /// <summary>
        /// Unregisters a hub client from future notifications about incoming scores in a given <paramref name="roomId"/>.
        /// </summary>
        Task UnregisterFromMultiplayerRoomAsync(int userId, long roomId);

        /// <summary>
        /// Unregisters a hub client from all multiplayer room subscriptions.
        /// </summary>
        Task UnregisterFromAllMultiplayerRoomsAsync(int userId);
    }
}
