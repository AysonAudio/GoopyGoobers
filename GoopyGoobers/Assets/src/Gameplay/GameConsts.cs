using System.Collections.Generic;
namespace Gameplay {
    public static class Levels {
        public static Level Easy = new("Easy", 50, new List<Spawner> {
            new(
                type: EnemyType.Grid,
                killsToActivate: 0,
                killsToDeactivate: 50,
                baseFreq: 0.5f,
                baseLifetime: 4f,
                increasedFreqPerKill: 0.05f,
                decreasedLifetimePerKill: 0.1f,
                maxFreq: 1f,
                minLifetime: 2f
            )
        });
    }
}
