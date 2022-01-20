using HoldfastSharedMethods;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : IHoldfastSharedMethods
{
    public static Dictionary<int, playerStruct> playerIdDictionary = new Dictionary<int, playerStruct>();
    public PlayerStats playerStats;

    public void OnIsServer(bool server)
    {
        // Why? http://answers.unity.com/answers/1269136/view.html
        playerStats = (new GameObject("PlayerStats")).AddComponent<PlayerStats>();

        var canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        for (int i = 0; i < canvases.Length; i++)
        {
            if (string.Compare(canvases[i].name, "Game Console Panel", true) == 0)
            {
                playerStats.SetInputField(canvases[i].GetComponentInChildren<InputField>(true));
                if (playerStats.GetInputField() != null)
                {
                    Debug.Log("Found the Game Console Panel");
                }
                else
                {
                    Debug.Log("Game Console Panel not found");
                }
                break;
            }
        }
    }

    public void OnTextMessage(int playerId, TextChatChannel channel, string text)
    {

    }

    public void PassConfigVariables(string[] value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            var splitData = value[i].Split(':');
            if (splitData.Length != 3) continue;

            if (splitData[0] == "2715432949")
            {
                if (splitData[1] == "token")
                {
                    Debug.Log("[PlayerStats] Token Found");
                    playerStats.SetToken(splitData[2]);
                } else if (splitData[1] == "feedback")
                {
                    Debug.Log("[PlayerStats] Feedback Option Passed");
                    playerStats.SetFeedback(bool.Parse(splitData[2]));
                }
            }

        }
    }

    public void OnSyncValueState(int value)
    {

    }

    public void OnUpdateSyncedTime(double time)
    {

    }

    public void OnUpdateElapsedTime(float time)
    {

    }

    public void OnUpdateTimeRemaining(float time)
    {

    }

    public void OnIsClient(bool client, ulong steamId)
    {

    }

    public void OnDamageableObjectDamaged(GameObject damageableObject, int damageableObjectId, int shipId, int oldHp, int newHp)
    {

    }

    public void OnPlayerHurt(int playerId, byte oldHp, byte newHp, EntityHealthChangedReason reason)
    {

    }

    public void OnPlayerKilledPlayer(int killerPlayerId, int victimPlayerId, EntityHealthChangedReason reason, string additionalDetails)
    {
        var killer = playerIdDictionary[killerPlayerId];
        var victim = playerIdDictionary[victimPlayerId];

        WWWForm data = new WWWForm();

        data.AddField("killerId", killerPlayerId);
        data.AddField("victimId", victimPlayerId);

        data.AddField("killerSteamId", (int)killer._steamId);
        data.AddField("victimSteamId", (int)victim._steamId);

        data.AddField("killerPlayerName", killer._playerName);
        data.AddField("victimPlayerName", victim._playerName);

        data.AddField("killerRegimentTag", killer._regimentTag);
        data.AddField("victimRegimentTag", victim._regimentTag);

        data.AddField("reason", (int)reason);
        data.AddField("details", additionalDetails);

        playerStats.SendEvent("playerKilledPlayer", data);
    }

    public void OnPlayerShoot(int playerId, bool dryShot)
    {

    }

    public void OnPlayerJoined(int playerId, ulong steamId, string playerName, string regimentTag, bool isBot)
    {
        if (isBot) steamId = 0;
        playerStruct temp = new playerStruct()
        {
            _steamId = steamId,
            _playerName = playerName,
            _regimentTag = regimentTag,
            _isBot = isBot
        };
        playerIdDictionary[playerId] = temp;
    }

    public void OnPlayerLeft(int playerId)
    {
        playerIdDictionary.Remove(playerId);
    }

    public void OnPlayerSpawned(int playerId, int spawnSectionId, FactionCountry playerFaction, PlayerClass playerClass, int uniformId, GameObject playerObject)
    {

    }

    public void OnScorableAction(int playerId, int score, ScorableActionType reason)
    {

    }

    public void OnRoundDetails(int roundId, string serverName, string mapName, FactionCountry attackingFaction, FactionCountry defendingFaction, GameplayMode gameplayMode, GameType gameType)
    {

    }

    public void OnPlayerBlock(int attackingPlayerId, int defendingPlayerId)
    {

    }

    public void OnPlayerMeleeStartSecondaryAttack(int playerId)
    {

    }

    public void OnPlayerWeaponSwitch(int playerId, string weapon)
    {

    }

    public void OnCapturePointCaptured(int capturePoint)
    {

    }

    public void OnCapturePointOwnerChanged(int capturePoint, FactionCountry factionCountry)
    {

    }

    public void OnCapturePointDataUpdated(int capturePoint, int defendingPlayerCount, int attackingPlayerCount)
    {

    }

    public void OnRoundEndFactionWinner(FactionCountry factionCountry, FactionRoundWinnerReason reason)
    {

    }

    public void OnRoundEndPlayerWinner(int playerId)
    {

    }

    public void OnPlayerStartCarry(int playerId, CarryableObjectType carryableObject)
    {

    }

    public void OnPlayerEndCarry(int playerId)
    {

    }

    public void OnPlayerShout(int playerId, CharacterVoicePhrase voicePhrase)
    {

    }

    public void OnInteractableObjectInteraction(int playerId, int interactableObjectId, GameObject interactableObject, InteractionActivationType interactionActivationType, int nextActivationStateTransitionIndex)
    {

    }

    public void OnEmplacementPlaced(int itemId, GameObject objectBuilt, EmplacementType emplacementType)
    {

    }

    public void OnEmplacementConstructed(int itemId)
    {

    }

    public void OnBuffStart(int playerId, BuffType buff)
    {

    }

    public void OnBuffStop(int playerId, BuffType buff)
    {

    }

    public void OnShotInfo(int playerId, int shotCount, Vector3[][] shotsPointsPositions, float[] trajectileDistances, float[] distanceFromFiringPositions, float[] horizontalDeviationAngles, float[] maxHorizontalDeviationAngles, float[] muzzleVelocities, float[] gravities, float[] damageHitBaseDamages, float[] damageRangeUnitValues, float[] damagePostTraitAndBuffValues, float[] totalDamages, Vector3[] hitPositions, Vector3[] hitDirections, int[] hitPlayerIds, int[] hitDamageableObjectIds, int[] hitShipIds, int[] hitVehicleIds)
    {

    }

    public void OnVehicleSpawned(int vehicleId, FactionCountry vehicleFaction, PlayerClass vehicleClass, GameObject vehicleObject, int ownerPlayerId)
    {

    }

    public void OnVehicleHurt(int vehicleId, byte oldHp, byte newHp, EntityHealthChangedReason reason)
    {

    }

    public void OnPlayerKilledVehicle(int killerPlayerId, int victimVehicleId, EntityHealthChangedReason reason, string details)
    {

    }

    public void OnShipSpawned(int shipId, GameObject shipObject, FactionCountry shipfaction, ShipType shipType, int shipNameId)
    {

    }

    public void OnShipDamaged(int shipId, int oldHp, int newHp)
    {

    }

    public void OnAdminPlayerAction(int playerId, int adminId, ServerAdminAction action, string reason)
    {

    }

    public void OnConsoleCommand(string input, string output, bool success)
    {

    }

    public void OnRCLogin(int playerId, string inputPassword, bool isLoggedIn)
    {

    }

    public void OnRCCommand(int playerId, string input, string output, bool success)
    {

    }
}

public class playerStruct
{
    internal ulong _steamId;
    internal bool _isBot;
    internal string _regimentTag;
    internal string _playerName;
}