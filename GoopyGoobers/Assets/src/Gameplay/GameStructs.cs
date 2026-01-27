using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
namespace Gameplay {

    ////////////////////////////////////////
    ////////////////////////////////////////
    public struct Enemy {
        public EnemyType Type;
        public int Col;
        public int Row;
        public float RemainingLifetime;
        public GameObject Object;
    }

    public enum EnemyType {
        Grid
    }

    ////////////////////////////////////////
    ////////////////////////////////////////
    public struct Level {
        public Guid Id;
        public string Name;
        public int KillsToWin;
        public List<Spawner> Spawners;

        public Level(string name, int killsToWin, List<Spawner> spawners) {
            Id = Guid.NewGuid();
            Name = name;
            KillsToWin = killsToWin;
            Spawners = spawners;
        }

        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(Object obj) {
            if (obj is not Level item) return false;
            return Id == item.Id;
        }
    }

    ////////////////////////////////////////
    ////////////////////////////////////////
    public class Spawner {
        public Guid Id;
        public EnemyType Type;

        public int KillsToActivate;
        public int KillsToDeactivate;
        public float BaseFreq;
        public float BaseLifetime;
        public float IncreasedFreqPerKill;
        public float DecreasedLifetimePerKill;
        public float MaxFreq;
        public float MinLifetime;

        public float CurrFreq;
        public float CurrLifetime;

        public int CurrSpawnCount;
        public float CurrSpawnStopwatch;

        public Spawner(
            EnemyType type,
            int killsToActivate,
            int killsToDeactivate,
            float baseFreq,
            float baseLifetime,
            float increasedFreqPerKill = 0f,
            float decreasedLifetimePerKill = 0f,
            float maxFreq = 0f,
            float minLifetime = 0f
        ) {
            if (baseFreq <= 0) throw new ArgumentOutOfRangeException(nameof(baseFreq));
            if (baseLifetime <= 0) throw new ArgumentOutOfRangeException(nameof(baseLifetime));

            Id = Guid.NewGuid();
            Type = type;
            KillsToActivate = killsToActivate;
            KillsToDeactivate = killsToDeactivate;
            BaseFreq = baseFreq;
            BaseLifetime = baseLifetime;
            IncreasedFreqPerKill = increasedFreqPerKill;
            DecreasedLifetimePerKill = decreasedLifetimePerKill;
            MaxFreq = maxFreq;
            MinLifetime = minLifetime;

            CurrFreq = baseFreq;
            CurrLifetime = baseLifetime;
        }

        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(Object obj) {
            if (obj is not Spawner item) return false;
            return Id == item.Id;
        }

        /// <summary>
        /// Uses deltaTime to update internal stopwatch.
        /// If enough time has elapsed to warrant a new spawn, reset internal stopwatch, and return true.
        /// If not, return false.
        /// </summary>
        public bool CheckSpawnTime(float deltaTime) {
            CurrSpawnStopwatch += deltaTime;
            if (CurrSpawnStopwatch < 1 / CurrFreq) return false;
            CurrSpawnStopwatch = 0f;
            CurrSpawnCount++;
            return true;
        }

        /// <summary>
        /// Update freq and lifetime as if one kill was added.
        /// </summary>
        public void AddKill() {
            if (CurrFreq < MaxFreq)
                CurrFreq = MathF.Min(CurrFreq + IncreasedFreqPerKill, MaxFreq);
            if (CurrLifetime > MinLifetime)
                CurrLifetime = MathF.Max(CurrLifetime - DecreasedLifetimePerKill, MinLifetime);
        }
    }
}
