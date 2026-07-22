using System;
using System.Collections.Generic;
using EFT;
using Fika.Core.Modding;
using Fika.Core.Modding.Events;
using Fika.Core.Networking.LiteNetLib;

namespace SPTMapProgression.Compatibility;

public class FikaCompatibility
{

    private List<NetPeer> _peers;
    
    public FikaCompatibility()
    {
        _peers = [];

        void OnPeerConnected(PeerConnectedEvent e)
        {
            _peers.Add(e.Peer);
        }
        FikaEventDispatcher.SubscribeEvent((Action<PeerConnectedEvent>) OnPeerConnected);
        
        void OnPeerDisconnected(PeerDisconnectedEvent e)
        {
            _peers.Remove(e.Peer);
        }
        FikaEventDispatcher.SubscribeEvent((Action<PeerDisconnectedEvent>) OnPeerDisconnected);
    }
    
    public List<Player> GetPeers()
    {
        List<Player> players = [];
        foreach (NetPeer p in _peers)
        {
            players.Add(p.Player.GetPlayer);
        }

        return players;
    }
}