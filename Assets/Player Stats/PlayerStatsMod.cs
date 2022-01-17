using HoldfastSharedMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/*
 * TODO:
 * Player Dictionary
 */
public class PlayerStatsMod : IHoldfastSharedMethods
{
    private InputField f1MenuInputField;
    public static Dictionary<int, playerStruct> playerIdDictionary = new Dictionary<int, playerStruct>();
    private int key = 0;

    public void OnIsServer(bool server)
    {
        // Get all the canvas items in the game
        var canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        for (int i = 0; i < canvases.Length; i++)
        {
            // Find the one that's called "Game Console Panel"
            if (string.Compare(canvases[i].name, "Game Console Panel", true) == 0)
            {
                // Inside this, now we need to find the input field where the player types messages.
                f1MenuInputField = canvases[i].GetComponentInChildren<InputField>(true);
                if (f1MenuInputField != null)
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

        var player = playerIdDictionary[playerId];

        WWWForm form = new WWWForm();
        form.AddField("playerId", playerId);
        form.AddField("channel", (int)channel);
        form.AddField("steamId", (int)player._steamId);
        form.AddField("playerName", player._playerName);
        form.AddField("regimentTag", player._regimentTag);
        form.AddField("text", text);

        new WWW("https://api.plexidev.org/api", form);

        // Repeat any messages
        f1MenuInputField.onEndEdit.Invoke(string.Format("serverAdmin privateMessage {0} {1} - 2", playerId, text));
    }

    public void PassConfigVariables(string[] value)
    {
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

    }

    public void OnPlayerShoot(int playerId, bool dryShot)
    {

    }

    public void OnPlayerJoined(int playerId, ulong steamId, string playerName, string regimentTag, bool isBot)
    {
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