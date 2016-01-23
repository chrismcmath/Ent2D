using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ent2D;
using Ent2D.Utils;

namespace Ent2D.Conflict {
    public static class ConflictClinic {
        public static bool Logging = false;

        public static List<BattleConflict> _Conflicts = new List<BattleConflict>();
        public static List<BattleConflictResolution> _Resolutions = new List<BattleConflictResolution>();

        public const string LOG_CONTROLLER_PREFIX = "<color=#add8e6ff>";
        public const string LOG_CONTROLLER_SUFFIX = "</color>";
        public const string LOG_BEHAVIOUR_PREFIX = "<color=#008000ff>";
        public const string LOG_BEHAVIOUR_SUFFIX = "</color>";

        public static void RegisterConflict(EntController contA,
                EntController contB,
                EntUtils.ColliderType typeA,
                EntUtils.ColliderType typeB) {

            if (Logging) {
                Debug.LogFormat("[CONFLICT] Attempt to register\n{0}{1}{2} ({3}{4}{5}) - {6}{7}{8} ({9}{10}{11})",
                        LOG_CONTROLLER_PREFIX, contA.name, LOG_CONTROLLER_SUFFIX,
                        LOG_BEHAVIOUR_PREFIX, typeA, LOG_BEHAVIOUR_SUFFIX,
                        LOG_CONTROLLER_PREFIX, contB.name, LOG_CONTROLLER_SUFFIX,
                        LOG_BEHAVIOUR_PREFIX, typeB, LOG_BEHAVIOUR_SUFFIX
                        );
            }

            BattleConflict conflict = new BattleConflict(contA, contB, typeA, typeB);
            if (!CollectionUtils.Contains(_Conflicts, conflict)) {
                _Conflicts.Add(new BattleConflict(contA, contB, typeA, typeB));
                if (Logging) {
                    Debug.Log("[CONFLICT] accepted count now " + _Conflicts.Count);
                }
            } else if (Logging) {
                Debug.Log("[CONFLICT] rejected");
            }
        }

        public static void ResolveConflicts() {
            if (_Conflicts.Count > 0 && Logging) {
                Debug.Log("---------- resolving "  + _Conflicts.Count + " conflicts ----------");
                PrintAllConflicts();
            }

            foreach (BattleConflict conflict in _Conflicts) {
                if (Logging) {
                    Debug.Log("------ resolving conflict ------");
                    conflict.Print();
                }

                BattleConflictAgitator winner = conflict.Winner;
                if (winner != null) {
                    _Resolutions.Add(
                            new BattleConflictResolution(winner,
                                BattleConflictResolution.Result.WIN,
                                conflict));
                    _Resolutions.Add(
                            new BattleConflictResolution(conflict.Loser,
                                BattleConflictResolution.Result.LOSE,
                                conflict));
                } else {
                    /* need to refactor
                       foreach (BattleConflictAgitator agitator in conflict.Agitators) {
                       _Resolutions.Add(
                       new BattleConflictResolution(agitator.Agitator,
                       BattleConflictResolution.Result.DRAW,
                       conflict));
                       }
                       */
                }
            }

            _Conflicts.Clear();

            PruneConflicts();

            foreach (BattleConflictResolution resolution in _Resolutions) {
                resolution.Resolve();
            }

            _Resolutions.Clear();
        }

        public static bool HasConflict(EntController avatarCont) {
            foreach (BattleConflict conflict in _Conflicts) {
                if (conflict.HasAgitator(avatarCont)) {
                    return true;
                }
            }
            return false;
        }

        private static void PruneConflicts() {
            List<BattleConflictResolution> simConfs = new List<BattleConflictResolution>();
            foreach (BattleConflictResolution resolution in _Resolutions) {
                simConfs = GetSimultaneousConflicts(resolution);
                if (simConfs.Count > 1) {
                    break;
                }
            }

            if (simConfs.Count > 1) {
                BattleConflictResolution final = GetWorstResolution(simConfs);
                foreach (BattleConflictResolution resolution in simConfs) {
                    if (resolution != final) {
                        _Resolutions.Remove(resolution);
                    }
                }

                //Recurse
                PruneConflicts();
            }
        }

        private static BattleConflictResolution GetWorstResolution(List<BattleConflictResolution> simConfs) {
            BattleConflictResolution resolution = simConfs[0];

            for (int i = 1; i < simConfs.Count; i++) {
                if (WorseResolution(simConfs[i], resolution)) {
                    resolution = simConfs[i];
                }
            }
            return resolution;
        }

        private static bool WorseResolution(BattleConflictResolution r1, BattleConflictResolution r2) {
            return (int) r1.ConflictResult < (int) r2.ConflictResult;
        }

        private static List<BattleConflictResolution> GetSimultaneousConflicts(BattleConflictResolution resolution) {
            List<BattleConflictResolution> simConfs = new List<BattleConflictResolution>();

            //PrintAllResolutions();
            foreach (BattleConflictResolution r in _Resolutions) {
                if (r.Agitator.Controller == resolution.Agitator.Controller) {
                    simConfs.Add(r);
                }
            }
            return simConfs;
        }

        public static void PrintAllConflicts() {
            foreach (BattleConflict conflict in _Conflicts) {
                conflict.Print();
            }
        }

        public static void PrintAllResolutions() {
            foreach (BattleConflictResolution r in _Resolutions) {
                r.Print();
            }
        }

        private static void Win(EntController winner) {
            if (Logging) {
                Debug.Log("------ winner: + " + winner.name + " ------");
            }
        }
        private static void Lose(EntController loser, EntController winner) {
            if (Logging) {
                Debug.Log("------ loser: + " + loser.name + " ------");
            }
        }
        private static void Draw(EntController stalemate) {
            if (Logging) {
                Debug.Log("------ stalemate: + " + stalemate.name + " ------");
            }
        }
    }
}
