using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
namespace Gameplay {

    public class GameManager : MonoBehaviour {
        public static readonly Level NullLevel = new("NullLevel", 0, new List<Spawner>());
        public static Level ActiveLevel { get; private set; } = NullLevel;

        static int _kills = 0;
        public static int Kills { get => _kills; set => SetKills(value); }

        static int _lives = 3;
        public static int Lives { get => _lives; set => SetLives(value); }

        public GameObject gridEnemyPrefab;
        [Min(1)] public int gridCols = 3;
        [Min(1)] public int gridRows = 3;
        public Vector3 gridOrigin = new(-2.2f, 1.1f, -3);
        public Vector3 gridSquareSize = new(3, 0, 3);

        public static List<(int, int)> EmptyGridSquares { get; private set; } = new();
        /// <summary> KEY: Col : Row </summary>
        static Safe2D<int, int, Enemy> _gridEnemies = new();
        /// <summary> KEY: KillsToActivate </summary>
        static Safe2D<int, Spawner> _queuedSpawners = new();
        /// <summary> KEY: KillsToDeactivate </summary>
        static Safe2D<int, Spawner> _activatedSpawners = new();

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        void Start() {
            // Test Gameplay
            // TODO: choose level to start in main menu instead
            StartLevel(Levels.Easy);
        }

        void Update() {
            foreach (var (_, spawnerList) in _activatedSpawners)
                foreach (var spawner in spawnerList.Where(spawner => spawner.CheckSpawnTime(Time.deltaTime)))
                    SpawnEnemy(spawner);
        }

        ////////////////////////////////////////
        // Game State Management
        ////////////////////////////////////////
        public void RestartLevel() => StartLevel(ActiveLevel);
        public void StartLevel(Level level) {
            ActiveLevel = level;
            _kills = 0;
            _lives = 3;
            EmptyGridSquares.Clear();
            for (var i = 0; i < gridCols; i++)
                for (var j = 0; j < gridRows; j++)
                    EmptyGridSquares.Add((i, j));
            _gridEnemies.Clear();
            _queuedSpawners.Clear();
            _activatedSpawners.Clear();
            foreach (var spawner in level.Spawners) QueueSpawner(spawner);
        }

        public void SpawnEnemy(Spawner spawner) {
            switch (spawner.Type) {
                default:
                case EnemyType.Grid:
                    if (EmptyGridSquares.Count == 0) return;
                    var i = RandomManager.Rng.Next(0, EmptyGridSquares.Count);
                    var randomEmptyGridSquare = EmptyGridSquares[i];
                    var spawnLocationX = gridOrigin.x + gridSquareSize.x * randomEmptyGridSquare.Item1;
                    var spawnLocationZ = gridOrigin.z + gridSquareSize.z * randomEmptyGridSquare.Item2;
                    var spawnLocation = new Vector3(spawnLocationX, gridOrigin.y, spawnLocationZ);
                    var enemy = new Enemy();
                    enemy.Type = EnemyType.Grid;
                    enemy.Col = randomEmptyGridSquare.Item1;
                    enemy.Row = randomEmptyGridSquare.Item2;
                    enemy.RemainingLifetime = spawner.CurrLifetime;
                    enemy.Object = Instantiate(gridEnemyPrefab, spawnLocation, gridEnemyPrefab.transform.rotation);
                    _gridEnemies[enemy.Col][enemy.Row] = enemy;
                    EmptyGridSquares.RemoveAt(i);
                    break;
            }
        }

        public static bool KillGridEnemyAt(int col, int row) {
            if (!_gridEnemies.ContainsKey(col)) return false;
            if (!_gridEnemies[col].ContainsKey(row)) return false;

            var enemy = _gridEnemies[col][row];
            Destroy(enemy.Object);

            foreach (var (_, spawnerList) in _activatedSpawners)
                foreach (var spawner in spawnerList)
                    spawner.AddKill();

            if (_activatedSpawners.ContainsKey(_kills + 1))
                _activatedSpawners.Remove(_kills + 1);

            if (_queuedSpawners.ContainsKey(_kills + 1)) {
                _activatedSpawners[_kills + 1].AddRange(_queuedSpawners[_kills + 1]);
                _queuedSpawners.Remove(_kills + 1);
            }

            _gridEnemies.Remove(col, row);
            EmptyGridSquares.Add((col, row));
            Kills++;
            return true;
        }

        public static void WinGame() {
            // throw new NotImplementedException();
        }

        public static void LoseGame() {
            // throw new NotImplementedException();
        }

        ////////////////////////////////////////
        // Singleton Data Management
        ////////////////////////////////////////
        public static void SetKills(int kills) {
            _kills = kills;
            if (ActiveLevel.Equals(NullLevel)) return;
            if (kills >= ActiveLevel.KillsToWin) WinGame();
        }

        public static void SetLives(int lives) {
            _lives = lives;
            if (lives <= 0) LoseGame();
        }

        ////////////////////////////////////////
        // Private Methods
        ////////////////////////////////////////
        static void QueueSpawner(Spawner spawner) {
            if (Kills >= spawner.KillsToDeactivate) return;
            if (Kills >= spawner.KillsToActivate) {
                ActivateSpawner(spawner);
                return;
            } _queuedSpawners[spawner.KillsToActivate].Add(spawner);
        }

        static void ActivateSpawner(Spawner spawner) {
            if (spawner.KillsToActivate == spawner.KillsToDeactivate) return;
            _activatedSpawners[spawner.KillsToActivate].Add(spawner);
        }
    }
}
